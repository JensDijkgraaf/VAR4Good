using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeInteraction : MonoBehaviour
{
    [SerializeField, Tooltip("Sound for tree hit")]
    private AudioClip treeHitSound;


    private GameObject Player;

    private AudioSource audioSource;

    private ScoreController scoreController;

    // Cooldown between hits in seconds
    private float hitCooldown = 0.5f;
    private float lastHitTime;

    private int hitCount = 0;

    private int prevHitId = -1;
    private int currentHitId = -2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player = GameObject.Find("Player");
        scoreController = Player.GetComponent<ScoreController>();

        if (scoreController == null)
        {
            Debug.LogError("ScoreController not found");
            return;
        }
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object's name contains "tree"
        if (other.gameObject.name.Contains("Tree"))
        {
            currentHitId = other.gameObject.GetInstanceID();
            // Check if enough time has passed since the last hit
            if (Time.time - lastHitTime > hitCooldown)
            {
                // Check if the axe's velocity is sufficient
                if (GetComponent<Rigidbody>().velocity.magnitude > 3)
                {
                    if (currentHitId != prevHitId)
                    {
                        hitCount = 0;
                    }
                    prevHitId = currentHitId;
                    // Dead trees are allowed to be chopped down.
                    if (!other.gameObject.name.Contains("Dead"))
                    {
                        scoreController.TreeHit();
                    }

                    // Play the tree hit sound
                    if (treeHitSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(treeHitSound);
                    }
                    hitCount++;
                    if (hitCount == 5)
                    {
                        hitCount = 0;
                        // Trigger tree falling sequence
                        StartCoroutine(other.gameObject.GetComponent<TreeController>().FallTree());
                    }

                    lastHitTime = Time.time;
                }
            }
        }
    }
}
