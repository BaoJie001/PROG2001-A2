using UnityEngine;

public class SheepDarkController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float rotateSpeed;
    public float rayDistance;

    public int maxHP;

    public int ScoreCount;

    public Transform appleRoot;
    public int passCount;

    public AudioClip getHutSfx;
    private int curHP;

    public int CurHP => curHP;
    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    private bool isDeath = false;
    public static SheepDarkController Instance;

    void Awake() => Instance = this;

    void OnDestroy() => Instance = null;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.freezeRotation = true;
        passCount = appleRoot.childCount;
        curHP = maxHP;
        PanMainGame.Instance.UpadteTxtHeart(curHP);
        PanMainGame.Instance.UpadteTxtCount(ScoreCount);
    }

    void Update()
    {
        if (isDeath)
            return;

        CheckGrounded();

        HandleMovement();

        HandleJump();
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        AudioManager.Instance.PlayOneShotSFX(getHutSfx);
        PanMainGame.Instance.UpadteTxtHeart(curHP);
        if (curHP <= 0)
        {
            curHP = 0;
            isDeath = true;
            PanMainGame.Instance.UpadteTxtHeart(curHP);
            PanMainGame.Instance.ShowUIPanel(EUIPanel.GameOver);
            return;
        }
    }

    void CheckGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
            isGrounded = hit.collider.tag == "Ground";
        else
            isGrounded = false;
        anim.SetBool("IsOnGround", isGrounded);
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        anim.SetFloat("YInput", verticalInput);

        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        float rotation = horizontalInput * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("IsOnGround", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            TakeDamage(1);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }

    internal void AddCount(int v)
    {
        ScoreCount += v;
        if (ScoreCount >= passCount)
        {
            PanMainGame.Instance.ShowUIPanel(EUIPanel.GameWin);
            return;
        }
        PanMainGame.Instance.UpadteTxtCount(ScoreCount);
    }
}
