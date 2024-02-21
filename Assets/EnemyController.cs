using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    int health = 10;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RecieveDamage(object[] values)
    {
        try
        {
            print(values[0]);
            health -= (int)values[0];
        }
        catch(Exception e)
        {
            print("Failed cast");
            print(e.Message);
            return;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void GetTargetPosition(object[] values)
    {
        try
        {
            agent.destination = (Vector3)values[0];
        }
        catch(Exception e)
        {
            print("Failed to set agent destination");
            print(e.Message);
        }
    }
}
