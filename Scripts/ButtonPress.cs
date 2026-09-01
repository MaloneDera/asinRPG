using Godot;
using System;

public partial class ButtonPress : Node2D
{
    public override void _Ready()
    {
        GD.Print("node is loaded");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            GD.Print("space is just Pressed ");

        }
        else if (Input.IsActionPressed("ui_accept"))
        {
            GD.Print("space is being Pressed");
        }
        else if (Input.IsActionJustReleased("ui_accept"))
        {
            GD.Print("space is just released");
        }
    }


}
