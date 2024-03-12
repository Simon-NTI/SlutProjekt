using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject targetObject;
    [SerializeField] int health = 10;
    [SerializeField] int damage = 8;
    [SerializeField] int cashValue = 10;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        UpdateAgentDestination();
    }

    private void RecieveDamage(object value)
    {
        try
        {
            //print(values[0]);
            health -= (int)value;
        }
        catch(Exception e)
        {
            print("Failed cast\n" + e.Message);
            return;
        }
        finally
        {
            if(health <= 0)
            {
                Destroy(gameObject);
                if(targetObject.CompareTag("player"))
                {
                    targetObject.SendMessage("RecieveCash", cashValue);
                }
            }
        }
    }

    private void UpdateAgentDestination()
    {
        try
        {
            agent.destination = targetObject.transform.position;
        }
        catch(Exception e)
        {
            print("Failed to set agent destination\n"+ e.Message);
        }
    }

    private void SetAgentTarget(object value)
    {
        targetObject = (GameObject) value;
    }
}
