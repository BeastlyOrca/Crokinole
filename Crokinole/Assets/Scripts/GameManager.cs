using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for easy access

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
    private GameObject currentPuck; // Keep track of the active puck



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

        Debug.Log("Turn ended. Next turn: " + (isPlayer1Turn ? "Player 1" : "Player 2"));

        // ✅ Check if game is over
        if (p1PucksRemaining <= 0 && p2PucksRemaining <= 0)
        {
            EndGame();
        }
    }


    void EndGame()
    {
        Debug.Log("Game Over!");

        if (player1Score > player2Score)
        {
            Debug.Log("Player 1 Wins!");
        }
        else if (player2Score > player1Score)
        {
            Debug.Log("Player 2 Wins!");
        }
        else
        {
            Debug.Log("It's a tie!");
        }

        // Optionally reset the game or display UI
        ResetGame();
    }
}
