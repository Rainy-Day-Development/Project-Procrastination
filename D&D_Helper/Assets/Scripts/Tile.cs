using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool Current = false;
    public bool Target = false;
    public bool Selectable = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (Target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (Selectable)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else {
            GetComponent<Renderer>().material.color = Color.white;
        }




	}
}
