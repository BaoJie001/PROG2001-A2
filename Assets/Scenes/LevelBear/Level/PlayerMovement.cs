using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private float runSpeed = 6f;

    [SerializeField]
    private float rotationSpeed = 120f;

    [SerializeField]
    private float jumpHeight = 2f;

    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private Transform cameraTarget;

    [SerializeField]
    private float cameraDistance = 5f;

    [SerializeField]
    private float cameraHeight = 2f;

    [SerializeField]
    private float cameraSmoothSpeed = 5f;

    [SerializeField]
    private string speedParameter = "Speed";

    [SerializeField]
    private string jumpParameter = "Jump";

    [SerializeField]
    private string groundedParameter = "Grounded";

    private CharacterController controller;
    private Animator animator;
    private Camera mainCamera;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isRunning;
    private float verticalVelocity;
    private float rotationVelocity;

    private Vector2 inputAxis;
    private bool jumpInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        if (cameraTarget == null)
        {
            cameraTarget = new GameObject("CameraTarget").transform;
            cameraTarget.SetParent(transform);
            cameraTarget.localPosition = new Vector3(0, cameraHeight, 0);
        }
    }

    void Update()
    {
        GetInput();
        HandleMovement();
        HandleCamera();
        UpdateAnimations();
    }

    private void GetInput()
    {
        inputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        isRunning = Input.GetKey(KeyCode.LeftShift);

        jumpInput = Input.GetButtonDown("Jump");
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (
            cameraForward * inputAxis.y + cameraRight * inputAxis.x
        ).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref rotationVelocity,
                0.2f
            );
            transform.rotation = Quaternion.Euler(0, angle, 0);

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }

        if (jumpInput && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger(jumpParameter);
        }

        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    private void HandleCamera()
    {
        if (mainCamera == null)
            return;

        Vector3 cameraOffset = -transform.forward * cameraDistance + Vector3.up * cameraHeight;
        Vector3 targetPosition = cameraTarget.position + cameraOffset;

        mainCamera.transform.position = Vector3.Lerp(
            mainCamera.transform.position,
            targetPosition,
            cameraSmoothSpeed * Time.deltaTime
        );

        mainCamera.transform.LookAt(cameraTarget.position);
    }

    private void UpdateAnimations()
    {
        float speed = inputAxis.magnitude;
        if (isRunning && speed > 0)
        {
            speed = Mathf.Clamp(speed * 2f, 0, 1f); // 跑步时速度加倍
        }

        animator.SetFloat(speedParameter, speed);
        animator.SetBool(groundedParameter, isGrounded);
    }

    public void SetMovementEnabled(bool enabled)
    {
        enabled = enabled;
    }

    public bool IsMoving()
    {
        return inputAxis.magnitude > 0.1f;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
