using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private InputActionReference jumpButton;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float jumpHeight = 10.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        jumpButton.action.performed += OnJump;
    }

    private void OnDisable()
    {
        jumpButton.action.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (groundedPlayer) 
        {   
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}