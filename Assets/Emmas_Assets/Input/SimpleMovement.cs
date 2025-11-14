using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviour
{
    public bool ableToMove = true;
    public float speed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    public GameObject legs; //to keep it in the right spot
    private Transform legsPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        legsPos = legs.transform;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
        legs.transform.position = legsPos.position;
    }


}