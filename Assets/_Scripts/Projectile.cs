using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioSource[] sounds = new AudioSource[5];

    void Start()
    {
        GameObject enemyExplosionSound = GameObject.Find("Enemy_Explosion");
        sounds[0] = enemyExplosionSound.GetComponent<AudioSource>();

        GameObject enemyHitSound = GameObject.Find("Enemy_Hit");
        sounds[1] = enemyHitSound.GetComponent<AudioSource>();

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
         if (other.tag == "Enemy")
        {
            sounds[0].Play();
        }

        if (other.tag == "Enemy_2")
        {
            sounds[0].Play();
        }

        if (other.tag == "Enemy_3")
        {
            sounds[0].Play();
        }

        if (other.tag == "Enemy_4")
        {
            sounds[0].Play();
        }

        if (other.tag == "Boss")
        {
            sounds[1].Play();
        }
    }
}





