using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class DefaultButton : MonoBehaviour
{
	public Slider MoveSpeedSlider;
	public Slider TurnSpeedSlider;
	public TMP_Dropdown TurnTypeDropdown;
	public TMP_Dropdown SnapAngleDropdown;
	public TMP_Dropdown MovementDropdown;

    private float _MoveSpeedSliderValue;
	private float _TurnSpeedSliderValue;
	private int _TMP_DropdownValue;
	private int _SnapAngleDropdownValue;
	private int _MovementDropdownValue;

	void Start()
	{
		// Store the default values when the script starts
		_MoveSpeedSliderValue = MoveSpeedSlider.value;
		_TurnSpeedSliderValue = TurnSpeedSlider.value;
		_TMP_DropdownValue = TurnTypeDropdown.value;
		_SnapAngleDropdownValue = SnapAngleDropdown.value;
		_MovementDropdownValue = MovementDropdown.value;

    }

	public void SetDefault()
	{
		// Reset the sliders to their default values
		MoveSpeedSlider.value = _MoveSpeedSliderValue;
		TurnSpeedSlider.value = _TurnSpeedSliderValue;

		// Reset the TMP dropdown menus to their default values
		TurnTypeDropdown.value = _TMP_DropdownValue;
		SnapAngleDropdown.value = _SnapAngleDropdownValue;
        MovementDropdown.value = _MovementDropdownValue;
    }
}