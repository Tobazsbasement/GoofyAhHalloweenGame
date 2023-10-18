using Godot;
using System;
using System.Numerics;

public partial class PlayerCamera : Node3D
{
	private Camera3D Camera;
	[Export]
	public double Sensitivity;
	[Export]
	public double Limits;

    public override void _Ready()
    {
        Camera = GetNode<Camera3D>("Camera3D");
    }

    public override void _Input(InputEvent ev)
    {
		if(ev is InputEventMouseMotion){
			InputEventMouseMotion ent = (InputEventMouseMotion)ev;

			RotateY(- ent.Relative.X * (float)Sensitivity);
			Camera.RotateX(-ent.Relative.Y * (float)Sensitivity);

			Godot.Vector3 CameraRot = Camera.Rotation;

			CameraRot.X = Mathf.Clamp(CameraRot.X, (float)Mathf.DegToRad(-Limits), (float)Mathf.DegToRad(Limits));

			Camera.Rotation = CameraRot;
			
		}
    }
}
