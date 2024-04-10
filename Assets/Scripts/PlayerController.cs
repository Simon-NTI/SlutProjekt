using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject PlayerUI;
    public int money = 0;
    [SerializeField] int health = 100;

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
