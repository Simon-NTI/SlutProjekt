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
    int health = 10;
    int cashValue = 20;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        UpdateAgentDestination();
    }

    /// <summary>
    /// Subtracts the given value from the health of this enemy, then check if the enemy has died
    /// </summary>
    /// <param name="value">
    /// The damage to apply
    /// </param>
    private void RecieveDamage(object value)
    {
        try
        {
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
                if(targetObject.CompareTag("Player"))
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
        try
        {
            targetObject = (GameObject) value;
        }
        catch(Exception e)
        {
            print("This target was not a GameObject\n" + e.Message);
        }
    }
}
