using Godot;
using System;

public partial class Player2d : CharacterBody2D
{
    public enum State {Idle, Run, Attack}
    [Export] public float speed {get; set;} = 10.0f;
    [Export] public AnimatedSprite2D Sprite {get; set;}
    private State _currentState = State.Idle;

    private string _facingDirection = "down";

    public override void _Ready()
    {
        GD.Print("Player Loaded");
        if (Sprite != null)
        {
            Sprite.AnimationFinished += OnAnimationFinished;
        }
    }

    public override void _ExitTree()
    {
        if(Sprite != null)
        {
            Sprite.AnimationFinished -= OnAnimationFinished;
        }
    }

    public override void _PhysicsProcess(double delta)
    {

        switch (_currentState)
        {
            case State.Idle : 
            case State.Run : 
                HandleMovementAndInput();
                break;
            case State.Attack:
                Velocity = Vector2.Zero;
                break;
        }
    MoveAndSlide();
    UpdateAnimation();
    }
    private void HandleMovementAndInput()
    {
        Vector2 inputDirection = Input.GetVector("ui_left","ui_right","ui_up","ui_down");
        if (Input.IsActionJustPressed("ui_accept"))
        {
            _currentState = State.Attack;
            Velocity = Vector2.Zero;
            return;
        }

        if(inputDirection != Vector2.Zero)
        {
            _currentState = State.Run;
            Velocity = inputDirection * speed;
            SetFacingDirection(inputDirection);
        }
        else
        {
            _currentState = State.Idle;
            Velocity = Vector2.Zero;
        }
        
    }

    private void SetFacingDirection(Vector2 direction)
    {
        // Determines primary direction based on movement vector
        if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
        {
            _facingDirection = direction.X > 0 ? "Right" : "Left";
        }
        else
        {
            _facingDirection = direction.Y > 0 ? "Down" : "Up";
        }
    }

private void UpdateAnimation()
    {
        if (Sprite == null) return;

        // Construct animation string name dynamically (e.g. "run_left", "idle_down")
        string statePrefix = _currentState switch
        {
            State.Run => "run",
            State.Attack => "attack",
            _ => "idle"
        };

        string animName = $"{statePrefix}_{_facingDirection}";

        // Only call Play() if it's not already playing to avoid resetting the frame counter
        if (Sprite.Animation != animName)
        {
            Sprite.Play(animName);
        }
    }

    private void OnAnimationFinished()
    {
        // When the non-looping attack animation finishes, return to Idle state
        if (_currentState == State.Attack)
        {
            _currentState = State.Idle;
        }
    }


}
