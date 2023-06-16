using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerMoviment : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector3 movDirection;
    public float movSpeed = 3f;
    public float rotSpeed = 2f;
    private float jumpSpeed = 5;
    private float ySpeed;
    private float originalStepOffset;
    private float jumpButtonGracePeriod = 0.2f;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    [SerializeField] private Transform camTransform;
    private CharacterController characControll;
    private Animator anim;

    void Start()
    {
        characControll = GetComponent<CharacterController>();
        originalStepOffset = characControll.stepOffset;
        anim = GetComponent<Animator>();
    }

    void Update()
    {            
        Move();
    }

    void Move()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movDirection = new Vector3(horizontal, 0, vertical);
        movDirection = Quaternion.AngleAxis(camTransform.transform.eulerAngles.y, Vector3.up) * movDirection;
        float magnitude = Mathf.Clamp01(movDirection.magnitude) * movSpeed;
        movDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characControll.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characControll.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characControll.stepOffset = 0;
        }

        Vector3 velocity = movDirection * magnitude;
        velocity.y = ySpeed;

        characControll.Move(velocity * Time.deltaTime);

        if (movDirection != Vector3.zero)
        {
            anim.SetInteger("transition_controller", 1);
            transform.forward = movDirection;
            Quaternion toRotation = Quaternion.LookRotation(movDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetInteger("transition_controller", 0);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
