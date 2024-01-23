using UnityEngine;
using TMPro;

public class DropdownController : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public GameObject continuousTurningUI;
    public GameObject snappingUI;
    public Behaviour continuousTurningScript; // Reference to ContinuousTurning script
    public Behaviour snappingScript; // Reference to Snapping script

    private void Start()
    {
        // Subscribe to the OnValueChanged event of the dropdown
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        
        // Set the default selection
        OnDropdownValueChanged(dropdown.value);

        //continuousTurningScript.enabled = true;
        //snappingScript.enabled = false;
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
        // Enable continuous turning script and disable snapping script
        continuousTurningScript.enabled = true;
        snappingScript.enabled = false;
    }

    private void EnableSnapping()
    {
        // Enable snapping script and disable continuous turning script
        continuousTurningScript.enabled = false;
        snappingScript.enabled = true;
    }
}
