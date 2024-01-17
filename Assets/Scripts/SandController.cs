using UnityEngine;

public class SandCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a bucket
        if (other.name.Equals("Bucket"))
        {
            // Enable the "Water" child object
            Transform sandChild = other.transform.Find("Sand");
            Transform waterChild = other.transform.Find("Water");
            
            if (sandChild != null)
            {
                if (waterChild != null)
                {
                    waterChild.gameObject.SetActive(false);
                }

                sandChild.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("Sand child not found on the bucket!");
            }
        }
    }
}
