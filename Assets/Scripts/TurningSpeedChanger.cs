using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class LookSpeedAdjuster : MonoBehaviour
{
	public Slider speedSlider;
	public ContinuousTurnProviderBase joystickScript; // Reference to your joystick script

	void Start()
	{
		// Subscribe to the OnValueChanged event of the slider
		speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);

		// Set the default speed
		OnSpeedSliderValueChanged(speedSlider.value);
	}

	private void OnSpeedSliderValueChanged(float value)
	{
		// Adjust the look speed based on the slider value
		AdjustLookSpeed(value);
	}

	private void AdjustLookSpeed(float newSpeed)
	{
		// Set the new look speed in your joystick script
		joystickScript.turnSpeed = newSpeed;
	}
}