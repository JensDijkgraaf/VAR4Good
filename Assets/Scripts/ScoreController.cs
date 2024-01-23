using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CustomStringAttribute : System.Attribute
{
    public readonly string Value;

    public CustomStringAttribute(string value)
    {
        Value = value;
    }
}
public enum Actions
{
    [CustomString("Tree hit")]
    TREE_HIT,

    [CustomString("Set tree on fire")]
    TREE_FIRE,
     
    [CustomString("Bee killed")]
    BEE_KILLED,
    
    [CustomString("Not keeping an eye on the fire")]
    NOT_LOOKING_FIRE,
}

public class ScoreController : MonoBehaviour
{
    [SerializeField, Tooltip("Starting score for the player"), Range(0, 100)]
    private int startScore = 100;

    private readonly Dictionary<Actions, int> _offenseWeight = new();
    private readonly Dictionary<Actions, int> _totalOffenses = new();

    // Start is called before the first frame update
    private void Start()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        _offenseWeight.Add(Actions.TREE_HIT, 1);
        _offenseWeight.Add(Actions.BEE_KILLED, 2);
        _offenseWeight.Add(Actions.TREE_FIRE, 4);
        _offenseWeight.Add(Actions.NOT_LOOKING_FIRE, 2);
    }

    // Public methods
    public void TreeHit()
    {
        TrackAction(Actions.TREE_HIT);
    }

    public void TreeOnFire()
    {
        TrackAction(Actions.TREE_FIRE);

    }
    
    public void NotLookingAtFire()
    {
        TrackAction(Actions.NOT_LOOKING_FIRE);

    }

    private void TrackAction(Actions action)
    {
        int points = _offenseWeight[action];
        if (_totalOffenses.ContainsKey(action))
        {
            _totalOffenses[action] += points;
        }
        else
        {
            _totalOffenses[action] = points;
        }
    }

    public string GetSummary()
    {
        var summaryLines = _totalOffenses.Select(entry =>
        {
            string customString = GetCustomString(entry.Key);
            int deduction = entry.Value * _offenseWeight[entry.Key];
            return $"{customString} x{entry.Value}\nDeducted: {deduction} points";
        }).ToList();

        int totalScore = GetScore();
        summaryLines.Add($"\nTotal: {totalScore}");

        return string.Join("\n", summaryLines);

    }

    private string GetCustomString(Actions action)
    {
        var field = typeof(Actions).GetField(action.ToString());
        var attribute = (CustomStringAttribute)Attribute.GetCustomAttribute(field, typeof(CustomStringAttribute));

        return attribute != null ? attribute.Value : action.ToString();
    } 
    public int GetScore()
    {
        return _totalOffenses.Aggregate(startScore,
            (current, entry) => current - entry.Value * _offenseWeight[entry.Key]);
    }
}