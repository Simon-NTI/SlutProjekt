using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] short crosshairOffset = 10;
    RectTransform left, right, upper, lower;
    void Awake()
    {
        Transform crosshair = transform.Find("Crosshair").gameObject.transform;
        left = crosshair.Find("Left").gameObject.GetComponent<RectTransform>();
        right = crosshair.Find("Right").gameObject.GetComponent<RectTransform>();
        upper = crosshair.Find("Upper").gameObject.GetComponent<RectTransform>();
        lower = crosshair.Find("Lower").gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCrosshair(object value)
    {
        left.localPosition = new(-crosshairOffset - (float)value, left.localPosition.y, left.localPosition.z);
        right.localPosition = new(crosshairOffset + (float)value, right.localPosition.y, right.localPosition.z);
        upper.localPosition = new(upper.localPosition.x, crosshairOffset + (float)value, upper.localPosition.z);
        lower.localPosition = new(lower.localPosition.x, -crosshairOffset - (float)value, lower.localPosition.z);
    }
}
