using System.Collections;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField]
    public bool isOnFire = false;

    private ParticleSystem particles;
    
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