using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuitConfirmation : MonoBehaviour
{
    public GameObject QuitCanvas;
    public Button InitQuitButton;
    public Button ConfirmButton;
    public Button CancelButton;
    private bool endGame = false;
    private TimeController _time;

    private ViewChanger viewChanger;

    void Start()
    {
        InitQuitButton.onClick.AddListener(ShowQuitPopup);
        ConfirmButton.onClick.AddListener(ConfirmQuit);
        CancelButton.onClick.AddListener(CancelQuit);

        QuitCanvas.SetActive(false);

        viewChanger = GameObject.Find("XR Origin (XR Rig)").GetComponent<ViewChanger>();
        _time = GameObject.Find("Sky").GetComponent<TimeController>();

    }


    public void RemoveQuitPopup()
    {
        QuitCanvas.SetActive(false);
    }
    public void ShowQuitPopup()
    {
        QuitCanvas.SetActive(true);
    }

    public void AlterButtonEndGame()
    {
        endGame = true;
        // Change button text
        InitQuitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exit Game";
        // Change popup text
        QuitCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Are you sure you want to exit the game?";
    }
    public void ConfirmQuit()
    {
        if (endGame)
        {
            Application.Quit();
        }
        else
        {
            // We freeze the time
            _time.FreezeTime();

            // We end the game
            RemoveQuitPopup();
            viewChanger.TransitionToOverhead(2f);
            
        }
    }

    public void CancelQuit()
    {
        QuitCanvas.SetActive(false);
    }
}