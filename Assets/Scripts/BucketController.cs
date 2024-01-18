using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    private Transform waterChild;
    private Transform sandChild;
    // Start is called before the first frame update
    void Start()
    {
        waterChild = transform.Find("Water");
        sandChild = transform.Find("Sand");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Water"))
        {
            sandChild.gameObject.SetActive(false);
            waterChild.gameObject.SetActive(true);
        }
        else if (other.name.Contains("Sand"))
        {
            waterChild.gameObject.SetActive(false);
            sandChild.gameObject.SetActive(true);
        }
    }


}
