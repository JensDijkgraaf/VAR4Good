using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField, Tooltip("Starting score for the player"), Range(0, 100)]
    private int score = 100;

    [SerializeField, Tooltip("Decrease in score for each tree hit"), Range(0, 10)]
    private int treeScoreDecrease = 1;

    private List<string> actions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TreeHit()
    {
        score -= treeScoreDecrease;
        TrackAction($"Tree chopped down -{treeScoreDecrease} points");
    }

    // Add this method to track actions
    public void TrackAction(string action)
    {
        actions.Add(action);
    }

    // Add a method to get the list of actions if needed
    public List<string> GetActions()
    {
        return actions;
    }

    // Add a method to get the current score if needed
    public int GetScore()
    {
        return score;
    }
}
