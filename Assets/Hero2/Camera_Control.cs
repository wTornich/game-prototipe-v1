using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Camera_Control : MonoBehaviour
{

    public Transform Player;
    public float YOffset;
    public float Sensibility;
    public float LimitRotation;

    float rotX;
    float rotY;

    void Start()
    {
        
    }

    
    void Update()
    {
        float Mouse_X = Input.GetAxis("Mouse Y");
        float Mouse_Y = Input.GetAxis("Mouse X");

        rotX -= Mouse_X * Sensibility * Time.deltaTime;
        rotY += Mouse_Y * Sensibility * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -LimitRotation, LimitRotation);

        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    private void LateUpdate()
    {
        transform.position = Player.position + Player.up * YOffset;
    }

}
