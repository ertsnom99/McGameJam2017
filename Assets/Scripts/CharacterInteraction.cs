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
        if (collision.tag == AreaManager.INTERACTIF_AREA)
        {
            currentInteractable = collision.gameObject;
        }
        if (collision.tag == "Character" && gameObject.tag == GameManager.PLAYER)
        {
            currentInfectable.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == AreaManager.INTERACTIF_AREA)
        {
            currentInteractable = null;
        }
        if (collision.tag == "Character" && gameObject.tag == GameManager.PLAYER)
        {
            currentInfectable.Remove(collision.gameObject);
        }
    }

    public void interact()
    {
        if (currentInteractable != null)
        {
            if (currentInteractable.GetComponent<Interactable>().IsInfectedObject() && gameObject.tag == GameManager.PLAYER)
            {
                character.setInfected(true);
                // ..........
            }
            
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
