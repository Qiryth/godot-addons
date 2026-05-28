#if TOOLS
using Godot;

namespace FlappyWizard.addons.right_click_pan;

[Tool]
public partial class RightClickPan : EditorPlugin
{
	private bool _rightMouseButtonDown;

	public override bool _Handles(GodotObject @object)
	{
		return true;
	}

	public override bool _ForwardCanvasGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Right } eventMouseButton)
		{
			_rightMouseButtonDown = true;
            SimulateMiddleClick(eventMouseButton.Pressed, eventMouseButton.Position, eventMouseButton.GlobalPosition);
			return eventMouseButton.Pressed;
		}

		if (_rightMouseButtonDown && @event is InputEventMouseMotion motion &&
		    !Input.IsMouseButtonPressed(MouseButton.Right))
		{
            SimulateMiddleClick(false, motion.Position, motion.GlobalPosition);
		}
		
		return false;
	}

	private static void SimulateMiddleClick(bool isPressed, Vector2 position, Vector2 globalPosition)
	{
		Input.ParseInputEvent(new InputEventMouseButton
		{
			ButtonIndex = MouseButton.Middle,
			Pressed = isPressed,
			Position = position,
			GlobalPosition = globalPosition,
		});
	}
}
#endif
