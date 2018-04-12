using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public Transform myTransform;
    public GameObject powerUpPrefab;
    public GameObject[] explosions = new GameObject[2];

    public bool turnAround;

    public float speed = 10f;

    // Use this for initialization
    void Start()
    {
        turnAround = false;
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroys object when off screen
        if (transform.position.x > 9 && turnAround == true)
        {
            Destroy(gameObject);
        }

        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        // Changing direction
        if (currentPosition.x < -8)
        {
            turnAround = true;
        }

        if (turnAround == true)
        {
            TurnAround();
        }
        else
        {
            Move();
        }

        transform.rotation = Quaternion.Euler(currentPosition.x * 80, 0, 0);
    }

    public void Move()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x += -speed * Time.deltaTime;
        transform.position = currentPosition;
    }

    public void TurnAround()
    {
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;

        currentPosition.y += -20f * Time.deltaTime;
        currentPosition.x += 20f * Time.deltaTime;
        transform.position = currentPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Bullet")
        {

            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(explosions[0], transform.position, Quaternion.identity);
            Plane.enemyKills[2]++;

            if (Plane.enemyKills[2] == 6)
            {
                GameObject powerUp = Instantiate(powerUpPrefab);
                powerUp.transform.position = transform.position;
            }

            // Parse the text of the scoreGT into an int
            int score = int.Parse(Main.scoreGT.text);
            score += 100;

            // Convert the score back to a string and display it
            Main.scoreGT.text = score.ToString();
        }
    }
}
