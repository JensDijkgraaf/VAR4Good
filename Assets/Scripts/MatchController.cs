using System.Collections;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField]
    public bool isOnFire = false;

    private ParticleSystem particles;
    private bool isTouchingTree = false;
    private float touchStartTime;
    private GameObject collidedTree;    

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Stop();

        if (isOnFire)
        {
            setOnFire();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            // Start the timer when the match touches the tree
            isTouchingTree = true;
            touchStartTime = Time.time;
            // Store the reference to the collided tree object
            collidedTree = other.gameObject;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            // Reset the timer and references when the match stops touching the tree
            isTouchingTree = false;
            touchStartTime = 0f;
            collidedTree = null;
        }
    }

    void Update()
    {
        // Check if the match has been held against the tree for 2 seconds or more
        if (isTouchingTree && Time.time - touchStartTime >= 2f && isOnFire)
        {
            // Ignite the tree only if the match is on fire and has been held for 2 seconds
            if (collidedTree != null)
            {
                collidedTree.GetComponent<TreeController>().SetOnFire();
                collidedTree = null;
            }
        }
    }

    public void setOnFire()
    {
        // If we already are on fire, we don't do anything.
        if (isOnFire)
            return;
        isOnFire = true;
        var main = particles.main;
        main.loop = true;
        particles.Play();
        StartFireTimer();
    }

    public void putFireOut()
    {
        isOnFire = false;
        particles.Stop();
    }

    private void StartFireTimer()
    {
        Invoke("putFireOut", 20f);
    }
}
