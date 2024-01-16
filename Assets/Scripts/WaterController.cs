using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a bucket
        if (other.name.Equals("Bucket"))
        {
            // Enable the "Water" child object
            Transform waterChild = other.transform.Find("Water");
            Transform sandChild = other.transform.Find("Sand");
            
            if (waterChild != null)
            {
                if (sandChild != null)
                {
                    sandChild.gameObject.SetActive(false);
                }

                waterChild.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Water child not found on the bucket!");
            }
        }
    }
}
