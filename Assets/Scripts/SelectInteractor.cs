using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

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

        XRGrabInteractable grabInteractable = arg.interactable.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            StartCoroutine(DisableGrabInteractable(grabInteractable, arg.interactable.gameObject));
        }

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

    private IEnumerator DisableGrabInteractable(XRGrabInteractable grabInteractable, GameObject obj)
    {
        // Wait for a short duration before disabling XRGrabInteractable
        yield return new WaitForSeconds(0.2f);

        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
            // Make the object kinematic after the delay
            MakeKinematic(obj);
        }
    }

    private void MakeKinematic(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
