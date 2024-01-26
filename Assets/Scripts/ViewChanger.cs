using UnityEngine;
using System.Collections;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ViewChanger : MonoBehaviour
{
    private float fadeDuration = 10f;
    public Color fadeColor;
    [SerializeField] public Camera mainCamera;
    private new Renderer renderer;
    private DateTime currentTime;
    private DateTime endOfDay;

    private ActionBasedContinuousMoveProvider actionBasedContinuousMoveProvider;
    private ActionBasedContinuousTurnProvider actionBasedContinuousTurnProvider;
    private CharacterController characterController;
    private JumpController jumpController;
    private GameObject wristCanvas;

    private OffboardingSummary offboardingSummary;


    private void Start()
    {
        renderer = mainCamera.transform.GetChild(0).GetComponent<Renderer>();

        offboardingSummary = GameObject.Find("Offboarding").GetComponent<OffboardingSummary>();

        //Player
        actionBasedContinuousMoveProvider = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousMoveProvider>();
        actionBasedContinuousTurnProvider = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousTurnProvider>();
        characterController = transform.GetComponent<CharacterController>();
        jumpController = transform.GetComponent<JumpController>();
        wristCanvas = GameObject.Find("WristCanvas");
    }

    private void FreezePlayer()
    {
        actionBasedContinuousMoveProvider.enabled = false;
        actionBasedContinuousTurnProvider.enabled = false;
        characterController.enabled = false;
        jumpController.enabled = false;
    }

    private void UnFreezePlayer()
    {
        actionBasedContinuousMoveProvider.enabled = true;
        actionBasedContinuousTurnProvider.enabled = true;
        characterController.enabled = true;
        jumpController.enabled = true;
    }

    private void TransitionToOverheadSubtask()
    {
        ShowOverheadView();

        // Freeze the player position.
        FreezePlayer();

        wristCanvas.SetActive(false);

        offboardingSummary.ShowSummary();

        FadeOutTopRend();
    }

    // Switches the camera to the overhead one, with a fade in/out.
    public void TransitionToOverhead()
    {
        Fade(0, 1, this.TransitionToOverheadSubtask);
    }

    // public void TransitionToGround()
    // {
    //     Fade(1, 0,topRend, this.ShowMainCamera);
    // }

    private void FadeOutTopRend()
    {
        Fade(1, 0);
    }


    private void Fade(float alphaIn,float alphaOut,Action endCall = null)
    {
        StartCoroutine(FadeRoutine(alphaIn,alphaOut,endCall));
    }
    public IEnumerator FadeRoutine(float alphaIn,float alphaOut,Action endCall = null)
    {
        float time = 0;
        while(time <=  fadeDuration){

            var color = fadeColor;
            color.a = Mathf.Lerp(alphaIn, alphaOut, Mathf.Clamp01(time / fadeDuration));

            renderer.material.SetColor("_Color",color);
            time += Time.deltaTime;
            yield return null;
        }
        var color2 = fadeColor;
        color2.a = alphaOut;
        renderer.material.SetColor("_Color",color2) ;
        if(endCall != null)
            endCall();
    }
    
    public void ShowOverheadView()
    {
        transform.position = new Vector3((float)44.32, (float)36.22, (float)18.13);
        transform.eulerAngles = new Vector3(30, -120, 0);
    }

    public void ShowGroundView()
    {
        transform.position = new Vector3((float)12.83, (float)0.0, (float)33.97);
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
}