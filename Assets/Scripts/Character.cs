using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private bool isInfected = false;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public bool IsInfected()
    {
        return isInfected;
    }

    public void setInfected(bool b)
    {
        isInfected = b;
    }
}
