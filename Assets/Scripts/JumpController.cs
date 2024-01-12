using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private InputActionReference jumpButton;
    private Vector3 playerVelocity;
    [SerializeField] private float jumpHeight = 1.0f;
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
        if (!Physics.Raycast(transform.position, Vector3.down, out var hit)) return;

        if (hit.distance <= 0.65) 
        {

            playerVelocity.y = 0.0f;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    void Update()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
} 