using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject continuousTurningUI;
    public GameObject snappingUI;
    public GameObject continuousTurning; // Reference to the continuous movement object in XR rig
    public GameObject snapping; // Reference to the snapping object in XR rig

    private void Start()
    {
        // Subscribe to the OnValueChanged event of the dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        
        // Set the default selection
        OnDropdownValueChanged(dropdown.value);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Deactivate all objects initially
        continuousTurningUI.SetActive(false);
        snappingUI.SetActive(false);

        // Activate the selected object based on the dropdown value
        switch (index)
        {
            case 0: // Continuous Turning
                continuousTurningUI.SetActive(true);
                // Add logic to modify continuous turning settings
                EnableContinuousTurning();
                break;

            case 1: // Snapping
                snappingUI.SetActive(true);
                // Add logic to modify snapping settings
                EnableSnapping();
                break;
        }
    }

    private void EnableContinuousTurning()
    {
        // Enable continuous movement object and disable snapping object
        continuousTurning.SetActive(true);
        snapping.SetActive(false);
    }

    private void EnableSnapping()
    {
        // Enable snapping object and disable continuous movement object
        continuousTurning.SetActive(false);
        snapping.SetActive(true);
    }
}
