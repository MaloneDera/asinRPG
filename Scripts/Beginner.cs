using Godot;
using System;

public partial class Beginner : Node2D
{
    [Export]
    public float Speed {get; set;} = 200.0f;
    private int _score = 0;

    public override void _Ready()
    {
        GD.Print("Script is initialized! Score : " + _score);
    }
    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            AddScore(20);
        }
    }
    public void AddScore(int amount)
    {
        _score += amount;
        GD.Print("Score Increased! Score : " + _score);
    }


}
