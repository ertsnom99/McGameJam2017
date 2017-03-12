using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour {

    public GameObject relatedObject;

    private const string ANIMATE_INTERACTION = "Animate";
    private const string FADE = "Fade";
    private GameObject interactingCharacter;
    private bool isInfectedObject = false;
    private bool isOccupied = false;
    private float interactionDuration = 2.0f;
    private float fadeDuration = 4.0f;
    private float lightsDelay = 15.0f;

    public bool IsInfectedObject()
    {
        return isInfectedObject;
    }

    public void SetInfectedObject(bool b)
    {
        isInfectedObject = b;
    }

    public void Interaction(GameObject interactingCharacter)
    {
        if (!isOccupied)
        {
            isOccupied = true;
            
            relatedObject.GetComponentInChildren<Animator>().SetTrigger(ANIMATE_INTERACTION);
            if (relatedObject.name == "Light")
            {
                // MUSIC 
                AkSoundEngine.PostEvent("Lightswitch", GameObject.Find("Music"));
                GameObject.Find("LightsOff").GetComponent<Animator>().SetTrigger(FADE);
                StartCoroutine(ManageOccupiedState(fadeDuration, true));
                StartCoroutine(ManageLightsDelay());
            } else
            {
                if (relatedObject.name == "Balcony")
                {
                    // MUSIC 
                    AkSoundEngine.PostEvent("BirdNest", GameObject.Find("Music"));
                }
                if (relatedObject.name == "Coffee")
                {
                    // MUSIC 
                    AkSoundEngine.PostEvent("CoffeeMachine", GameObject.Find("Music"));
                }
                if (relatedObject.name == "Toilet")
                {
                    // MUSIC 
                    AkSoundEngine.PostEvent("Toilet", GameObject.Find("Music"));
                }
                if (relatedObject.name == "Garden")
                {
                    // MUSIC 
                    AkSoundEngine.PostEvent("Plant", GameObject.Find("Music"));
                }
                if (relatedObject.name == "Trash")
                {
                    // MUSIC 
                    AkSoundEngine.PostEvent("Trashcan", GameObject.Find("Music"));
                }
                if (relatedObject.name == "Workspace")
                {
                    // MUSIC 
                    AkSoundEngine.PostEvent("Workstation", GameObject.Find("Music"));
                }
                this.interactingCharacter = interactingCharacter;
                interactingCharacter.GetComponentInChildren<SpriteRenderer>().enabled = false;
                interactingCharacter.GetComponentInChildren<Animator>().SetTrigger(ANIMATE_INTERACTION);
               
                if (interactingCharacter.tag == GameManager.PLAYER)
                {
                    interactingCharacter.GetComponent<PlayerController>().enabled = false;
                }
                if (interactingCharacter.tag == GameManager.BOT)
                {
                    interactingCharacter.GetComponent<AIMovement>().enabled = false;
                }

                StartCoroutine(ManageOccupiedState(interactionDuration, false));         
            }          
        }
    }

    public IEnumerator ManageLightsDelay()
    {
        yield return new WaitForSeconds(lightsDelay);
        isOccupied = false;
    }

    IEnumerator ManageOccupiedState(float interactionDuration, bool lights)
    {   
        yield return new WaitForSeconds(interactionDuration);
        isOccupied = false;
        if (!lights) {
            if (interactingCharacter.tag == GameManager.PLAYER)
            {
                interactingCharacter.GetComponent<PlayerController>().enabled = true;
            }
            if (interactingCharacter.tag == GameManager.BOT)
            {
                interactingCharacter.GetComponent<AIMovement>().enabled = true;
            }
            interactingCharacter.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
}
