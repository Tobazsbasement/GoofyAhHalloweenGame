using Godot;
using System;

public partial class Player : CharacterBody3D
{
	private Vector3 InputVector;
	public Vector3 CalculatedVelocity;
	private PlayerCamera Camera;
	private AnimationPlayer AnimPlayer;
	private bool IsSprinting;
	public enum States {
		IDLE,
		MOVING
	}
	public States CurrentState;

	[Export]
	private VelocityComponent VelocityNode;

    public override void _Ready()
    {
        Camera = GetNode<PlayerCamera>("PlayerCamera");
		AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private Vector3 HandleInput(double delta){
		InputVector.X = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");
		InputVector.Z = Input.GetActionStrength("Backwards") - Input.GetActionStrength("Forwards");

		IsSprinting = Input.IsActionPressed("Sprint");

		return InputVector;
	}

    public override void _PhysicsProcess(double delta)
    {
		HandleInput(delta);

		StateMachine(delta);

		MoveAndSlide();
    }

	private void ApplyMovement(double delta){
		if(IsSprinting){
			Velocity = VelocityNode.Sprint(InputVector, delta).Normalized() * Camera.Transform.Basis;

			AnimPlayer.Play("Run");
		}
		else{
			Velocity = VelocityNode.Accelerate(InputVector, delta).Normalized() * Camera.Transform.Basis;
			
			AnimPlayer.Play("Walk");
		}
	}

	private void StateMachine(double delta){
		if(InputVector == Vector3.Zero){
			CurrentState = States.IDLE;
		}
		else if(InputVector != Vector3.Zero){
			CurrentState = States.MOVING;
		}

		switch(CurrentState){
			case States.IDLE:
				Velocity = VelocityNode.ApplyFriction(Velocity);

				AnimPlayer.Play("Idle");

				break;
			
			case States.MOVING:
				ApplyMovement(delta);

				break;
		}
	}
}
