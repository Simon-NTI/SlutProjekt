using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] short crossHairBaseOffset = 10;
    private TextMeshProUGUI moneyCounter;
    private (RectTransform left, RectTransform right, RectTransform upper, RectTransform lower) crosshair;
    private GameObject shop;
    [SerializeField] GameObject shopItemTemplate;
    [SerializeField] GameObject contentArea;
    private PlayerController playerController;
    private ShootController shootController;

    void Awake()
    {
        playerController = transform.GetComponentInParent<PlayerController>();
        shootController = transform.GetComponentInParent<ShootController>();

        shop = transform.Find("Shop").gameObject;

        Transform crosshairObject = transform.Find("Crosshair").gameObject.transform;

        crosshair.left = crosshairObject.Find("Left").gameObject.GetComponent<RectTransform>();
        crosshair.right = crosshairObject.Find("Right").gameObject.GetComponent<RectTransform>();
        crosshair.upper = crosshairObject.Find("Upper").gameObject.GetComponent<RectTransform>();
        crosshair.lower = crosshairObject.Find("Lower").gameObject.GetComponent<RectTransform>();

        moneyCounter = transform.Find("MoneyCounter").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        InitiateShop();
    }

    void Update()
    {
        UpdateShopState();
    }

    private void InitiateShop()
    {
        int weaponCount = Weapon.weapons.Length;
        for (int i = 0; i < weaponCount; i++)
        {
            contentArea.GetComponent<RectTransform>().sizeDelta = new(
                contentArea.GetComponent<RectTransform>().sizeDelta.x,
                weaponCount * 250
            );

            Vector2 sizeDelta = contentArea.GetComponent<RectTransform>().sizeDelta;
            GameObject shopItem = Instantiate(shopItemTemplate, contentArea.transform);


            shopItem.GetComponent<RectTransform>().anchoredPosition = new(
                sizeDelta.x,
                sizeDelta.y / 2f - (sizeDelta.y / weaponCount * i + 150)
            );

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

            Button button = shop.transform.GetComponent<Button>();

            shopItem.transform.Find("ButtonText").GetComponent<TextMeshProUGUI>().text = "Buy " + Weapon.weapons[i].name;
            shopItem.transform.Find("Description").GetComponent<TextMeshProUGUI>().text =
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

    private void BuyWeapon(int buttonIndex)
    {
        WeaponMetadata weapon = Weapon.weapons[buttonIndex];
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
