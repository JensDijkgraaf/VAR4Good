using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool closed = true;
    // Start is called before the first frame update
    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, -90f, 0) * closedRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Door()
    {
        if (closed)
        {
            StartCoroutine(OpeningDoor());
        }
    }

    private IEnumerator OpeningDoor()
    {
        closed = false;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            transform.rotation = Quaternion.Slerp(closedRotation, openRotation, t);
            yield return null;
        }
    }
}
