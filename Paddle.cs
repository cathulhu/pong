using Godot;
using System;

public partial class Paddle : CharacterBody2D
{
	[Export] public Boolean RightPaddle;
	public int paddelSpeed = 200;
	public Vector2 lastVector;
	private float _Xposition;

	public float lastVelocity;
	// // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Xposition = this.Position.X;
	}
	//
	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double timeDelta)
	{
		var isUp = false;
		lastVector = new Vector2(0, 0);
		
		//curviture simulation
		
		if (Input.IsKeyPressed(Key.W) && !RightPaddle)
		{
			MoveAndCollide(Vector2.Up * (float)timeDelta * paddelSpeed);
			lastVector = new Vector2( 0, -paddelSpeed);
		}
		
		if (Input.IsKeyPressed(Key.S) && !RightPaddle)
		{
			MoveAndCollide(Vector2.Down * (float)timeDelta * paddelSpeed);
			lastVector = new Vector2( 0, paddelSpeed);
		}
		
		if (Input.IsKeyPressed(Key.Up) && RightPaddle)
		{
			MoveAndCollide(Vector2.Up * (float)timeDelta * paddelSpeed);
			lastVector = new Vector2( 0, -paddelSpeed);
		}

		if (Input.IsKeyPressed(Key.Down) && RightPaddle)
		{
			MoveAndCollide(Vector2.Down * (float)timeDelta * paddelSpeed);
			lastVector = new Vector2( 0, paddelSpeed);
		}

		this.Position = new Vector2(_Xposition, this.Position.Y);


	}
}
