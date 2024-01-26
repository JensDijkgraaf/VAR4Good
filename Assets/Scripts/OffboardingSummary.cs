using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class OffboardingSummary : MonoBehaviour
{
    private ScoreController scoreController;
    [SerializeField] private TextMeshPro offboardingText;

    // Start is called before the first frame update
    void Start()
    {
        scoreController = GameObject.Find("Player").GetComponent<ScoreController>();
    }

    public void ShowSummary()
    {
        string summary = scoreController.GetSummary();
        GameObject screen = transform.gameObject;
        screen.SetActive(true);
        offboardingText.text = summary;
    }
}
