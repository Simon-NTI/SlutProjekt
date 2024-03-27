using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] short crosshairOffset = 10;
    RectTransform left, right, upper, lower;
    TextMeshProUGUI moneyCounter;
    void Awake()
    {
        Transform crosshair = transform.Find("Crosshair").gameObject.transform;
        left = crosshair.Find("Left").gameObject.GetComponent<RectTransform>();
        right = crosshair.Find("Right").gameObject.GetComponent<RectTransform>();
        upper = crosshair.Find("Upper").gameObject.GetComponent<RectTransform>();
        lower = crosshair.Find("Lower").gameObject.GetComponent<RectTransform>();
        moneyCounter = transform.Find("MoneyCounter").gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateMoneyCounter(object value)
    {
        moneyCounter.text = $"$$$: {value}";
    }

    private void UpdateCrosshair(object _value)
    {
        float value = (float)_value;

        left.localPosition = new(
            -crosshairOffset - value, 
            left.localPosition.y, 
            left.localPosition.z
            );

        right.localPosition = new(
            crosshairOffset + value, 
            right.localPosition.y, 
            right.localPosition.z
            );

        upper.localPosition = new(
            upper.localPosition.x, 
            crosshairOffset + value, 
            upper.localPosition.z
            );

        lower.localPosition = new(
            lower.localPosition.x, 
            -crosshairOffset - value, 
            lower.localPosition.z
            );
    }
}
