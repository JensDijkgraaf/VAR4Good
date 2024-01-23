using System;
using UnityEngine;

public class FireCollision : MonoBehaviour
{
    [SerializeField, Range(0, 5)]
    private float velocity_threshold;

    private ParticleSystem particles;
    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        if (particles is not null) ;
        // particles.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherObjectMatch = other.gameObject.name.IndexOf("match", StringComparison.OrdinalIgnoreCase) >= 0;
        // Check if other object is a match
        if (!otherObjectMatch)
            return;
        var velocity = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        // Check if velocity is great enough
        if (velocity >= velocity_threshold)
        {
            // Light the match on fire by calling the match object
            var controller = other.gameObject.GetComponent("MatchController") as MatchController;
            if (controller != null)
            {
                controller.setOnFire();
            }
        }
    }
}
