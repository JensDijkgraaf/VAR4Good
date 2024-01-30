using System.Collections;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _player;
    private GameObject _campfire;

    private ScoreController _scoreController;
    private CampfireController _campfireController;
    private const float PermittedAngle = 45f;
    private const float NSecondsCheck = 1;
    private float time = 0;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _campfire = GameObject.Find("Campfire");
        _campfireController = _campfire.GetComponent<CampfireController>();
        _scoreController = _player.GetComponent<ScoreController>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            time = 0;
            var isLooking = IsLookingInCampfireDirection();
            if (!isLooking && _campfireController.isOnFire)
            {
                _scoreController.NotLookingAtFire();
            }
        }
    }

    private bool IsLookingInCampfireDirection()
    {
        // Get the direction from the player to the campfire
        var playerToCampfireDirection = _campfire.transform.position - _player.transform.position;

        // Get the angle between the player's forward direction and the player-to-campfire direction
        float angle = Vector3.Angle(_player.transform.forward, playerToCampfireDirection);

        return angle >= PermittedAngle;
    }
}