using System;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] short baseOffset = 10;
    TextMeshProUGUI moneyCounter;
    (RectTransform left, RectTransform right, RectTransform upper, RectTransform lower) crosshair;
    GameObject shop;

    (string name, int damage, float fireDelay, float recoil, bool automaticFire, int price, bool bought)[] shopItems
    = new (string name, int damage, float fireDelay, float recoil, bool automaticFire, int price, bool bought)[4] 
    {
        ("Pistol", 3, 0.65f, 10, false, 100, true), 
        ("SMG", 2, 0.3f, 6, true, 150, false), 
        ("Sniper", 5, 1f, 20, false, 350, false), 
        ("Assault_Rifle", 4, 0.5f, 10, true, 600, false)
    };

    PlayerController playerController;

    void Awake()
    {
        playerController = transform.GetComponentInParent<PlayerController>();

        shop = transform.Find("Shop").gameObject;
        //Button button = shop.transform.Find("BuyPistol").gameObject.GetComponent<Button>();

        Transform crosshairObject = transform.Find("Crosshair").gameObject.transform;

        crosshair.left = crosshairObject.Find("Left").gameObject.GetComponent<RectTransform>();
        crosshair.right = crosshairObject.Find("Right").gameObject.GetComponent<RectTransform>();
        crosshair.upper = crosshairObject.Find("Upper").gameObject.GetComponent<RectTransform>();
        crosshair.lower = crosshairObject.Find("Lower").gameObject.GetComponent<RectTransform>();

        moneyCounter = transform.Find("MoneyCounter").gameObject.GetComponent<TextMeshProUGUI>();

        //shopButtons.pistol = shop.transform.Find("BuyPistol").gameObject.GetComponent<Button>();
    }

    private void Start() {
        InitiateShop();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShopState();
    }

    private void InitiateShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            string recoilRating;
            if (shopItems[i].recoil < 10)
            {
                recoilRating = "Low";
            }
            else if(shopItems[i].recoil > 19)
            {
                recoilRating = "High";
            }
            else
            {
                recoilRating = "Medium";
            }

            Transform button = shop.transform.Find("Buy" + Regex.Replace(shopItems[i].name, "_", ""));
            if(button == null)
            {
                print($"Could not find corresponding button for weapon {shopItems[i].name}");
                continue;
            }

            button.gameObject.SetActive(true);
            button.Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "Buy " + shopItems[i].name;
            button.Find("Description").GetComponent<TextMeshProUGUI>().text =
                $"{Regex.Replace(shopItems[i].name, "\\s", " ")}:\n"
                + $"Damage: {shopItems[i].damage}\n"
                + $"Fire-rate: {Math.Round(1f / shopItems[i].fireDelay, 1)}\n"
                + $"Recoil: {recoilRating}\n"
                + $"Price: {shopItems[0].price}";
        }
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

    private void UpdateMoneyCounter(object value) => moneyCounter.text = $"$$$: {value}";

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
