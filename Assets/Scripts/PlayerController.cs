using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject PlayerUI;
    int money = 0;
    [SerializeField] int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RecieveDamage()
    {

    }

    private void RecieveCash(object value)
    {
        try
        {
            money += (int)value;
        }
        catch(Exception e)
        {
            print("Failed to cast money value\n" + e.Message);
        }
        finally
        {
            PlayerUI.SendMessage("UpdateMoneyCounter", money);
        }
    }
}
