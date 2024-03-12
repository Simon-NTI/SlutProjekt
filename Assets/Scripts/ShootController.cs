using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] GameObject gunRay;
    GameObject camera;
    Vector3? debugSphereCenter;
    [SerializeField] GameObject debugSpherePrefab;
    GameObject debugSphere;
    [SerializeField] bool debugMode, drawPlayerRayImpactPoint, drawPlayerRay;
    [SerializeField] float debugSphereRadius = 0.2f;


     [SerializeField] float damage;
    void Awake()
    {
        camera = gameObject.GetComponentInChildren<Camera>().gameObject;
        if(debugMode)
        {
            debugSphere = Instantiate(debugSpherePrefab);
        }

        Weapons weapons = gameObject.GetComponent<Weapons>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProducePlayerRay();
    }

    private void ProducePlayerRay()
    {
        if (debugMode && drawPlayerRay)
        {
            Debug.DrawLine(camera.transform.position, camera.transform.position + 500 * camera.transform.forward);
        }

        Physics.Raycast(
            camera.transform.position,
            camera.transform.forward,
            out RaycastHit hit
        );

        if (hit.collider != null)
        {
            debugSphereCenter = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                LineRenderer line = Instantiate(gunRay).GetComponent<LineRenderer>();
                line.SetPosition(0, camera.transform.position);
                line.SetPosition(1, hit.point);

                if(hit.collider.CompareTag("enemy"))
                {
                    hit.collider.transform.gameObject.SendMessage("RecieveDamage", damage);
                    gameObject.SendMessage("IncreaseRecoilDebt",  SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else
        {
            debugSphereCenter = null;
        }

        if (debugSphereCenter != null)
        {
            if (debugMode && drawPlayerRayImpactPoint)
            {
                debugSphere.transform.position = (Vector3)debugSphereCenter;
            }
        }
    }
}