using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    private Transform waterChild;
    private Transform sandChild;

    [SerializeField, Tooltip("Sound for bucket filling with sand")]
    private AudioClip sandSound;

    [SerializeField, Tooltip("Sound for bucket filling with water")]
    private AudioClip waterSound;

    [SerializeField, Tooltip("Sound for emptying waterbucket")]
    private AudioClip emptyWaterSound;

    [SerializeField, Tooltip("Sound for emptying sandbucket")]
    private AudioClip emptySandSound;

    private AudioSource audioSource;

    private ParticleSystem particles;

    private Renderer particleRenderer;

    [SerializeField, Tooltip("Material for water")]
    private Material waterMaterial;

    [SerializeField, Tooltip("Material for sand")]
    private Material sandMaterial;

    private float bucketCampfireRadius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        waterChild = transform.Find("Water");
        sandChild = transform.Find("Sand");
        particles = GetComponent<ParticleSystem>();
        particleRenderer = particles.GetComponent<Renderer>();
        particles.Stop();
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckBucketOrientation();
    }

    private void CheckBucketOrientation()
    {
        float upsideDownThreshold = 140f;

        // Calculate the angle between the world up vector and the local up vector of the bucket
        float angle = Vector3.Angle(Vector3.up, transform.up);

        if (angle > upsideDownThreshold)
        {
            // Deactivate child objects when held upside down
            if (waterChild.gameObject.activeSelf)
            {
                LetWaterFall();

                // Check for Campfire
                var colliders = Physics.OverlapSphere(transform.position, bucketCampfireRadius);
                foreach (Collider collider in colliders)
                {
                    var campfire = collider.GetComponent<CampfireController>();
                    if (campfire != null && campfire.isOnFire)
                    {
                        campfire.AddWater();
                        // Set the bucket back to empty, so disable waterchild
                        waterChild.gameObject.SetActive(false);
                        audioSource.PlayOneShot(emptyWaterSound);
                    }       
                }
                
                colliders = Physics.OverlapSphere(transform.position, bucketCampfireRadius);
                foreach (Collider collider in colliders)
                {
                    var tree = collider.GetComponent<TreeController>();
                    if (tree != null && tree._isOnFire)
                    {
                        tree.AddWater();
                        // Set the bucket back to empty, so disable waterchild
                        waterChild.gameObject.SetActive(false);
                        audioSource.PlayOneShot(emptyWaterSound);
                    }       
                }
            }
            else if (sandChild.gameObject.activeSelf)
            {
                LetSandFall();
                var colliders = Physics.OverlapSphere(transform.position, bucketCampfireRadius);
                foreach (Collider collider in colliders)
                {
                    var campfire = collider.GetComponent<CampfireController>();
                    if (campfire != null && campfire.isOnFire)
                    {
                        campfire.AddSand();
                        // Set the bucket back to empty, so disable sandchild
                        sandChild.gameObject.SetActive(false);
                        audioSource.PlayOneShot(emptySandSound);
                    }       
                }
            }
        }
    }

    private void FillBucketSand()
    {
        // Check if the bucket is not already filled with sand
        if (!sandChild.gameObject.activeSelf)
        {
            sandChild.gameObject.SetActive(true);
            waterChild.gameObject.SetActive(false);
            audioSource.PlayOneShot(sandSound);
        }
    }
    private void FillBucketWater()
    {
        // Check if the bucket is not already filled with water
        if (!waterChild.gameObject.activeSelf)
        {
            waterChild.gameObject.SetActive(true);
            sandChild.gameObject.SetActive(false);
            audioSource.PlayOneShot(waterSound);
        }
    }
    private void LetWaterFall()
    {
        particleRenderer.material = waterMaterial;
        particles.Play();
        audioSource.PlayOneShot(waterSound);
        waterChild.gameObject.SetActive(false); 
    }
    
    private void LetSandFall()
    {
        particleRenderer.material = sandMaterial;
        particles.Play();
        audioSource.PlayOneShot(sandSound);
        sandChild.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Water"))
        {
            FillBucketWater();
        }
        else if (other.name.Contains("Sand"))
        {
            FillBucketSand();
        }
    }
}