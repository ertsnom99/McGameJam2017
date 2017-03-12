using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    private GameObject currentInteractable;
    private List<GameObject> currentInfectable;
    private List<GameObject> currentKillable;
    private Character character;

    private void Awake()
    {
        currentInfectable = new List<GameObject>();
        currentKillable = new List<GameObject>();
        character = GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == AreaManager.INTERACTIF_AREA)
        {
            currentInteractable = collision.gameObject;
        }
        if ((collision.tag == GameManager.PLAYER || collision.tag == GameManager.BOT) && gameObject.tag == GameManager.PLAYER && gameObject != collision.gameObject)
        {
            if (!collision.GetComponent<Character>().IsDead)
            {
                currentInfectable.Add(collision.gameObject);
            }
            currentKillable.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == AreaManager.INTERACTIF_AREA)
        {
            currentInteractable = null;
        }
        if ((collision.tag == GameManager.PLAYER || collision.tag == GameManager.BOT) && gameObject.tag == GameManager.PLAYER && gameObject != collision.gameObject)
        {
            currentInfectable.Remove(collision.gameObject);
            currentKillable.Remove(collision.gameObject);          
        }
    }

    public void Kill()
    {
        if (currentKillable.Count != 0)
        {
            //Debug.Log("Kill success!");
            //Debug.Log("current Killable : " + currentKillable.Count);
            foreach (GameObject killable in currentKillable)
            {
                  GetComponent<Character>().Kill(killable);            
            }
            currentKillable.Clear();
        }
    }

    public void Interact()
    {
        if (currentInteractable != null)
        {
            if (currentInteractable.GetComponent<Interactable>().IsInfectedObject() && gameObject.tag == GameManager.PLAYER)
            {
                Debug.Log("a character is infectious");
                character.setInfectious(true);
                GameObject.Find("InteractablesManager").GetComponent<InteractablesManager>().StartInfection();
                currentInteractable.GetComponent<Interactable>().SetInfectedObject(false);
            }           
            currentInteractable.GetComponent<Interactable>().Interaction(gameObject);
        }
    }

    public void Infect()
    {
        if (gameObject.GetComponent<Character>().IsInfectious && currentInfectable.Count != 0)
        {
            //Debug.Log("Infection success!");
            //Debug.Log("current Infectable : " + currentInfectable.Count);
            foreach (GameObject infectable in currentInfectable){
                if (!infectable.GetComponent<Character>().IsInfected)
                {
                    infectable.GetComponent<Character>().BecomeInfected();
                }
            }
        }
    }
}
