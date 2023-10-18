using Godot;
using System;

public partial class Player : CharacterBody3D
{
	private Vector3 InputVector;
	public Vector3 CalculatedVelocity;
	private PlayerCamera Camera;
	[Export]
	private VelocityComponent VelocityNode;

    public override void _Ready()
    {
        Camera = GetNode<PlayerCamera>("PlayerCamera");
    }
    private Vector3 HandleInput(double delta){
		InputVector.X = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");
		InputVector.Z = Input.GetActionStrength("Backwards") - Input.GetActionStrength("Forwards");

		return InputVector;
	}

    public override void _PhysicsProcess(double delta)
    {
		HandleInput(delta);

		if(InputVector == Vector3.Zero){
			Velocity = Vector3.Zero;
		}
		else{
			Velocity = VelocityNode.Accelerate(InputVector, delta).Normalized() * Camera.Transform.Basis;
		}

		MoveAndSlide();
    }
}
