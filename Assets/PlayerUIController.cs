using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    RectTransform left, right, upper, lower;
    void Awake()
    {
        left = transform.Find("Left").gameObject.GetComponent<RectTransform>();
        right = transform.Find("Right").gameObject.GetComponent<RectTransform>();
        upper = transform.Find("Upper").gameObject.GetComponent<RectTransform>();
        lower = transform.Find("Lower").gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCrosshair(object value)
    {
        print("I RAN!");
        try
        {
            left.position = new(25 + (float)value, left.position.y, left.position.y);
        }
        catch
        {

        }
    }
}
