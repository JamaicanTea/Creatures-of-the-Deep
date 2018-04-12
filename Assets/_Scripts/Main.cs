using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static GUIText scoreGT;
    public static bool dead;

    // Enemy
    public static int[] enemyWaves = new int[5];
    // Add special of each enemy that drops the buff.

    // [0] = Shrimp
    // [1] = Last Shrimp
    // [2] = Prawn
    // [3] = Last Prawn
    // [4] = Pacu
    public GameObject[] enemies = new GameObject[5];

    // Audio
    public AudioSource[] sounds = new AudioSource[6];

    void Awake()
    {
        Cursor.visible = false;
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;

        enemyWaves[0] = 0;
        enemyWaves[1] = 0;
        enemyWaves[2] = 0;
        enemyWaves[3] = 0;
        enemyWaves[4] = 0;

        // Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");

        // Get the GUIText Component of that GameObject
        scoreGT = scoreGO.GetComponent<GUIText>();
        scoreGT.text = "0";

        sounds[0].Play();
        StartCoroutine(FirstWave());
    }

    IEnumerator RestartGame()
    {
        dead = false;
        sounds[0].Stop();
        yield return new WaitForSeconds(2.5f);
        Application.LoadLevel(Application.loadedLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (dead)
        {
            StartCoroutine(RestartGame());
        }
        else
        {
            dead = false;
        }

        if (enemyWaves[0] == 6)
        {
            enemyWaves[0]++;
            CancelInvoke("EnemySpawn");
            //waveOne = false;
            //waveTwo = true;
            //StartCoroutine(SecondWave());
        }

        /*if (enemyWaves[1] == 6)
        {
            enemyWaves[1]++;
            CancelInvoke("EnemySpawn2");
            StartCoroutine(ThirdWave());
        }

        if (enemyWaves[2] == 6)
        {
            enemyWaves[2]++;
            CancelInvoke("EnemySpawn3");
            StartCoroutine(FourthWave());
        }

        if (enemyWaves[3] == 6)
        {
            enemyWaves[3]++;
            CancelInvoke("EnemySpawn4");
            StartCoroutine(Boss());
        }*/
    }

    IEnumerator FirstWave()
    {
        // Increase this later to 3
        yield return new WaitForSeconds(1);
        InvokeRepeating("EnemySpawn", 1, 0.5f);
    }

    /*IEnumerator SecondWave()
    {
        yield return new WaitForSeconds(2);
        InvokeRepeating("EnemySpawn2", 1, 1);
    }

    IEnumerator ThirdWave()
    {
        yield return new WaitForSeconds(3);
        InvokeRepeating("EnemySpawn3", 1, 1);
    }

    IEnumerator FourthWave()
    {
        yield return new WaitForSeconds(2);
        InvokeRepeating("EnemySpawn4", 1, 1);
    }

    IEnumerator Boss()
    {
        yield return new WaitForSeconds(5);
        Instantiate(enemies[4], new Vector3(15, 3, 0), Quaternion.identity);
    }*/

    void EnemySpawn()
    {
        Instantiate(enemies[0], new Vector3(15, 5, 0), Quaternion.identity);
        enemyWaves[0]++;
    }

    /*void EnemySpawn2()
    {
        Instantiate(enemies[0], new Vector3(15, -5, 0), Quaternion.identity);
        enemyWaves[1]++;
    }

    void EnemySpawn3()
    {
        Instantiate(enemies[2], new Vector3(15, 5, 0), Quaternion.identity);
        enemyWaves[2]++;
    }

    void EnemySpawn4()
    {
        Instantiate(enemies[2], new Vector3(15, -5, 0), Quaternion.identity);
        enemyWaves[3]++;
    }*/
}
