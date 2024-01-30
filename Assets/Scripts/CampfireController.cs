using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SocialPlatforms.Impl;

public class CampfireController : MonoBehaviour
{
    private int logCount = 0;
    private int stoneCount = 0;
    private ParticleSystem particles;
    public bool isOnFire = false;

    private float elapsedTime = 0f;
    private float spreadChance = 0.05f;
    
    private bool isTouchingMatch = false;

    private float touchStartTime;

    private GameObject collidedMatch = null;

    private List<GameObject> neighbours;

    [SerializeField, Tooltip("Sound for campfire")]
    private AudioClip campfireSound;

    private AudioSource audioSource;

    private GameObject _player;

    private GameObject _sky;

    private ScoreController _scoreController;

    private TimeController _timeController;

    private float _maxAllowedTime;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
        audioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player");
        _scoreController = _player.GetComponent<ScoreController>();
        _sky = GameObject.Find("Sky");
        _timeController = _sky.GetComponent<TimeController>();
        _maxAllowedTime = _timeController.minutesInDay * 20f;

        if (isOnFire)
        {
            setOnFire();
        }
    }

    void Update()
    {
        // Check if the match has been held against the campfire for 2 seconds or more
        if (isTouchingMatch && Time.time - touchStartTime >= 2f
             && collidedMatch != null && collidedMatch.GetComponent<MatchController>().isOnFire
             && !isOnFire)
        {
            setOnFire();
            collidedMatch = null;
            touchStartTime = 0f;
        }
        if (isOnFire)
        {
            var main = particles.main;
            main.startSize = 1f + logCount * 4f;

            // Use time.deltaTime to count seconds
            elapsedTime += Time.deltaTime;

            // Check if a second has passed
            if (elapsedTime >= 1f)
            {
                elapsedTime = 0f; // Reset the timer
                // Spread fire to neighbours
                RandomSpreadFireToNeighbours();
            }
        }
        // Extra check to make sure the campfire is put out when there are no logs left
        if (logCount == 0 && isOnFire)
        {
            putFireOut();
        }
    }

    public void setOnFire()
    {
        // If we already are on fire, we don't do anything.
        if (isOnFire)
           return;

        for (int i = 0; i < 8 - stoneCount; i++)
        {
            _scoreController.CampfireSetOnFireWithoutStones();
        }

        isOnFire = true;
        var main = particles.main;
        main.loop = true;
        particles.Play();
        audioSource.clip = campfireSound;
        audioSource.loop = true;
        audioSource.Play();
        _scoreController.CampfireSetOnFire();
        Invoke("CampfireBurnedLong", _maxAllowedTime);
        StartFireTimer();
    }

    private void CampfireBurnedLong()
    {
        if (isOnFire)
        {
            Invoke("CampfireBurnedLong", 10f);
            _scoreController.CampfireBurnedLong();
        }
    }
    public void removeLogsOverTime()
    {
        logCount--;
        neighbours = GetNearbyTrees(1.5f*logCount);
        if (logCount >= 2)
        {
            Invoke("removeLogsOverTime", 10f);
        }
    }

    public void removeLog(bool isSand = false)
    {
        if (logCount == 1 && isSand)
        {
            logCount--;
            putFireOut();
        }
        else if (logCount > 1)
        {
            logCount--;
        }
        neighbours = GetNearbyTrees(1.5f*logCount);
    }

    public void putFireOut()
    {
        isOnFire = false;
        particles.Stop();
        audioSource.Stop();
    }

    private void StartFireTimer()
    {
        Invoke("removeLogsOverTime", 10f);
    }

    public void AddStone()
    {
        stoneCount++;
    }

    public void AddLog()
    {
        logCount++;
        neighbours = GetNearbyTrees(1.5f*logCount);
    }

    public void AddSand()
    {
        removeLog(true);
    }

    public void AddWater()
    {
        removeLog();
    }

    private void RandomSpreadFireToNeighbours()
    {
        foreach (var neighbour in neighbours)
        {
            if (Random.value <= spreadChance)
            {
                neighbour.GetComponent<TreeController>().SetOnFire();
            }
        }
    }

    private List<GameObject> GetNearbyTrees(float radius)
    {
        List<GameObject> nearbyTrees = new List<GameObject>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.name.Contains("Tree") && collider.gameObject != gameObject)
                nearbyTrees.Add(collider.gameObject);
        }

        return nearbyTrees;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Match") && 
            other.GetComponent<MatchController>().isOnFire && logCount == 5)
        {
            // Start the timer when the match touches the tree
            isTouchingMatch = true;
            touchStartTime = Time.time;
            collidedMatch = other.gameObject;
        }

        if (isOnFire && other.gameObject.name.Contains("Log"))
        {
            AddLog();
            Destroy(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Match"))
        {
            // Reset the timer and references when the match stops touching the tree
            isTouchingMatch = false;
            touchStartTime = 0f;
            collidedMatch = null;
        }
    }
}
