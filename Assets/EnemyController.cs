using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] int health = 10;
    [SerializeField] int damage = 8;
    [SerializeField] int cashValue = 10;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAgentDestination();
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
            player.SendMessage("RecieveCash", cashValue);
        }
    }

    private void UpdateAgentDestination()
    {
        try
        {
            agent.destination = player.transform.position;
        }
        catch(Exception e)
        {
            print("Failed to set agent destination");
            print(e.Message);
        }
    }
}
