using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SelectInteractor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private XRSocketInteractor socketInteractor;

    private CampfireController campfireController;
    private bool isStoneSocket = false;

    private bool isLogSocket = false;

    void Start()
    {
        string parentName = transform.parent.name;
        if (parentName.Contains("Stone"))
        {
            isStoneSocket = true;
        }
        if (parentName.Contains("Log"))
        {
            isLogSocket = true;
        }
        campfireController = transform.parent.parent.GetComponent<CampfireController>();
        meshRenderer = GetComponent<MeshRenderer>();
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(HideMaterial);
        socketInteractor.selectExited.AddListener(ShowMaterial);
    }

    private void HideMaterial(SelectEnterEventArgs arg)
    {
        meshRenderer.enabled = false;
        if (isStoneSocket) 
        {
            campfireController.AddStone();
        }
        if (isLogSocket) 
        {
            campfireController.AddLog();
        }
    }

    private void ShowMaterial(SelectExitEventArgs arg)
    {
        meshRenderer.enabled = true;
    }
}
