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
            // attack sound effect
            // AkSoundEngine.PostEvent("AttackSound", GameObject.Find("Music"));
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
                character.setInfectious(true);
                Debug.Log("Someone has become a zombie.");
                currentInteractable.GetComponent<Interactable>().SetInfectedObject(false);
                GameObject.Find("InteractablesManager").GetComponent<InteractablesManager>().StartInfection();
                GameObject.Find("ControllersManager").GetComponent<ControllersManager>().VibrateController(GetComponent<PlayerController>().joystickNumber, 1, 0.15f);                
            }           
            currentInteractable.GetComponent<Interactable>().Interaction(gameObject);
        }
    }

    public void Infect()
    {
        if (gameObject.GetComponent<Character>().IsInfectious && currentInfectable.Count != 0)
        {
            foreach (GameObject infectable in currentInfectable){
                if (!infectable.GetComponent<Character>().IsInfected)
                {
                    infectable.GetComponent<Character>().BecomeInfected();
                }
            }
        }
    }
}
