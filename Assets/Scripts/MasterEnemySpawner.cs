using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float spawnInterval; 
    [SerializeField] float currentCooldown; 
    [SerializeField] GameObject enemy;
    List<Vector3> spawnPositions = new List<Vector3>();
    void Awake()
    {
        currentCooldown = spawnInterval;
        try
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                print($"Position of child at index {i}: {gameObject.transform.GetChild(i).position}");
                spawnPositions.Add(gameObject.transform.GetChild(i).position);
            }
            print("Spawnposition count:" + spawnPositions.Count);
        }
        catch(Exception e)
        {
            print("Failed to get position of child\n" + e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(currentCooldown);
        CheckSpawnConditions();
    }

    private void CheckSpawnConditions()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            currentCooldown += spawnInterval;
            Instantiate(
                enemy, 
                spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Count)], 
                Quaternion.identity
            ).SendMessage("SetAgentTarget", player);
        }
    }
}
