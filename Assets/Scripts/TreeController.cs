using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private bool _isOnFire = false;

    private float _burningTime = 30f;
    // Needed for spreading fire
    private List<GameObject> neighbours;

    private float fallDuration = 5f;
    private float spreadChance = 0.05f;
    private float elapsedTime = 0f;
    private ParticleSystem particles;

    private AudioSource audioSource;
    [SerializeField, Tooltip("Sound for tree falling")]
    private AudioClip treeFallSound;

    [SerializeField, Tooltip("Sound for fire")]
    private AudioClip fireSound;


    [SerializeField, Tooltip("Prefab for the log")]
    private GameObject logPrefab;
    private ScoreController _scoreController;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        _scoreController = Player.GetComponent<ScoreController>();

        audioSource = GetComponent<AudioSource>();
        neighbours = GetNearbyTrees();

        particles = GetComponent<ParticleSystem>();
        if (particles is not null)
            particles.Stop();

        if (_isOnFire)
            SetOnFire();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOnFire)
        {
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
    }

    private void RandomSpreadFireToNeighbours()
    {
        foreach (var neighbour in neighbours)
        {
            if (Random.Range(0f, 1f) <= spreadChance)
                neighbour.GetComponent<TreeController>().SetOnFire();
        }
    }

    public void SetOnFire()
    {
        if (!_isOnFire)
        {
            _isOnFire = true;
            StartCoroutine(BurnTree());
        }
    }
    private List<GameObject> GetNearbyTrees(float radius = 5.0f)
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

    private IEnumerator BurnTree()
    {
        _scoreController.TreeOnFire();
        // Display the fire animation
        particles.Play();

        yield return new WaitForSeconds(_burningTime);
        StartCoroutine(FallTree(false));
    }

    // Coroutine
    public IEnumerator FallTree(bool shouldSpawnLogs = true)
    {
        // Play the tree falling sound
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
        if (shouldSpawnLogs)
            SpawnLogs(transform.position);

        // Destroy the tree
        Destroy(this.gameObject);
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
