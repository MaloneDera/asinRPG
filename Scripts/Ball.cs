using Godot;
using System;

public partial class Ball : CharacterBody2D
{   
    [Export] public float speed {get; set;} = 5.0f;
    [Export] public float acceleration {get; set;} = 20.0f;
    [Export] public float friction {get; set;} = 20.0f;
    public override void _Ready()
    {
       GD.Print("ball version 2 is loaded");
    }
    public override void _Process(double delta)
    {   float deltaFLoat = (float)delta;
        
        Vector2 getDirection = Input.GetVector("ui_right","ui_left","ui_up","ui_down");
        Vector2 targetVelocity = Velocity;
        if(getDirection != Vector2.Zero)
        {
            targetVelocity += targetVelocity.MoveToward(getDirection*speed,acceleration*deltaFLoat);
        }
        else
        {
            targetVelocity += targetVelocity.MoveToward(Vector2.Zero, friction * deltaFLoat);
        }
        Velocity = targetVelocity;
        MoveAndSlide();
    }


}
