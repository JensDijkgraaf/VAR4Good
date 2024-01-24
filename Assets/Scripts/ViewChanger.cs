using UnityEngine;
using System.Collections;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ViewChanger : MonoBehaviour
{
    private float fadeDuration = 10f;
    public Color fadeColor;
    [SerializeField] public Camera mainCamera;
    [SerializeField] public Camera overheadCamera;
    private Renderer bottomRend;
    private Renderer topRend;
    private DateTime currentTime;
    private DateTime endOfDay;

    private ActionBasedContinuousMoveProvider actionBasedContinuousMoveProvider;
    private ActionBasedContinuousTurnProvider actionBasedContinuousTurnProvider;


    private void Start()
    {
        bottomRend  = mainCamera.transform.GetChild(0).GetComponent<Renderer>();
        topRend = overheadCamera.transform.GetChild(0).GetComponent<Renderer>();

        //Player
        actionBasedContinuousMoveProvider = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousMoveProvider>();
        actionBasedContinuousTurnProvider = GameObject.Find("XR Origin (XR Rig)").GetComponent<ActionBasedContinuousTurnProvider>();

    }

    private void FreezePlayer()
    {
        actionBasedContinuousMoveProvider.enabled = false;
        actionBasedContinuousTurnProvider.enabled = false;
    }

    private void UnFreezePlayer()
    {
        actionBasedContinuousMoveProvider.enabled = true;
        actionBasedContinuousTurnProvider.enabled = true;
    }

    private void TransitionToOverheadSubtask()
    {
        ShowOverheadView();

        // Freeze the player position.
        FreezePlayer();

        FadeOutTopRend();
    }

    // Switches the camera to the overhead one, with a fade in/out.
    public void TransitionToOverhead()
    {
        Fade(0, 1, bottomRend, this.TransitionToOverheadSubtask);
    }

    // public void TransitionToGround()
    // {
    //     Fade(1, 0,topRend, this.ShowMainCamera);
    // }

    private void FadeOutTopRend()
    {
        Fade(1, 0,topRend);
    }


    private void Fade(float alphaIn,float alphaOut, Renderer rend, Action endCall = null)
    {
        StartCoroutine(FadeRoutine(alphaIn,alphaOut,rend, endCall));
    }
    public IEnumerator FadeRoutine(float alphaIn,float alphaOut,Renderer rend, Action endCall = null)
    {
        float time = 0;
        while(time <=  fadeDuration){

            var color = fadeColor;
            color.a = Mathf.Lerp(alphaIn, alphaOut, Mathf.Clamp01(time / fadeDuration));

            rend.material.SetColor("_Color",color);
            time += Time.deltaTime;
            yield return null;
        }
        var color2 = fadeColor;
        color2.a = alphaOut;
        rend.material.SetColor("_Color",color2) ;
        if(endCall != null)
            endCall();
    }
    
    public void ShowOverheadView()
    {
        mainCamera.enabled = false;
        overheadCamera.enabled = true;
    }

    public void ShowMainCamera()
    {
        mainCamera.enabled = true;
        overheadCamera.enabled = false;
    }
}