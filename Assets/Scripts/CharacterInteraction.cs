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
    private float killDelay = 5.0f;

    private void Awake()
    {
        currentInfectable = new List<GameObject>();
        currentKillable = new List<GameObject>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        killDelay -= Time.deltaTime;
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
        if (currentKillable.Count != 0 && killDelay < 0)
        {
            killDelay = 5.0f;
            Debug.Log("Kill success!");
            Debug.Log("current Killable : " + currentKillable.Count);
            // MUSIC 
            AkSoundEngine.PostEvent("AttackSound", GameObject.Find("Music"));
            Debug.Log("Sound  attack riggred");
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
                //GameObject.Find("ControllersManager").GetComponent<ControllersManager>().VibrateController(GetComponent<PlayerController>().joystickNumber - 1, 1, 0.15f);
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
