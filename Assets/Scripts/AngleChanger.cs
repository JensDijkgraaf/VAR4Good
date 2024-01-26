using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class AngleChanger : MonoBehaviour
{
    public TMP_Dropdown angleDropdown;
    public SnapTurnProviderBase snappingScript; // Reference to your SnapTurnProvider script

    void Start()
    {
        // Subscribe to the OnValueChanged event of the dropdown
        angleDropdown.onValueChanged.AddListener(OnAngleDropdownValueChanged);
        
        // Set the default selection
        OnAngleDropdownValueChanged(angleDropdown.value);
    }

    private void OnAngleDropdownValueChanged(int index)
    {
        // Get the selected angle based on the dropdown value
        float selectedAngle = 0f; // Default value

        switch (index)
        {
            case 0: // 15 degrees
                selectedAngle = 15f;
                break;

            case 1: // 30 degrees
                selectedAngle = 30f;
                break;

            case 2: // 45 degrees
                selectedAngle = 45f;
                break;
        }

        // Change the turning angle in the SnapTurnProvider script
        ChangeTurningAngle(selectedAngle);
    }

    private void ChangeTurningAngle(float newAngle)
    {
        snappingScript.turnAmount = newAngle;
    }
}