using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target; // 玩家对象

    [Header("摄像机设置")]
    public float distance = 5f; // 摄像机与玩家的距离
    public float height = 2f; // 摄像机高度
    public float smoothSpeed = 5f; // 跟随平滑度
    public float rotationSpeed = 3f; // 旋转速度

    [Header("鼠标控制")]
    public bool enableMouseControl = true;
    public float mouseSensitivity = 2f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 80f;

    private Vector3 offset;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void Start()
    {
        // 如果没有指定目标，尝试查找玩家对象
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        // 计算初始偏移量
        if (target != null)
        {
            offset = transform.position - target.position;
        }

        // 初始化摄像机角度
        Vector3 angles = transform.eulerAngles;
        currentRotationX = angles.y;
        currentRotationY = angles.x;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // 鼠标控制摄像机旋转
        if (enableMouseControl && Input.GetMouseButton(1)) // 右键按住旋转
        {
            HandleMouseRotation();
        }

        // 计算目标位置
        Vector3 targetPosition = CalculateTargetPosition();

        // 平滑移动摄像机
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // 摄像机始终看向玩家
        transform.LookAt(target.position + Vector3.up * height * 0.5f);
    }

    void HandleMouseRotation()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 更新旋转角度
        currentRotationX += mouseX;
        currentRotationY -= mouseY;

        // 限制垂直角度
        currentRotationY = Mathf.Clamp(currentRotationY, minVerticalAngle, maxVerticalAngle);
    }

    Vector3 CalculateTargetPosition()
    {
        // 计算旋转后的偏移量
        Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);
        Vector3 rotatedOffset = rotation * new Vector3(0, height, -distance);

        // 返回目标位置（玩家位置 + 旋转后的偏移量）
        return target.position + rotatedOffset;
    }

    // 公共方法：重置摄像机角度
    public void ResetCameraAngle()
    {
        currentRotationX = target.eulerAngles.y;
        currentRotationY = 15f; // 默认俯视角度
    }

    // 公共方法：设置跟随目标
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }
}
