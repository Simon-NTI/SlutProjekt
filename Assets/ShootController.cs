using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    GameObject camera;
    void Awake()
    {
        camera = gameObject.GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        print("Camera position: " + camera.transform.position);
        print("Camera target position: " + camera.transform.position + 500 * camera.transform.rotation.eulerAngles.normalized);

        Debug.DrawLine(
            camera.transform.position, 
            camera.transform.position + 500 * camera.transform.rotation.eulerAngles.normalized, 
            Color.blue, 
            0.5f, 
            false
            );
    }
}
