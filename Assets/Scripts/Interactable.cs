using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour {

    private const string ANIMATE_INTERACTION = "Animate";
    public GameObject relatedObject;

    private GameObject interactingCharacter;
    private bool isInfectedObject = false;
    private bool isOccupied = false;
    private float interactionDuration = 2.0f;

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

    public void Interaction(GameObject interactingCharacter)
    {
        if (!isOccupied)
        {
            isOccupied = true;
            this.interactingCharacter = interactingCharacter;
            relatedObject.GetComponentInChildren<Animator>().SetTrigger(ANIMATE_INTERACTION);
            interactingCharacter.GetComponentInChildren<Animator>().SetTrigger(ANIMATE_INTERACTION);
            StartCoroutine(ManageOccupiedState(interactionDuration));
        }
    }

    IEnumerator ManageOccupiedState(float interactionDuration)
    {        
        if (interactingCharacter.tag == GameManager.PLAYER)
        {
            interactingCharacter.GetComponent<PlayerController>().enabled = false;
        }
        if (interactingCharacter.tag == GameManager.BOT)
        {
            interactingCharacter.GetComponent<NavMeshAgent>().enabled = false;
            interactingCharacter.GetComponent<AIMovement>().enabled = false;
        }
        yield return new WaitForSeconds(interactionDuration);
        isOccupied = false;
        if (interactingCharacter.tag == GameManager.PLAYER)
        {
            interactingCharacter.GetComponent<PlayerController>().enabled = true;
        }
        if (interactingCharacter.tag == GameManager.BOT)
        {
            interactingCharacter.GetComponent<NavMeshAgent>().enabled = true;
            interactingCharacter.GetComponent<AIMovement>().enabled = true;
        }
    }
}
