using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hero_Moviment : MonoBehaviour
{

    private CharacterController controller;
    private Animator anim;
    [SerializeField] public Transform cameraTransform;

    public float speed;
    public float gravity;
    public float rotacionSpeed;

 

    private float rotacion;
    private Vector3 moviment;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {


        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moviment = Vector3.forward * speed;
                anim.SetInteger("transition_controller", 1);
               
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                moviment = Vector3.zero;
                anim.SetInteger("transition_controller", 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                moviment = Vector3.back * speed;
                anim.SetInteger("transition_controller", 2);
                
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                moviment = Vector3.zero;
                anim.SetInteger("transition_controller", 0);
            }

        }

        //cameraTransform = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);

        rotacion += Input.GetAxis("Horizontal") * rotacionSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotacion, 0);

        moviment.y -= gravity * Time.deltaTime;
        moviment = transform.TransformDirection(moviment);

        
        
        controller.Move(moviment * Time.deltaTime);

    }
}
