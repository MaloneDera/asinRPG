using Godot;
using System;
using System.ComponentModel;

public partial class MainMenu : Control
{
	[Export] public Button StartButton {get; set;}
	[Export] public Button OptionsButton {get; set;}
	[Export] public Button QuitButton {get; set;}
	[Export(PropertyHint.File, "*.tscn")] public string GamePlayScenePath {get;set;} = "res://Scenes/over_world.tscn";



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Main Menu Loaded");
		if(StartButton != null)
		{
			StartButton.Pressed += OnStartButtonPressed;
		}if (OptionsButton != null)
        {
            OptionsButton.Pressed += OnOptionsButtonPressed;
        }

        if (QuitButton != null)
        {
            QuitButton.Pressed += OnQuitButtonPressed;
        }

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnStartButtonPressed()
	{
		GD.Print("Starting Game ... ");
		if (!string.IsNullOrEmpty(GamePlayScenePath))
		{
			GetTree().ChangeSceneToFile(GamePlayScenePath);
		}
		else
		{
			GD.PrintErr("GameplayScenePath is not set in the Inspector");

		}
	}
	private void OnOptionsButtonPressed()
    {
        GD.Print("Options menu clicked!");
        // Logic to open an options popup or transition to an options scene
    }

    private void OnQuitButtonPressed()
    {
        GD.Print("Quitting Game...");

        // Closes the game application gracefully
        GetTree().Quit();
    }
}
