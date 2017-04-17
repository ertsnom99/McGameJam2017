using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {

    public Color infectedColor = new Color(238, 225, 119);

    private const string DIE = "Die";
    private const string KILL = "Kill";
    private const int killDelay = 5;
    public bool IsInfectious { private set; get; }
    public bool IsInfected { private set; get; }
    public bool IsDead { private set; get; }
    private float symptomsDelay = 5.0f;
    private float deathDelay = 15.0f;
    private float animationDelay = 2.5f;

    public void setInfectious(bool b) // can infect others
    {
        IsInfectious = b;
    }

    public void setInfected(bool b) // has caught the virus
    {
        IsInfected = b;
    }

    public void BecomeInfected()
    {
        IsInfected = true;
        StartCoroutine(ShowSymptoms());
        StartCoroutine(DieFromVirus());
    }

    IEnumerator ShowSymptoms()
    {
        yield return new WaitForSeconds(symptomsDelay);
        // AkSoundEngine.PostEvent("CalmToPanic", GameObject.Find("Music"));
        GetComponentInChildren<SpriteRenderer>().color = infectedColor;
    }

    IEnumerator DieFromVirus()
    {
        yield return new WaitForSeconds(deathDelay);
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        // death sound effect 
        // AkSoundEngine.PostEvent("DeathSound", GameObject.Find("Music"));
        IsDead = true;
        GetComponentInChildren<Animator>().SetTrigger(DIE);
        if (gameObject.tag == GameManager.BOT)
        {
            GetComponent<AIMovement>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else if (gameObject.tag == GameManager.PLAYER)
        {
            GetComponent<PlayerController>().enabled = false;
        }

        yield return new WaitForSeconds(animationDelay);
        gameObject.SetActive(false);
    }

    public void Kill(GameObject killable)
    {
        gameObject.GetComponentInChildren<Animator>().SetTrigger(KILL);
        if (killable.activeSelf && killable != null && killable.GetComponentInChildren<Character>() != null)
        { 
            killable.GetComponentInChildren<Character>().IsKilled();
        }
    }

    public void IsKilled()
    {
        StartCoroutine(Die());
    }
}
