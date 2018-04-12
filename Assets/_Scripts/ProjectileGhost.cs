using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGhost : MonoBehaviour
{
    public AudioSource[] sounds = new AudioSource[5];

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 15)
        {
            Destroy(gameObject);
        }

        if (transform.position.x <= -15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
    }
}





