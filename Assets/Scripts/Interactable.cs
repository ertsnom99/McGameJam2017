using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    private bool isInfectedObject = true;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public bool IsInfectedObject()
    {
        return isInfectedObject;
    }

    public void SetInfectedObject(bool b)
    {
        isInfectedObject = b;
    }

}
