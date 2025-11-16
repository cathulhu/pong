using Godot;
using System;

public partial class Ball : RigidBody2D
{
	[Export] public GameScene gameScene;
	private Vector2 _lastVelocity;
	public static float startingBallSpeed = 500;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LinearVelocity = new Vector2(-startingBallSpeed, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		// save last velocity for use in calculations
		_lastVelocity = LinearVelocity;

		if (LinearVelocity.X > 0)
		{
			LinearVelocity += new Vector2(0, (float)delta * AngularVelocity * 10);
		}
		else
		{
			LinearVelocity += new Vector2(0, (float)delta * AngularVelocity * -10);
		}
	}

	// this isn't an override because it's a signal
	public void BodyEntered(Node bodyHit)
	{
		if (bodyHit is Paddle paddle)
		{
			var velocityDifference = Position.Y - paddle.Position.Y;
			LinearVelocity = _lastVelocity * new Vector2(-1, 1);
			
			// add extra velocity to ball based on how far from the center of paddle the the ball hits
			LinearVelocity += new Vector2(0,velocityDifference);
			
			// add they component of the paddle to the y component of the ball velocity
			LinearVelocity += paddle.lastVector;
		}
		else if (bodyHit is Bumper)
		{
			LinearVelocity = _lastVelocity* new Vector2(1, -1);
		}
		else if (bodyHit is ScoreArea scoreZone)
		{
			if (scoreZone.rightScore)
			{
				gameScene.registerScore("left");
			}
			else
			{
				gameScene.registerScore("right");
			}
		}
	}
}
