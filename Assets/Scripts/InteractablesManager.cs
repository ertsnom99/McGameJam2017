using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour {

    public GameObject[] interactableObjects;
    public GameObject[] potentialInfectiveObjects;
    public GameObject safeObject;
    public GameObject infectiveObject;

    void Start () {
        InfectObjectStart();
	}
	
	void Update () {
		
	}

    public void InfectObjectStart()
    {
        int random = (int)Random.Range(0, potentialInfectiveObjects.Length-1);
        Debug.Log("Random : " + random);
        Debug.Log("Infected : " + potentialInfectiveObjects[random].name);
        potentialInfectiveObjects[random].GetComponent<Interactable>().SetInfectedObject(true);
    }
}
