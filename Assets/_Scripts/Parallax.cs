using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    // The scrolling foregrounds
    public GameObject[] panels;


    // Use this for initialization
    void Start ()
    {

        // Set initial positions of panels
        //panels[0].transform.position = new Vector3(0, 0, 10);
        //panels[1].transform.position = new Vector3(5.36f, 0, 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Vector3 pos = panels[1].transform.position;

        /*if (panels[1].transform.position.x <-8)
        {
            panels[1].transform.position = new Vector3(8, 0, 10);
        }
        else
        {
            
            pos.x -= 1 * 2 * Time.deltaTime;
            panels[1].transform.position = pos;
        }*/
    }
}
