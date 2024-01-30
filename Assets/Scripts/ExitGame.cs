using UnityEngine;
using UnityEngine.UI;

public class QuitConfirmation : MonoBehaviour
{
    public GameObject QuitCanvas;
    public Button InitQuitButton;
    public Button ConfirmButton;
    public Button CancelButton;

    private ViewChanger viewChanger;

    void start()
    {
        InitQuitButton.onClick.AddListener(ShowQuitPopup);
        ConfirmButton.onClick.AddListener(ConfirmQuit);
        CancelButton.onClick.AddListener(CancelQuit);

        QuitCanvas.SetActive(false);
        
        viewChanger = GameObject.Find("XR Origin (XR Rig)").GetComponent<ViewChanger>();
    }

    public void ShowQuitPopup()
    {
        QuitCanvas.SetActive(true);
    }

    public void ConfirmQuit()
    {
        // We end the game
       viewChanger.TransitionToOverhead(2f); 
    }

    public void CancelQuit()
    {
        QuitCanvas.SetActive(false);
    }
}