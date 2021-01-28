using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    State state = State.standing;
    float speedModifier = 1;

    private CharacterController controller;
    private new CapsuleCollider collider;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        collider = GetComponent<CapsuleCollider>();
    }

    private bool IsGrounded() => Physics.CheckCapsule(collider.bounds.min, new Vector3(collider.bounds.min.x, collider.bounds.min.y - 0.2f, collider.bounds.min.z), 0.1f);

    void Update()
    {
        ChangeState();

        if (IsGrounded())
        {
            if(velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= 2;
        }

        controller.Move(move * speed * speedModifier * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void ChangeState()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (state == State.crouch)
            {
                state = State.standing;
                controller.transform.localScale = new Vector3(1, 1, 1);
                speedModifier = 1;
            }
            else
            {
                state = State.crouch;
                controller.transform.localScale = new Vector3(1, 0.5f, 1);
                speedModifier = 0.5f;
            }
        }
        if (Input.GetButtonDown("Prone"))
        {
            if (state == State.prone)
            {
                state = State.standing;
                controller.transform.localScale = new Vector3(1, 1, 1);
                speedModifier = 1;
            }
            else
            {
                state = State.prone;
                controller.transform.localScale = new Vector3(1, 0.1f, 1);
                speedModifier = 0.2f;
            }
        }
    }
}

enum State
{
    standing,
    crouch,
    prone
}
