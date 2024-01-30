using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResetGameConfirmation : MonoBehaviour
{
    public GameObject ResetCanvas;
    public Button InitResetButton;
    public Button ConfirmButton;
    public Button CancelButton;


    void Start()
    {
        InitResetButton.onClick.AddListener(ShowResetPopup);
        ConfirmButton.onClick.AddListener(ConfirmReset);
        CancelButton.onClick.AddListener(CancelReset);
        
        ResetCanvas.SetActive(false);
    }

    public void ShowResetPopup()
    {
        ResetCanvas.SetActive(true);
    }

    public void ConfirmReset()
    {
        // Reload scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void CancelReset()
    {
        ResetCanvas.SetActive(false);
    }
}