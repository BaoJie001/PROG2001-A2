using UnityEngine;
using UnityEngine.UI;

public class DeerManager : MonoBehaviour
{
    public int healthCount;
    public int maxTomatoCount = 3;
    public static int tomatoItemCount;

    public AudioClip hurt;

    [SerializeField]
    private float moveSpeed = 5f; // 移动速度

    [SerializeField]
    private float turnSpeed = 180f; // 转向速度

    [SerializeField]
    private float jumpForce = 8f; // 跳跃力度

    [SerializeField]
    private float groundCheckDistance = 0.1f; // 地面检测距离

    [SerializeField]
    private LayerMask groundLayer; // 地面层级

    [SerializeField]
    private float walkThreshold = 0.1f; // 走路动画阈值

    [SerializeField]
    private float runThreshold = 0.5f; // 跑步动画阈值

    [Header("组件引用")]
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private Vector3 movement;
    private float turnInput;
    private float currentSpeed;

    public Text txtPassGame;
    public Text txtGameOver;
    public bool passGame;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.drag = 0.5f;
        rb.angularDrag = 0.5f;
        healthCount = 3;
        PanMainGame.Instance.UpadteTxtHeart(healthCount);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.Instance.PlayOneShotSFX(hurt);
            healthCount--;

            PanMainGame.Instance.UpadteTxtHeart(healthCount);
            if (healthCount <= 0)
            {
                healthCount = 0;
                txtGameOver.text = $"SCORE:{tomatoItemCount}";
                PanMainGame.Instance.ShowUIPanel(EUIPanel.GameOver);
            }
        }
    }

    void Update()
    {
        if (passGame)
            return;
        if (tomatoItemCount >= maxTomatoCount)
        {
            passGame = true;
            PanMainGame.Instance.ShowUIPanel(EUIPanel.GameWin);
            txtPassGame.text = $"{healthCount}\n{tomatoItemCount}";
        }

        PanMainGame.Instance.UpadteTxtCount(tomatoItemCount);
        PanMainGame.Instance.UpadteTxtHeart(healthCount);

        GetInput();

        CheckGrounded();

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void GetInput()
    {
        float moveInput = Input.GetAxis("Vertical");
        movement = transform.forward * moveInput * moveSpeed;

        currentSpeed = Mathf.Abs(moveInput);

        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        Vector3 velocity = movement;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    void Turn()
    {
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        animator.SetTrigger("Jump");
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;

        if (
            Physics.Raycast(
                rayStart,
                Vector3.down,
                out hit,
                groundCheckDistance + 0.1f,
                groundLayer
            )
        )
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void UpdateAnimation()
    {
        if (animator == null)
            return;
        animator.SetFloat("Speed", currentSpeed);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        Vector3 rayEnd = rayStart + Vector3.down * (groundCheckDistance + 0.1f);
        Gizmos.DrawLine(rayStart, rayEnd);
    }
}
