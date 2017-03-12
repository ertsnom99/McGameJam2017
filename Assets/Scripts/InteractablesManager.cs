using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{

    public GameObject[] interactableObjects;
    public GameObject[] potentialInfectiveObjects;
    public GameObject safeObject;
    public GameObject infectiveObject;

    public bool InfectionStarted { private set; get; }

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        InfectionStarted = false;
    }

    public void InfectRandomObject()
    {
        int random = (int)Random.Range(0, potentialInfectiveObjects.Length - 1);
        //Debug.Log("Random : " + random);
        //Debug.Log("Infected : " + potentialInfectiveObjects[random].name);
        potentialInfectiveObjects[random].GetComponent<Interactable>().SetInfectedObject(true);
    }

    public void StartInfection()
    {
        print("INFECTION START!");
        InfectionStarted = true;
    }
}
