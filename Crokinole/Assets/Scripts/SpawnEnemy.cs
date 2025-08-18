using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject SpawnEnemyPrefab;
    public Transform player2Container;   // assign the Player1 GameObject in inspector


    public void Spawn()
    {
        Vector3 spawnPos = new Vector3(0.0199999996f, -4.9f, -2.66000009f);
        GameObject newPuck = Instantiate(SpawnEnemyPrefab, spawnPos, Quaternion.identity);
        newPuck.tag = "Player2";

        // how to get slingshot variables
        Slingshot puckScript = newPuck.GetComponent<Slingshot>();
        puckScript.owner = Slingshot.Player.Player2; // or Player2


        // Make the new puck a child of Player1 container
        if (player2Container != null)
        {
            newPuck.transform.SetParent(player2Container);
        }
    }
}
