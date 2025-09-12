using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Singleton pattern (optional, makes access easier)
    public static GameManager Instance { get; private set; }

    public int player1Score = 0;
    public int player2Score = 0;

    public int maxPucksPerPlayer = 6; // Adjust based on game rules
    private int p1PucksRemaining;
    private int p2PucksRemaining;

    public bool isPlayer1Turn = true; // True = P1, False = P2

    public int gameMode = 0;

    public Transform boardCenter; // Assign in Inspector
    public float innerCircleRadius = 2.5f; // Adjust for scoring area


    // Handleing spawn
    public GameObject puckPrefab; // Assign in inspector
    public Transform spawnPoint;  // Where the new puck should appear
    public GameObject currentPuck; // Keep track of the active puck

    public Transform player1Parent;
    public Transform player2Parent;

    public int p1C;
    public int p2C;



    [Header("Movement Bounds")]
    public BoxCollider movementBounds1;
    public BoxCollider movementBounds2;

    // Singleton pattern (optional, makes access easier)
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ResetGame();

    }

    void Update()
    {
        // r to reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        p1C = GetPlayer1PuckCount();
        p2C = GetPlayer2PuckCount();



    }

    void ResetGame()
    {
        p1PucksRemaining = maxPucksPerPlayer;
        p2PucksRemaining = maxPucksPerPlayer;
        player1Score = 0;
        player2Score = 0;
        isPlayer1Turn = true; // P1 starts
    }

    public void CheckPuckPosition(GameObject puck)
    {

    }

    void AwardPoints()
    {
        if (isPlayer1Turn)
        {
            player1Score += 5; // Adjust score based on rules
        }
        else
        {
            player2Score += 5;
        }
    }

    public void EndTurn()
    {
        /*
        if (isPlayer1Turn)
        {
            p1PucksRemaining--;
        }
        else
        {
            p2PucksRemaining--;
        }

        // ✅ Switch turns
        isPlayer1Turn = !isPlayer1Turn;

        //Debug.Log("Turn ended. Next turn: " + (isPlayer1Turn ? "Player 1" : "Player 2"));

        // ✅ Check if game is over
        if (p1PucksRemaining <= 0 && p2PucksRemaining <= 0)
        {
            EndGame();
        }
        */
    }


    void EndGame()
    {
        /*
        //Debug.Log("Game Over!");

        if (player1Score > player2Score)
        {
            //Debug.Log("Player 1 Wins!");
        }
        else if (player2Score > player1Score)
        {
            //Debug.Log("Player 2 Wins!");
        }
        else
        {
            //Debug.Log("It's a tie!");
        }

        // Optionally reset the game or display UI
        ResetGame();
        */
    }

    public int GetPlayer1PuckCount()
    {
        return player1Parent.childCount;
    }

    public int GetPlayer2PuckCount()
    {
        return player2Parent.childCount;
    }

    public void UpdateInnerCount(string playerTag, int points)
    {
        if (playerTag == "Player1") player1Score += points;
        else if (playerTag == "Player2") player2Score += points;

        //Debug.Log($"Inner Circle: P1={player1Inner}, P2={player2Inner}");
    }

    public void UpdateOuterCount(string playerTag, int points)
    {
        if (playerTag == "Player1") player1Score += points;
        else if (playerTag == "Player2") player2Score += points;

        //Debug.Log($"Outer Circle: P1={player1Outer}, P2={player2Outer}");
    }

    public void ToggleCurrentPuckMoveMode()
    {
        if (currentPuck != null)
        {
            Slingshot slingshot = currentPuck.GetComponent<Slingshot>();
            if (slingshot != null)
            {
                slingshot.ToggleMoveMode();
            }
        }
    }
    

    public void ConfirmPuckMove()
    {
        if (currentPuck != null)
        {
            Slingshot slingshot = currentPuck.GetComponent<Slingshot>();
            if (slingshot != null)
            {
                slingshot.ConfirmPosition();
            }
        }
    }


}
