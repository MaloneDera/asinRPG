using Godot;
using System;
using System.Formats.Tar;

public partial class Player2d : CharacterBody2D
{
    [Export] public float Speed {get; set;} = 100.0f;
    [Export] public AnimatedSprite2D Sprite {get; set;}

    private string _facing = "down";
    private bool _isAttacking = false;
    public override void _Ready()
    {
        GD.Print("Player Loaded");
        if(Sprite != null)
        {
            Sprite.AnimationFinished += () =>  _isAttacking = false;
        }
    }


    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept") && !_isAttacking)
        {

            _isAttacking = true;
            Velocity = Vector2.Zero;
            Sprite.Play("attack_" + _facing);
        }
        if (!_isAttacking)
        {
            Vector2 inputDirection = Input.GetVector("ui_left","ui_right","ui_up","ui_down");
            Velocity = inputDirection * Speed;
            if(inputDirection.X>0) _facing = "right";
            else if (inputDirection.X < 0) _facing = "left";
            else if (inputDirection.Y > 0) _facing = "down";
            else if (inputDirection.Y < 0) _facing = "up";

            if(inputDirection != Vector2.Zero)
            {
                Sprite.Play("run_" + _facing);

            }
            else
            {
                Sprite.Play("idle_"+ _facing);
            }
        }
        MoveAndSlide();
    }


}
