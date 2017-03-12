using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int CHARACTER_SPEED = 5;
    public const string PLAYER = "Player";
    public const string BOT = "Bot";

    public TextMesh timerText;

    public AreaManager areaManagerScript;

    public GameObject computerCharacter;
    public GameObject playerCharacter;
    public GameObject characterContainers;

    public ControllersManager controllersManager;
    public InteractablesManager interactablesManager;

    private int numberBotCharacters;

    private float remainingTime;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        numberBotCharacters = 1;
        remainingTime = 180.0f;
    }

    private void Start()
    {
        CreateBots();
        controllersManager.SearchForControllers();
        CreatePlayers();
        InitializeTimer();
    }

    private void CreateBots()
    {
        for (int i = 0; i < numberBotCharacters; i++)
        {
            Vector3 spawnPoint = areaManagerScript.GeneratePosition();

            GameObject computer = Instantiate(computerCharacter);
            computer.GetComponent<AIMovement>().areaManager = areaManagerScript;

            computer.transform.position = spawnPoint;
            computer.transform.parent = characterContainers.transform;
        }
    }

    private void CreatePlayers()
    {
        for (int i = 0; i < controllersManager.controllersNumber.Length; i++)
        {
            Vector3 spawnPoint = areaManagerScript.GeneratePosition();

            GameObject player = Instantiate(playerCharacter);
            player.GetComponent<PlayerController>().joystickNumber = controllersManager.controllersNumber[i];

            player.transform.position = spawnPoint;
            player.transform.parent = characterContainers.transform;
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


