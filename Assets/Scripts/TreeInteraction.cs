using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInteraction : MonoBehaviour
{
    [SerializeField, Tooltip("The amount of times the tree has already been hit"), Range(0, 5)]
    private int hitCount = 0;

    private float fallDuration = 5f;

    private float hitCooldown = 0.5f;

    private float lastHitTime;

    [SerializeField, Tooltip("Prefab for the log")]
    private GameObject logPrefab;

    [SerializeField, Tooltip("Sound played when the tree is hit")]
    private AudioClip treeHitSound;

    [SerializeField, Tooltip("Sound played when the tree falls")]
    private AudioClip treeFallSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Axe"))
        {
            if (Time.time - lastHitTime > hitCooldown)
            {
                if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 3)
                {
                    if (hitCount < 5)
                    {
                        hitCount++;

                        if (treeHitSound != null && audioSource != null)
                        {
                            audioSource.PlayOneShot(treeHitSound);
                        }
                        if (hitCount == 5)
                        {
                            StartCoroutine(FallTree());
                        }
                    }
                }
            }
        }
    }

    private IEnumerator FallTree()
    {
        if (treeFallSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(treeFallSound);
        }
        float rotationSpeedX = 90f / fallDuration;
        float rotationSpeedZ = 90f / fallDuration;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(startRotation.eulerAngles.x + 90f, startRotation.eulerAngles.y, startRotation.eulerAngles.z + 90f);

        float elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;

        // Spawn logs after the tree falls
        SpawnLogs();
        
        // Destroy the tree
        Destroy(gameObject);
    }

    private void SpawnLogs()
    {
        // Spawn a random number of logs between 1 and 4
        int logCount = Random.Range(1, 5);

        for (int i = 0; i < logCount; i++)
        {
            // Duplicate the logPrefab
            GameObject newLog = Instantiate(logPrefab, transform.position, Quaternion.identity);

            // Adjust the position (you may want to add some offset)
            newLog.transform.position += new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f));

            // Set a unique name for each log
            newLog.name = "Log_" + i;
        }
    }
}
