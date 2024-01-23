using UnityEngine;
using System.Collections;

public class ViewChanger : MonoBehaviour
{
    private float fadeDuration = 10f;
    public Color fadeColor;
    [SerializeField] public Camera mainCamera;
    [SerializeField] public Camera overheadCamera;

    private Renderer rend;

    private void Start(){
        rend = GetComponent<Renderer>();
        TransitionToOverhead();
    }

    public void TransitionToOverhead(){
        FadeIn();
    }

    public void FadeIn(){
        Fade(0,1);
    }

    public void FadeOut(){
        Fade(1,0);
    }
    private void Fade(float alphaIn,float alphaOut){
        StartCoroutine(FadeRoutine(alphaIn,alphaOut));
    }
    public IEnumerator FadeRoutine(float alphaIn,float alphaOut){
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
        ShowOverheadView();

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