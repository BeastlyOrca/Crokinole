using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject SpawnPlayerPrefab;


    public void Spawn()
    {
        Vector3 spawnPos = new Vector3(-0.140000f, -4.979999f, -9.14999f);
        Instantiate(SpawnPlayerPrefab, spawnPos, Quaternion.identity);
        
    }
}
