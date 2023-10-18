using Godot;
using System;

public partial class VelocityComponent : Node
{
	[Export]
	public int MaxSpeed;
	[Export]
	public int Acceleration;
	[Export]
	public int Friction;
	[Export]
	public int SprintMax;
	[Export]
	public int SprintAcceleration;

	public Vector3 Accelerate(Vector3 Velocity, double delta){
		Velocity.X *= Acceleration * (float)delta;
		Velocity.Z *= Acceleration * (float)delta;

		Velocity.X = Mathf.Clamp(Velocity.X, -MaxSpeed, MaxSpeed);
		Velocity.Z = Mathf.Clamp(Velocity.Z, -MaxSpeed, MaxSpeed);
		
		return Velocity;
	}

	public Vector3 Sprint(Vector3 Velocity, double delta){
		Velocity.X *= SprintAcceleration * (float)delta;
		Velocity.Z *= SprintAcceleration * (float)delta;

		Velocity.X = Mathf.Clamp(Velocity.X, -SprintMax, SprintMax);
		Velocity.Z = Mathf.Clamp(Velocity.Z, -SprintMax, SprintMax);

		return Velocity;

	}
	public Vector3 ApplyFriction(Vector3 Velocity){
		Velocity.X = Mathf.MoveToward(Velocity.X, 0, Friction);
		Velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Friction);

		return Velocity;
	}
	
}
