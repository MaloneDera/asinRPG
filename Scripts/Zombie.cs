using Godot;
using System;
using System.Threading;

public partial class Zombie : CharacterBody2D
{
    public enum State {Loiter, Chase, Attack }
    [Export] public float WalkSpeed {get; set;} = 40.0f;
    [Export] public float ChaseSpeed {get; set;} = 90.0f;
    [Export] public AnimatedSprite2D Sprite {get; set;}
    [Export] public Area2D DetectionArea {get; set;}
    [Export] public Area2D AttackArea {get; set;}

    private State _currentState = State.Loiter;
    private Node2D _targetPlayer = null;

    private Vector2 _loiterDirection = Vector2.Zero;
    private double _loiterTimer = 0.0;
    private  System.Random _random = new System.Random();

    public override void _Ready()
    {
        DetectionArea.BodyEntered += OnDetectionBodyEntered;
        DetectionArea.BodyExited +=OnDetectionBodyExited;
        AttackArea.BodyEntered += OnAttackBodyEntered;
        AttackArea.BodyExited += OnAttackBodyExited;
        
        if(Sprite != null)
        {
            Sprite.AnimationFinished += OnAnimationFinished;
        }
        ChooseNewLoiterDirection();
    }
    public override void _PhysicsProcess(double delta)
    {
        switch (_currentState)
        {
            case State.Loiter: 
                ProcessLoiter(delta);
                break;
            case State.Chase:
                ProcessChase();
                break;
            case State.Attack:
                Velocity = Vector2.Zero;
                break;
        }
        MoveAndSlide();
        UpdateAnimations();
    }

    private void OnDetectionBodyEntered(Node2D body)
    {
        if(body is CharacterBody2D && body.Name == "Player2D")
        {
            _targetPlayer = body;
            _currentState = State.Chase;
        }
    }
    private void OnDetectionBodyExited(Node2D body)
    {
        if (body == _targetPlayer)
        {
            _targetPlayer = null;
            _currentState = State.Loiter;
            ChooseNewLoiterDirection();
        }
    }

    private void OnAttackBodyEntered(Node2D body)
    {
        if (body == _targetPlayer)
        {
            _currentState = State.Attack;
        }
    }
    private void OnAttackBodyExited(Node2D body)
    {
        if(body == _targetPlayer && _currentState != State.Attack)
        {
            _currentState = State.Chase;
        }
    }
    private void OnAnimationFinished()
    {
        if(_targetPlayer != null)
        {
            _currentState = AttackArea.OverlapsBody(_targetPlayer)? State.Attack : State.Chase;
        }
    }
    private void UpdateAnimations()
    {
        if(Sprite == null) return;
        if(_currentState == State.Attack)
        {
            Sprite.Play("zombie_attack");
        }
        else if(Velocity != Vector2.Zero)
        {
            Sprite.Play("zombie_walk");
            Sprite.FlipH = Velocity.X > 0;
        }
        else
        {
            Sprite.Play("zombie_idle");
        }
    }
    private void ChooseNewLoiterDirection()
    {
        int pick = _random.Next(0,4);
        _loiterDirection = pick switch
        {
            0=>Vector2.Up,
            1=>Vector2.Down,
            2=>Vector2.Left,
            3=>Vector2.Right,
            _=>Vector2.Zero
        };
        _loiterTimer = _random.NextDouble() * 2.0 + 1.0;
    }
    private void ProcessChase()
    {
        if (_targetPlayer != null)
        {
            Vector2 direction = (_targetPlayer.GlobalPosition - GlobalPosition).Normalized();
            Velocity = direction * ChaseSpeed;
        }
    }
    private void ProcessLoiter(double delta)
    {
        _loiterTimer -=delta;
        if(_loiterTimer <=0)
        {
            ChooseNewLoiterDirection();
        }
        Velocity = _loiterDirection * WalkSpeed;
    }

}
