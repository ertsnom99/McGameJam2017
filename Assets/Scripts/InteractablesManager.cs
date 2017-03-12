using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour {

    public GameObject[] interactableObjects;
    public GameObject[] infectiveObjects;
    public GameObject safeObject;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public GameObject getRandomInfective()
    {
        int random = (int)Random.Range(0, infectiveObjects.Length-1);
        return infectiveObjects[random];
    }
}
