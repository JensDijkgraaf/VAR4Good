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

    [CustomString("Campfire burned 10 seconds too long")]
    CAMPFIRE_BURNED_LONG,

    [CustomString("Campfire set on fire without enough stones")]
    CAMPFIRE_SET_ON_FIRE_WITHOUT_STONES,
}

public class ScoreController : MonoBehaviour
{
    private int startScore = 0;

    private static readonly Dictionary<Actions, int> _offenseWeight = new Dictionary<Actions, int>
    {
        { Actions.TREE_HIT, 1 },
        { Actions.BEE_KILLED, 2 },
        { Actions.TREE_FIRE, 4 },
        { Actions.NOT_LOOKING_FIRE, 2 },
        { Actions.CAMPFIRE_BURNED_LONG, 2 },
        { Actions.CAMPFIRE_SET_ON_FIRE_WITHOUT_STONES, 2 }
    };    private readonly Dictionary<Actions, int> _totalOffenses = new();

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

    public void BeeKilled()
    {
        TrackAction(Actions.BEE_KILLED);
    }

    public void CampfireBurnedLong()
    {
        TrackAction(Actions.CAMPFIRE_BURNED_LONG);
    }

    public void CampfireSetOnFireWithoutStones()
    {
        TrackAction(Actions.CAMPFIRE_SET_ON_FIRE_WITHOUT_STONES);
    }

    public void CampfireSetOnFire()
    {
        startScore += 100;
    }

    private void TrackAction(Actions action)
    {
        int points = _offenseWeight[action];
        if (_totalOffenses.ContainsKey(action))
        {
            _totalOffenses[action] += 1;
        }
        else
        {
            _totalOffenses[action] = 1;
        }
    }

    public string GetSummary()
    {
        var summaryLines = _totalOffenses.Select(entry =>
        {
            string customString = GetCustomString(entry.Key);
            int deduction = entry.Value * _offenseWeight[entry.Key];
            return $"{customString} x{entry.Value}\nDeducted: <b><color=red>{deduction}</color></b> points\n\n";
        }).ToList();

        int totalScore = GetScore();
        summaryLines.Add($"\n<size=30>Total: <b>{totalScore}</b></size>");

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