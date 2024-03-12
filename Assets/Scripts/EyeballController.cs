using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyeBallController : MonoBehaviour
{
    GameObject headCamera;
    [SerializeField] Vector2 sensitivity = new(5, 5);
    [SerializeField] float pitchLimit = 80;
    float recoilDebt = 0;
    float cameraRotationX = 0;


    void Awake()
    {
        headCamera = GetComponentInChildren<Camera>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void OnLook(InputValue value)
    {
        Vector2 lookVector = value.Get<Vector2>();

        float degreesY = lookVector.x * sensitivity.x;
        float degreesX = lookVector.y * sensitivity.y;
        transform.Rotate(Vector3.up, degreesY);

        cameraRotationX += degreesX;
        cameraRotationX = Math.Clamp(cameraRotationX, -pitchLimit, pitchLimit);
        headCamera.transform.localEulerAngles = new(-cameraRotationX, 0, 0);
    }

    void Update()
    {
        
    }

    public void IncreaseRecoilDebt(object value)
    {
        try
        {
            recoilDebt += (int)value;
        }
        catch(Exception e)
        {
            print("Failed cast\n" + e.Message);
        }
    }
}
