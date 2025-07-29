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

    public Transform boardCenter; // Assign in Inspector
    public float innerCircleRadius = 2.5f; // Adjust for scoring area


    void Start()
    {
        //ResetGame();
    }

    void Update()
    {
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

    void EndTurn()
    {
      
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
