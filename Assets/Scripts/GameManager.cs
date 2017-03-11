using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public const int CHARACTER_SPEED = 5;

    public TextMesh timerText;

    public AreaManager areaManagerScript;

    public GameObject computerCharacter;
    public GameObject computerContainers;

    public GameObject[] infectiveObjects;

    private int numberBotCharacters;
    private float remainingTime;

    private void Awake()
    {
        InitializeVariables();
        CreateBots();
        InitializeTimer();
    }

    private void InitializeVariables()
    {
        numberBotCharacters = 20;
        remainingTime = 180.0f;
    }

    private void CreateBots()
    {
        for (int i = 0; i < numberBotCharacters; i++)
        {
            Vector3 spawnPoint = areaManagerScript.GeneratePosition();

            GameObject computer = Instantiate(computerCharacter);
            computer.GetComponent<AIMovement>().areaManager = areaManagerScript;

            computer.transform.position = spawnPoint;
            computer.transform.parent = computerContainers.transform;
        }
    }

    private void InitializeTimer()
    {
        UpdateTimer(remainingTime);
        StartCoroutine(DecrementTimer());
    }

    private IEnumerator DecrementTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            remainingTime -= 1.0f;
            UpdateTimer(remainingTime);
        }
    }

    private void UpdateTimer(float remainingTime)
    {
        int minutes = (int)Mathf.Floor(remainingTime / 60);
        int seconds = (int)Mathf.Floor(remainingTime % 60);
        timerText.text = minutes + ":" + (100 + seconds).ToString().Substring(1, 2);
    }
}

