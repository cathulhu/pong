using Godot;
using System;
using System.Diagnostics;
using System.Threading;

public partial class GameScene : Node2D
{
	public bool shopOpen = false;
	public int ScoreLeft = 0;
	public int ScoreRight = 0;
	[Export] public Label ScoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Allows us to process inputs even while paused in _Input()
		ProcessMode = ProcessModeEnum.Always;
		resetBall();
		GetTree().Paused = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.Escape)
		{
			TogglePauseGame();
		}

		if (@event is InputEventKey && ((InputEventKey)@event).Pressed && ((InputEventKey)@event).Keycode == Key.P)
		{
			ToggleShopMenu();
		}
	}

	public void ToggleShopMenu()
	{
		if (!shopOpen)
		{
			shopOpen = true;
			var interfaceController = GetNode<Control>("interfaceController");
			var shopScene = GD.Load<PackedScene>("res://shop.tscn");
			var sceneInstance = shopScene.Instantiate<Control>();
			interfaceController.AddChild(sceneInstance);
		}
		else
		{
			var controlToRemove = GetNode<Control>("interfaceController/shopControl");
			controlToRemove.QueueFree();
			shopOpen = false;
		}
	}

	public void resetBall()
	{
		RigidBody2D ball = GetNode<RigidBody2D>("./ball");
		PhysicsServer2D.BodySetState(ball.GetRid(), PhysicsServer2D.BodyState.Transform, new Transform2D(0, new Vector2(580,300)));
		
		// here when the ball goes to the middle it has a 50/50 chance to move left or right
		if (GD.Randf() <= 0.5)
		{
			ball.LinearVelocity = new Vector2(-Ball.startingBallSpeed, 0);
		}
		else
		{
			ball.LinearVelocity = new Vector2(Ball.startingBallSpeed, 0);
		}
		
		
	}

	public void registerScore(string side)
	{
		if (side == "right")
		{
			ScoreRight++;
			GD.Print(ScoreRight);
		}
		else
		{
			ScoreLeft++;
			GD.Print(ScoreLeft);
		}
		ScoreLabel.Text = $"{ScoreLeft} - {ScoreRight}";
		resetBall();
	}

	public void TogglePauseGame()
	{
		GetTree().Paused = !GetTree().Paused;
		var interfaceController = GetNode<Control>("interfaceController");
		
		if (GetTree().Paused)
		{
			var pauseMenuScene = GD.Load<PackedScene>("res://main_menu.tscn");
			var pauseMenuInstance = pauseMenuScene.Instantiate();
			interfaceController.AddChild(pauseMenuInstance);
		}
		else
		{
			var controlToRemove = GetNode<Control>("interfaceController/menuControlRoot");

			if (controlToRemove != null)
			{
				controlToRemove.QueueFree();
			}
		}
	}
}
