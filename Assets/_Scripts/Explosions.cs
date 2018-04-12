using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour {

    // Use this for initialization
	void Start ()
    {
        StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
