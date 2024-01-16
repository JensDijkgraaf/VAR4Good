using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StoneInteractor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private XRSocketInteractor socketInteractor;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(HideMaterial);
        socketInteractor.selectExited.AddListener(ShowMaterial);
    }

    private void HideMaterial(SelectEnterEventArgs arg)
    {
        meshRenderer.enabled = false;
    }

    private void ShowMaterial(SelectExitEventArgs arg)
    {
        meshRenderer.enabled = true;
    }
}
