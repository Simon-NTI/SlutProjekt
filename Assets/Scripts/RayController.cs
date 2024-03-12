using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    [SerializeField] float startTimeToLive = 3f;
    float timeToLive;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        timeToLive = startTimeToLive;
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0)
        {
            Destroy(gameObject);
        }

        Color color = new Color(
        lineRenderer.material.color.r, 
        lineRenderer.material.color.g, 
        lineRenderer.material.color.b, 
        timeToLive / startTimeToLive
        );
        
        lineRenderer.material.SetColor("_Color", color);
    }
}
