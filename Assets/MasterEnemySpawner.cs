using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval; 
    [SerializeField] float currentCooldown; 
    [SerializeField] GameObject enemy;
    List<Vector3> spawnPositions;
    void Awake()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            spawnPositions.Add(gameObject.transform.GetChild(i).position);
        }
        currentCooldown = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            currentCooldown += spawnInterval;
            Instantiate(enemy);
        }
    }
}
