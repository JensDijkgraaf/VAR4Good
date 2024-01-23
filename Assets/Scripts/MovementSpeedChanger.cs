using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class MoveSpeedAdjuster : MonoBehaviour
{
	public Slider MoveSpeedSlider;
	public ContinuousMoveProviderBase MoveScript;

	void Start()
	{
		// Subscribe to the OnValueChanged event of the slider
		MoveSpeedSlider.onValueChanged.AddListener(OnMoveSpeedSliderValueChanged);

		// Set the default speed
		OnMoveSpeedSliderValueChanged(MoveSpeedSlider.value);
	}

	private void OnMoveSpeedSliderValueChanged(float value)
	{
		// Adjust the look speed based on the slider value
		AdjustMoveSpeed(value);
	}

	private void AdjustMoveSpeed(float newSpeed)
	{
		// Set the new look speed in your joystick script
		MoveScript.moveSpeed = newSpeed;
	}
}