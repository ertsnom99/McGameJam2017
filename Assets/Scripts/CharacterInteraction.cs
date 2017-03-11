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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            currentInteractable = collision.gameObject;
        }
        if (collision.tag == "Character")
        {
            currentInfectable.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
