using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] short baseOffset = 10;
    TextMeshProUGUI moneyCounter;
    (RectTransform left, RectTransform right, RectTransform upper, RectTransform lower) crosshair;
    GameObject shop;

    PlayerController playerController;
    //(Button pistol, Button smg, Button assaultRifle) shopButtons;

    void Awake()
    {
        playerController = transform.GetComponentInParent<PlayerController>();

        shop = transform.Find("Shop").gameObject;
        Transform crosshairObject = transform.Find("Crosshair").gameObject.transform;

        crosshair.left = crosshairObject.Find("Left").gameObject.GetComponent<RectTransform>();
        crosshair.right = crosshairObject.Find("Right").gameObject.GetComponent<RectTransform>();
        crosshair.upper = crosshairObject.Find("Upper").gameObject.GetComponent<RectTransform>();
        crosshair.lower = crosshairObject.Find("Lower").gameObject.GetComponent<RectTransform>();

        moneyCounter = transform.Find("MoneyCounter").gameObject.GetComponent<TextMeshProUGUI>();

        //shopButtons.pistol = shop.transform.Find("BuyPistol").gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShopState();
    }

    private void UpdateShopState()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale = 1;
                shop.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Time.timeScale = 0;
                shop.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void BuyPistol()
    {
        if(playerController.money >= 50)
        {
            print("You can afford the pistol :D");
        }
        else
        {
            print("You can not afford the pistol :(");
        }
    }

    private void UpdateMoneyCounter(object value)
    {
        moneyCounter.text = $"$$$: {value}";
    }

    /// <summary>
    /// Produces an offset on each crosshair arm
    /// </summary>
    /// <param name="_value"></param> <summary>
    /// 
    /// </summary>
    /// <param name="_value"></param>
    private void UpdateCrosshair(object _value)
    {
        float offsetValue = 0;
        try
        {
            offsetValue = (float)_value;
        }
        catch(Exception e)
        {
            print("Failed to cast value\n" + e.Message);
        }

        crosshair.left.localPosition = new(
            -baseOffset - offsetValue, 
            crosshair.left.localPosition.y, 
            crosshair.left.localPosition.z
            );

        crosshair.right.localPosition = new(
            baseOffset + offsetValue, 
            crosshair.right.localPosition.y, 
            crosshair.right.localPosition.z
            );

        crosshair.upper.localPosition = new(
            crosshair.upper.localPosition.x, 
            baseOffset + offsetValue, 
            crosshair.upper.localPosition.z
            );

        crosshair.lower.localPosition = new(
            crosshair.lower.localPosition.x, 
            -baseOffset - offsetValue, 
            crosshair.lower.localPosition.z
            );
    }
}
