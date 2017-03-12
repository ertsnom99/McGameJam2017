using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {

    public Color infectedColor = new Color(238, 225, 119);

    private const string DIE = "Die";
    private const string KILL = "Kill";
    public bool IsInfectious { private set; get; }
    public bool IsInfected { private set; get; }
    public bool IsDead { private set; get; }
    private float symptomsDelay = 5.0f;
    private float deathDelay = 15.0f;
    private float animationDelay = 2.5f;

    public void setInfectious(bool b)
    {
        IsInfectious = b;
    }

    public void setInfected(bool b)
    {
        IsInfected = b;
    }

    public void BecomeInfected()
    {
        Debug.Log("I AM INFECTED!!");
        IsInfected = true;
        StartCoroutine(ShowSymptoms());
        StartCoroutine(DieFromVirus());
    }

    IEnumerator ShowSymptoms()
    {
        yield return new WaitForSeconds(symptomsDelay);
        GetComponentInChildren<SpriteRenderer>().color = infectedColor;
        Debug.Log("Color changed");
    }

    IEnumerator DieFromVirus()
    {
        yield return new WaitForSeconds(deathDelay);
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        // MUSIC 
        AkSoundEngine.PostEvent("DeathSound", GameObject.Find("Music"));
        IsDead = true;
        GetComponentInChildren<Animator>().SetTrigger(DIE);
        if (gameObject.tag == GameManager.BOT)
        {
            Debug.Log("Script deactivated");
            GetComponent<AIMovement>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (gameObject.tag == GameManager.PLAYER)
        {
            GetComponent<PlayerController>().enabled = false;
        }

        yield return new WaitForSeconds(animationDelay);
        Destroy(gameObject);
    }

    public void Kill(GameObject killable)
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger(KILL);
        killable.GetComponentInChildren<Character>().IsKilled();
    }

    public void IsKilled()
    {
        StartCoroutine(Die());
    }


}
