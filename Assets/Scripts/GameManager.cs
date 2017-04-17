using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject[] bots;
    private GameObject[] players;

    private float remainingTime;
    private bool gameEnded;

    public GameObject gameFinishedUI;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        numberBotCharacters = 10;
        remainingTime = 3.0f;
        gameEnded = false;
    }
    
    private void Start()
    {
        CreateBots();
        controllersManager.SearchForControllers();
        CreatePlayers();
        interactablesManager.InfectRandomObject();
        InitializeTimer();
    }

    private void CreateBots()
    {
        bots = new GameObject[numberBotCharacters];

        for (int i = 0; i < numberBotCharacters; i++)
        {
            Vector3 spawnPoint = areaManagerScript.GenerateSpawnPoint();
            
            GameObject computer = Instantiate(computerCharacter);
            computer.GetComponent<AIMovement>().areaManager = areaManagerScript;
            computer.GetComponent<AIMovement>().changePriority(i);

            computer.transform.position = spawnPoint;
            computer.transform.parent = characterContainers.transform;

            bots[i] = computer;
        }
    }

    private void CreatePlayers()
    {
        players = new GameObject[controllersManager.controllersNumber.Length];

        for (int i = 0; i < controllersManager.controllersNumber.Length; i++)
        {
            Vector3 spawnPoint = areaManagerScript.GenerateSpawnPoint();

            GameObject player = Instantiate(playerCharacter);
            player.GetComponent<PlayerController>().joystickNumber = controllersManager.controllersNumber[i];
            player.transform.position = spawnPoint;
            player.transform.parent = characterContainers.transform;
            players[i] = player;
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

            if (remainingTime <= 0)
            {
                EndGame("Time Out!");
            }
            else
            {
                UpdateTimer(remainingTime);
            }
        }
    }

    private void UpdateTimer(float remainingTime)
    {
        int minutes = (int)Mathf.Floor(remainingTime / 60);
        int seconds = (int)Mathf.Floor(remainingTime % 60);
        timerText.text = minutes + ":" + (100 + seconds).ToString().Substring(1, 2);
    }

    private void Update()
    {
        CheckForWinner();
    }

    private void CheckForWinner()
    {
        if (!gameEnded && interactablesManager.InfectionStarted)
        {
            bool otherPlayersWin = true;
            bool infectiousPlayerWins = true;
            
            foreach (GameObject player in players)
            {
                if (player.GetComponent<Character>().IsInfectious && !player.GetComponent<Character>().IsDead)
                {
                    otherPlayersWin = false;
                }
                else if (!player.GetComponent<Character>().IsInfectious && !player.GetComponent<Character>().IsInfected && !player.GetComponent<Character>().IsDead)
                {
                    infectiousPlayerWins = false;
                }
            }

            foreach (GameObject bot in bots)
            {
                if (!bot.GetComponent<Character>().IsInfected && !bot.GetComponent<Character>().IsDead)
                {
                    infectiousPlayerWins = false;
                }
            }
            
            if (otherPlayersWin && infectiousPlayerWins)
            {
                EndGame("Draw!");
            }
            else if (otherPlayersWin)
            {
                EndGame("Humans' victory!");
            }
            else if (infectiousPlayerWins)
            {
                EndGame("Zombie's victory!");
            }
        }
        else if (!gameEnded)
        {
            int countRemainingPlayers = players.Length;

            foreach (GameObject player in players)
            {
                if (!player.activeSelf)
                {
                    countRemainingPlayers--;
                }
            }

            if (countRemainingPlayers < 1)
            {
                EndGame("Game Over!");
            }
        }
    }

    private void EndGame(string message)
    {

        // end game sound
        // AkSoundEngine.PostEvent("PanicToEnd", GameObject.Find("Music"));
        // AkSoundEngine.PostEvent("CalmToEnd", GameObject.Find("Music"));

        gameEnded = true;

        GameObject.Find("TitleText").GetComponent<Text>().text = message;

        gameFinishedUI.GetComponent<Animator>().SetTrigger("GameFinished");
    }
}


