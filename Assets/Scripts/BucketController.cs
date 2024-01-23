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
    // Start is called before the first frame update
    void Start()
    {
        waterChild = transform.Find("Water");
        sandChild = transform.Find("Sand");
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var campfire = other.GetComponent<CampfireController>();
        if (other.name.Contains("Water"))
        {
            // Check if the bucket is not already filled with water
            if (!waterChild.gameObject.activeSelf)
            {
                waterChild.gameObject.SetActive(true);
                sandChild.gameObject.SetActive(false);
                audioSource.PlayOneShot(waterSound);
            }
        }
        else if (other.name.Contains("Sand"))
        {
            // Check if the bucket is not already filled with sand
            if (!sandChild.gameObject.activeSelf)
            {
                sandChild.gameObject.SetActive(true);
                waterChild.gameObject.SetActive(false);
                audioSource.PlayOneShot(sandSound);
            }
        }
        else if (other.name.Contains("Campfire") && campfire.isOnFire)
        {
            if (waterChild.gameObject.activeSelf)
            {
                campfire.AddWater();
                // Set the bucket back to empty, so disable waterchild
                waterChild.gameObject.SetActive(false);
                audioSource.PlayOneShot(emptyWaterSound);
            }
            else if (sandChild.gameObject.activeSelf)
            {
                campfire.AddSand();
                // Set the bucket back to empty, so disable sandchild
                sandChild.gameObject.SetActive(false);
                audioSource.PlayOneShot(emptySandSound);
            } 
        }
    }


}
