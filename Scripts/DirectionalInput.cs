using Godot;
using System;
public partial class DirectionalInput : Node2D
{
    public override void _Ready()
    {
        GD.Print("node is loaded");
    }

public override void _PhysicsProcess(double delta)
{
    // Automatically normalizes diagonal movement and handles analog sticks
    Vector2 moveDirection = Input.GetVector(
        "ui_left",  // -X
        "ui_right", // +X
        "ui_up",    // -Y
        "ui_down"   // +Y
    );

GD.Print(moveDirection);
 
}
}
