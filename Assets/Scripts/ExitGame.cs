using UnityEngine;
using UnityEngine.UI;

public class QuitConfirmation : MonoBehaviour
{
    public GameObject QuitCanvas;
    public Button InitQuitButton;
    public Button ConfirmButton;
    public Button CancelButton;


    void start()
    {
        InitQuitButton.onClick.AddListener(ShowQuitPopup);
        ConfirmButton.onClick.AddListener(ConfirmQuit);
        CancelButton.onClick.AddListener(CancelQuit);

        QuitCanvas.SetActive(false);
    }

    public void ShowQuitPopup()
    {
        QuitCanvas.SetActive(true);
    }

    public void ConfirmQuit()
    {
        Application.Quit();
    }

    public void CancelQuit()
    {
        QuitCanvas.SetActive(false);
    }
}