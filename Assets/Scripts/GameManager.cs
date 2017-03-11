using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public const int CHARACTER_SPEED = 1;
    public GameObject[] infectiveObjects;
    public int numberBotCharacters = 20;
    public TextMesh timerText;

    private float timer = 180.0f;
    

    private void Update()
    {
        ManageTimer(); 
    }

    private void ManageTimer()
    {
        timer -= Time.deltaTime;
        int minutes = (int) Mathf.Floor(timer / 60);
        int seconds = (int) Mathf.Floor(timer % 60);
        timerText.text = minutes + ":" + seconds;
    }
}
