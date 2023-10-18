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

	public Vector3 Accelerate(Vector3 Velocity, double delta){
		Velocity.X *= Acceleration * (float)delta;
		Velocity.Z *= Acceleration * (float)delta;

		Velocity.X = Mathf.Clamp(Velocity.X, -MaxSpeed, MaxSpeed);
		Velocity.Z = Mathf.Clamp(Velocity.Z, -MaxSpeed, MaxSpeed);
		
		return Velocity;
	}

	public Vector3 ApplyFriction(Vector3 Velocity){
		Velocity.X = Mathf.Lerp(Velocity.X, 0, Friction);
		Velocity.Z = Mathf.Lerp(Velocity.Z, 0, Friction);

		return Velocity;
	}
	
}
