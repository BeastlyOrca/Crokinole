using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject SpawnPlayerPrefab; // puck prefab
    public Transform player1Container;   // assign the Player1 GameObject in inspector

    public void Spawn()
    {
        Vector3 spawnPos = new Vector3(-0.14f, -4.98f, -9.15f);
        GameObject newPuck = Instantiate(SpawnPlayerPrefab, spawnPos, Quaternion.identity);
        newPuck.tag = "Player1";

        // how to get slingshot variables
        Slingshot puckScript = newPuck.GetComponent<Slingshot>();
        puckScript.owner = Slingshot.Player.Player1; // or Player2

        // Make the new puck a child of Player1 container
        if (player1Container != null)
        {
            newPuck.transform.SetParent(player1Container);
        }
    }
}
