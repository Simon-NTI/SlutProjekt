using System;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] short crossHairBaseOffset = 10;
    TextMeshProUGUI moneyCounter;
    (RectTransform left, RectTransform right, RectTransform upper, RectTransform lower) crosshair;
    GameObject shop;
    PlayerController playerController;
    ShootController shootController;

    void Awake()
    {
        playerController = transform.GetComponentInParent<PlayerController>();
        shootController = transform.GetComponentInParent<ShootController>();

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

    private void Start() 
    {
        InitiateShop();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShopState();
    }

    private void InitiateShop()
    {
        for (int i = 0; i < Weapon.weapons.Length; i++)
        {
            string recoilRating;
            if (Weapon.weapons[i].recoil < 10)
            {
                recoilRating = "Low";
            }
            else if(Weapon.weapons[i].recoil > 19)
            {
                recoilRating = "High";
            }
            else
            {
                recoilRating = "Medium";
            }

            Transform button = shop.transform.Find("Buy" + Weapon.weapons[i].name.Replace("_", ""));
            if(button == null)
            {
                print($"Could not find corresponding button for weapon {Weapon.weapons[i].name}");
                continue;
            }

            button.gameObject.SetActive(true);
            button.Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "Buy " + Weapon.weapons[i].name;
            button.Find("Description").GetComponent<TextMeshProUGUI>().text =
                $"{Weapon.weapons[i].name.Replace("_", "")}:\n"
                + $"Damage: {Weapon.weapons[i].damage}\n"
                + $"Fire-rate: {Math.Round(1f / Weapon.weapons[i].fireDelay, 1)}\n"
                + $"Recoil: {recoilRating}\n"
                + $"Price: {Weapon.weapons[i].price}";
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


    private void BuyWeapon(WeaponMetadata weapon)
    {
        if(playerController.money >= weapon.price && !weapon.bought)
        {
            playerController.money -= weapon.price;
            weapon.bought = true;
        }
        else if(weapon.bought)
        {
            shootController.SendMessage("EquipWeapon", weapon);
        }
        else
        {
            print($"You can not afford the {weapon.name} :(");
        }
    }
    public void BuyPistol() => BuyWeapon(Weapon.pistol);
    public void BuySMG() => BuyWeapon(Weapon.smg);
    public void BuySniper() => BuyWeapon(Weapon.sniper);
    public void BuyAssaultRifle() => BuyWeapon(Weapon.assaultRifle);

    private void UpdateMoneyCounter(object value) => moneyCounter.text = $"$$$: {value}";

    /// <summary>
    /// Produces an offset on each crosshair arm
    /// </summary>
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
            -crossHairBaseOffset - offsetValue, 
            crosshair.left.localPosition.y, 
            crosshair.left.localPosition.z
            );

        crosshair.right.localPosition = new(
            crossHairBaseOffset + offsetValue, 
            crosshair.right.localPosition.y, 
            crosshair.right.localPosition.z
            );

        crosshair.upper.localPosition = new(
            crosshair.upper.localPosition.x, 
            crossHairBaseOffset + offsetValue, 
            crosshair.upper.localPosition.z
            );

        crosshair.lower.localPosition = new(
            crosshair.lower.localPosition.x, 
            -crossHairBaseOffset - offsetValue, 
            crosshair.lower.localPosition.z
            );
    }
}
