using UnityEngine;

public class debugcol : MonoBehaviour {
    [SerializeField, Range(0, 5)]
    private float velocity_threshold;

    private void OnCollisionEnter(Collision collision) {
        float a = 1;
        var test = collision.collider.gameObject.name;
        if (collision.collider.gameObject.tag.Equals("test")) {
            float mag = collision.relativeVelocity.magnitude;
            // Now that we have the magnitude, we can check if it was big enough
            if (mag > velocity_threshold) {
                // TODO: here spawn sparks/firea
                a = 0;
            }
        }

    }

}
