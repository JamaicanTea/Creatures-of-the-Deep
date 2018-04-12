using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour

{
    public Transform myTransform;

    public AudioSource[] sounds = new AudioSource[5];
    public GameObject[] explosions = new GameObject[2];

    // 1: Enemy Bullet
    // 2: Shield Power Up
    public GameObject[] bossObjects = new GameObject[2];

    public static int health = 10;

    bool[] points = new bool[5];

    // Use this for initialization
    void Start()
    {
        myTransform = transform;

        points[0] = false;
        points[1] = false;
        points[2] = false;
        points[3] = false;
        points[4] = false;

        InvokeRepeating("Fire", 1f, 1.5f);

        GameObject enemyExplosionSound = GameObject.Find("Enemy_Explosion");
        sounds[0] = enemyExplosionSound.GetComponent<AudioSource>();

        GameObject enemyFireSound = GameObject.Find("Enemy_Fire");
        sounds[1] = enemyFireSound.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
            sounds[0].Play();
            Instantiate(bossObjects[1], transform.position, Quaternion.identity);

            // Parse the text of the scoreGT into an int
            int score = int.Parse(Main.scoreGT.text);
            score += 500;

            // Convert the score back to a string and display it
            Main.scoreGT.text = score.ToString();
        }

        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        Move();
        transform.rotation = Quaternion.Euler(currentPosition.x * 60, 0, 0);

        // Changing direction
        if (currentPosition.x < -7)
        {
            points[0] = true;
        }
        if (points[0])
        {
            TurnAround();
        }

        // Change direction again
        if (currentPosition.x > 7 && currentPosition.y < -2)
        {
                points[0] = false;
                points[1] = true;
        }

        if (points[1])
        {
            TurnAround2();
        }

        if (currentPosition.x > 7 && currentPosition.y > 4 && points[1] == true)
        {
                points[1] = false;
                points[2] = true;
        }

        if (points[2])
        {
            TurnAround3();
        }

        if (currentPosition.x < -4f && currentPosition.y < -1f && points[2] == true)
        {
                points[2] = false;
                points[3] = true;
        }
        if (points[3])
        {
            TurnAround4();
        }

        if (currentPosition.x < -2 && currentPosition.y > 4 && points[3] == true)
        {
            points[3] = false;
            points[4] = true;
        }
        if (currentPosition.y > 4)
        {
            if (currentPosition.x < -3)
            {
                points[4] = false;
                points[0] = true;
            }
        }
    }

    public void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += -12f * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void TurnAround()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.y -= 10f * Time.deltaTime;
        currentPosition.x += 26f * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void TurnAround2()
    {
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        currentPosition.x += 12f * Time.deltaTime;
        currentPosition.y += 10f * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void TurnAround3()
    {
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        currentPosition.y -= 10f * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void TurnAround4()
    {
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        currentPosition.x += 12f * Time.deltaTime;
        currentPosition.y += 10f * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void TurnAround5()
    {
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        currentPosition.x += 16f * Time.deltaTime;
        currentPosition.y -= 6f * Time.deltaTime;
        transform.position = currentPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Bullet")
        {
            health--;
            Destroy(other.gameObject);
            Instantiate(explosions[1], transform.position, Quaternion.identity);
        }
    }

    void Fire()
    {
        StartCoroutine(DualShot());
    }

    IEnumerator DualShot()
    {
        yield return new WaitForSeconds(0);
        sounds[1].Play();
        GameObject bullet = Instantiate(bossObjects[0], transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = Vector3.left * 50f;
        yield return new WaitForSeconds(0.3f);
        sounds[1].Play();
        GameObject bullet2 = Instantiate(bossObjects[0], transform.position, Quaternion.identity) as GameObject;
        bullet2.GetComponent<Rigidbody>().velocity = Vector3.left * 50f;
    }
}
