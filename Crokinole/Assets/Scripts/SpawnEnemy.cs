using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject SpawnEnemyPrefab;
    

    public void Spawn()
    {
        Vector3 spawnPos =  new Vector3(0.0199999996f, -4.9f, -2.66000009f);
        Instantiate(SpawnEnemyPrefab, spawnPos, Quaternion.identity);
    }
}
