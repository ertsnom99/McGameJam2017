using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private GameObject currentInteractable;
    private List<GameObject> currentInfectable;
    private Character character;

    private void Awake()
    {
        currentInfectable = new List<GameObject>();
        character = GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("TriggerEnter");
        if (collision.tag == "Interactable")
        {
            currentInteractable = collision.gameObject;
        }
        if (collision.tag == "Character")
        {
            currentInfectable.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("TriggerExit");
        if (collision.tag == "Interactable")
        {
            currentInteractable = null;
        }
        if (collision.tag == "Character")
        {
            currentInfectable.Remove(collision.gameObject);
        }
    }

    public void interact()
    {
        if (currentInteractable != null)
        {
            if (currentInteractable.GetComponent<Interactable>().IsInfectedObject())
            {
                Debug.Log("I am infected!");
                character.setInfected(true);
            }
            Debug.Log("Interaction success!");
            // Set animation trigger
            currentInteractable.GetComponent<Interactable>().Interaction(gameObject);
        }
    }

    public void infect()
    {
        if (currentInfectable != null)
        {
            Debug.Log("Infection success!");
            // Set animation trigger
        }
    }
}
