using Godot;
using System;

public partial class GameScene : Node2D
{
	public int ScoreLeft = 0;
	public int ScoreRight = 0;
	[Export] public Label ScoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		resetBall();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}
