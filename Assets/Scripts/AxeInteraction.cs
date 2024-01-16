using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeInteraction : MonoBehaviour
{
    [SerializeField, Tooltip("Sound for tree hit")]
    private AudioClip treeHitSound;

    [SerializeField, Tooltip("Sound for tree falling")]
    private AudioClip treeFallSound;

    [SerializeField, Tooltip("Prefab for the log")]
    private GameObject logPrefab;

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
                    if (currentHitId != prevHitId) {
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
                        StartCoroutine(FallTree(other.gameObject));
                    }

                    lastHitTime = Time.time;
                }
            }
        }
    }

    private IEnumerator FallTree(GameObject tree)
    {
        // Play the tree falling sound
        if (treeFallSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(treeFallSound);
        }

        float fallDuration = 5f;
        float rotationSpeedX = 90f / fallDuration;
        float rotationSpeedZ = 90f / fallDuration;

        Quaternion startRotation = tree.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(startRotation.eulerAngles.x + 90f, startRotation.eulerAngles.y, startRotation.eulerAngles.z + 90f);

        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            tree.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tree.transform.rotation = targetRotation;

        // Spawn logs after the tree falls
        SpawnLogs(tree.transform.position);

        // Destroy the tree

        Destroy(tree);
    }

    private void SpawnLogs(Vector3 spawnPosition)
    {
        // Spawn a random number of logs between 1 and 4
        int logCount = Random.Range(1, 5);

        for (int i = 0; i < logCount; i++)
        {
            // Duplicate the logPrefab
            GameObject newLog = Instantiate(logPrefab, spawnPosition, Quaternion.identity);

            newLog.transform.position = new Vector3(spawnPosition.x + Random.Range(-0.5f, 0.5f), spawnPosition.y + 1, spawnPosition.z + Random.Range(-0.5f, 0.5f));
            float randomRotationY = Random.Range(0f, 360f);
            newLog.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);
        
            // Set a unique name for each log
            newLog.name = "Log_" + i;
        }
    }
}
