using UnityEngine;

public class CampfireController : MonoBehaviour
{
    private int logCount = 0;
    private ParticleSystem particles;
    public bool isOnFire = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("SM_Prop_Loghalf_02"))
        {
            logCount++;
        }

        if (other.gameObject.name.Contains("Match") && other.GetComponent<MatchController>().isOnFire)
        {
            setOnFire();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("SM_Prop_Loghalf_02"))
        {
            logCount--;
        }
    }
}
