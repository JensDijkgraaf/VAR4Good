using System;
using UnityEngine;

public class FireCollision : MonoBehaviour {
    [SerializeField, Range(0, 5)]
    private float velocity_threshold;

    private ParticleSystem particles;
    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        if(particles is not null)
            particles.Stop();
    }

    private void OnCollisionEnter(Collision collision) {
        // TODO enter a correct tag!
        if (collision.collider.gameObject.tag.Equals("test")) {
            float mag = collision.relativeVelocity.magnitude;
            // Now that we have the magnitude, we can check if it was big enough
            if (mag > velocity_threshold) {
                particles.Play();
            }
        }

    }

}
