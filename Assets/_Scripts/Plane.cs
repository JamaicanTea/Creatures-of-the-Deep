using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Plane : MonoBehaviour
{
    // Gets the Plane's Transform
    public Transform planeTransform;

    // Lives
    public int shieldLevel;
    public GameObject[] explosions = new GameObject[2];

    // Bullet
    public int shot = 1;
    public bool shoot = false;

    // 1: Player_Bullet
    // 2: Player_Bullet_Ghost
    public GameObject[] playerBullet = new GameObject[2];

    // Power Ups
    //int speedBuffCount = 0;

    // [0] = shootingSpeedBuffCount
    // [1] = movementSpeedBuffCount
    int[] buffCounts = new int[2];
    public static int[] enemyKills = new int[4];
    bool[] powerUps = new bool[5];
    
    public GameObject[] powerUpBuffs = new GameObject[4];
    
    // 1: Twin Shot
    // 2: Movementspeed Buff
    // 3: Shield
    GameObject[] powerUpGOs = new GameObject[3];
    public static int buffCount = 0;

    // Audio
    public AudioSource[] sounds = new AudioSource[6];

    void Awake()
    {
        planeTransform = GameObject.Find("Plane").GetComponent<Transform>();
        //shieldGO = GameObject.Find("Shield");
    }

    // Use this for initialization
    void Start()
    {
        shieldLevel = 1;

        enemyKills[0] = 0;
        enemyKills[1] = 0;
        enemyKills[2] = 0;
        enemyKills[3] = 0;

        // Shoot Speed Buff
        powerUps[0] = false;
        powerUps[1] = false;

        // Twin Shot Gun
        /*powerUps[2] = false;
        powerUpGOs[0] = GameObject.Find("Power Up Gun EGO");
        powerUpGOs[0].SetActive(false);

        // Tri Shot Gun
        powerUps[3] = false;
        powerUpGOs[1] = GameObject.Find("Power Up Gun EGO 1");
        powerUpGOs[1].SetActive(false);*/

        // Shield
        powerUpGOs[2] = GameObject.Find("Shield");

        // Buff Counts
        buffCounts[0] = 0;
        buffCounts[1] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Shield health
        if (shieldLevel >= 1)
        {
            powerUpGOs[2].SetActive(true);
        }
        else if (shieldLevel == 0)
        {
            powerUpGOs[2].SetActive(false);
        }
        else if (shieldLevel <= 0)
        {
            Destroy(gameObject);
            sounds[0].Play();
            Instantiate(explosions[0], transform.position, Quaternion.identity);
        }

        // Restricts players movement
        Vector3 currentPosition = transform.position;
        float clampedX = Mathf.Clamp(currentPosition.x, -11f, 11f);
        float clampedY = Mathf.Clamp(currentPosition.y, -7f, 7f);

        if (!Mathf.Approximately(clampedX, currentPosition.x))
        {
            currentPosition.x = clampedX;
            transform.position = currentPosition;
        }
        if (!Mathf.Approximately(clampedY, currentPosition.y))
        {
            currentPosition.y = clampedY;
            transform.position = currentPosition;
        }
        
        if (Input.GetAxis("Jump") == 1 && shot > 0)
        {
            StartCoroutine(Shoot());

            if (powerUps[2])
            {
                TwinBuff();
            }

            /*if (powerUps[3])
            {
                TriBuff();
            }*/
        }        
    }
    void FixedUpdate()
    {

    }

    void Move()
    {
        // pull information from the input class
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        if (buffCounts[1] == 0)
        {
            // Change transform.position based on the axes
            Vector3 pos = transform.position;
            pos.y += yAxis * 7f * Time.deltaTime;
            pos.x += xAxis * 7f * Time.deltaTime;
            transform.position = pos;
        }
        else if (buffCounts[1] == 1)
        {
            Vector3 pos = transform.position;
            pos.y += yAxis * 8f * Time.deltaTime;
            pos.x += xAxis * 8f * Time.deltaTime;
            transform.position = pos;
        }
        else if (buffCounts[1] == 2)
        {
            Vector3 pos = transform.position;
            pos.y += yAxis * 9f * Time.deltaTime;
            pos.x += xAxis * 9f * Time.deltaTime;
            transform.position = pos;
        }
        else if (buffCounts[1] == 3)
        {
            Vector3 pos = transform.position;
            pos.y += yAxis * 10f * Time.deltaTime;
            pos.x += xAxis * 10f * Time.deltaTime;
            transform.position = pos;
        }
        // Rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis * 30, 0, xAxis * -12);
    }

    // Change these to shoot closer from player and just one
    void TwinBuff()
    {
        Transform bulletTransform = playerBullet[0].GetComponent<Transform>();
        Vector3 tempPos = planeTransform.position;
        tempPos.y = tempPos.y + 0.1f;
        tempPos.x = tempPos.x + 0.5f;
        bulletTransform.position = tempPos;

        // Calls Bullet prefab at offset location
        GameObject twinShot = Instantiate(playerBullet[0], bulletTransform.position, Quaternion.identity);
        // Obtain Bullet prefab's Rigidbody
        Rigidbody twinShotRB = twinShot.GetComponent<Rigidbody>();
        // Shoots Bullet to the right
        twinShotRB.velocity += 35f * Vector3.right;
    }

    IEnumerator Shoot()
    {
        Transform bulletTransform = playerBullet[0].GetComponent<Transform>();
        Vector3 tempPos = planeTransform.position;
        tempPos.y = tempPos.y - 0.3f;
        tempPos.x = tempPos.x + 0.5f;
        bulletTransform.position = tempPos;

        // Calls Bullet prefab at Plane's current location
        GameObject projectileInstance = Instantiate(playerBullet[0], bulletTransform.position, Quaternion.identity);
        // Obtain Bullet prefab's Rigidbody
        Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
        // Shoots Bullet to the right
        projectileRb.velocity += 35f * Vector3.right;
        // Subtracts 1 int to the 'shot' counter
        shot--;
        sounds[1].Play();

        if (buffCounts[0] == 0)
        {            
            yield return new WaitForSeconds(.8f);
            shot++;
        }
        else if (buffCounts[0] == 1)
        {
            sounds[1].Play();
            yield return new WaitForSeconds(.6f);
            shot++;
        }
        else if (buffCounts[0] == 2)
        {
            sounds[1].Play();
            yield return new WaitForSeconds(.4f);
            shot++;
        }
        else if (buffCounts[0] == 3)
        {
            sounds[1].Play();
            yield return new WaitForSeconds(.2f);
            shot++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Instantiate(explosions[1], other.transform.position, Quaternion.identity);
            sounds[3].Play();
            shieldLevel--;
            enemyKills[0]++;

            int score = int.Parse(Main.scoreGT.text);
            score += 100;
            Main.scoreGT.text = score.ToString();

            if (enemyKills[0] == 6)
            {
                GameObject powerUp = Instantiate(powerUpBuffs[0]);
                powerUp.transform.position = other.transform.position;
            }

            if(shieldLevel == 0)
            {
                sounds[5].Play();
            }
        }

        if (other.tag == "Enemy_2")
        {
            Destroy(other.gameObject);
            Instantiate(explosions[1], other.transform.position, Quaternion.identity);
            sounds[3].Play();
            shieldLevel--;
            enemyKills[1]++;

            int score = int.Parse(Main.scoreGT.text);
            score += 100;
            Main.scoreGT.text = score.ToString();

            if (enemyKills[1] == 6)
            {
                GameObject powerUp = Instantiate(powerUpBuffs[0]);
                powerUp.transform.position = other.transform.position;
            }

            if (shieldLevel == 0)
            {
                sounds[5].Play();
            }
        }

        if (other.tag == "Enemy_3")
        {
            Destroy(other.gameObject);
            Instantiate(explosions[1], other.transform.position, Quaternion.identity);
            sounds[3].Play();
            shieldLevel--;
            enemyKills[2]++;

            int score = int.Parse(Main.scoreGT.text);
            score += 100;
            Main.scoreGT.text = score.ToString();

            if (enemyKills[2] == 6)
            {
                GameObject powerUp = Instantiate(powerUpBuffs[1]);

                //Instantiate(powerUp.transform, transform.position, Quaternion.identity);
                // or
                powerUp.transform.position = other.transform.position;
            }

            if (shieldLevel == 0)
            {
                sounds[5].Play();
            }
        }

        if (other.tag == "Enemy_4")
        {
            Destroy(other.gameObject);
            Instantiate(explosions[1], other.transform.position, Quaternion.identity);
            sounds[3].Play();
            shieldLevel--;
            enemyKills[3]++;

            int score = int.Parse(Main.scoreGT.text);
            score += 100;
            Main.scoreGT.text = score.ToString();

            if (enemyKills[3] == 6)
            {
                GameObject powerUp = Instantiate(powerUpBuffs[2]);

                //Instantiate(powerUp.transform, transform.position, Quaternion.identity);
                // or
                powerUp.transform.position = other.transform.position;
            }

            if (shieldLevel == 0)
            {
                sounds[5].Play();
            }
        }

        if (other.tag == "Boss")
        {
            shieldLevel--;
            Boss.health--;
            sounds[4].Play();

            if (shieldLevel == 0)
            {
                sounds[5].Play();
            }
        }

        if (other.tag == "Enemy_Bullet")
        {
            Destroy(other.gameObject);
            shieldLevel--;

            if (shieldLevel == 0)
            {
                sounds[5].Play();
            }
        }

        // Twin Power Up
        if (other.tag == "Twin Shot")
        {
            Destroy(other.gameObject);
            powerUps[2] = true;
            sounds[2].Play();

            int score = int.Parse(Main.scoreGT.text);
            score += 50;
            Main.scoreGT.text = score.ToString();
        }

        // Tri Power Up changing to movementspeed buff
        if (other.tag == "Tri Shot")
        {
            Destroy(other.gameObject);
            buffCounts[1] ++;
            sounds[2].Play();

            int score = int.Parse(Main.scoreGT.text);
            score += 50;
            Main.scoreGT.text = score.ToString();
        }

        // Shoot Speed Power Up
        if (other.tag == "Shoot Speed Buff")
        {
            Destroy(other.gameObject);
            buffCounts[0]++;
            sounds[2].Play();

            int score = int.Parse(Main.scoreGT.text);
            score += 50;
            Main.scoreGT.text = score.ToString();            
        }

        if (other.tag == "Shield Buff")
        {
            Destroy(other.gameObject);
            shieldLevel++;
            sounds[2].Play();

            int score = int.Parse(Main.scoreGT.text);
            score += 50;
            Main.scoreGT.text = score.ToString();
        }
    }

    void OnDestroy()
    {
        Main.dead = true;    
    }
}
