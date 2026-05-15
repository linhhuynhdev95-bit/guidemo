using UnityEngine;
using DG.Tweening; // Nhớ thêm namespace này

public class PaperGentleFloatAnimation : MonoBehaviour
{
    [Header("Gentle Floating Animation Settings")]
    public float minFloatDuration = 2f; // Thời gian bay tối thiểu giữa các điểm
    public float maxFloatDuration = 4f; // Thời gian bay tối đa giữa các điểm
    public float maxHorizontalFloatDistance = 0.5f; // Khoảng cách bay ngang tối đa từ vị trí xuất phát
    public float maxVerticalFloatDistance = 0.3f; // Khoảng cách bay dọc tối đa từ vị trí xuất phát
    public Ease floatEaseType = Ease.InOutSine; // Kiểu easing cho chuyển động

    [Header("Rotation Settings (Z-axis only)")]
    public float minZRotationSpeed = 30f; // Tốc độ xoay trục Z tối thiểu (degrees/s)
    public float maxZRotationSpeed = 90f; // Tốc độ xoay trục Z tối đa (degrees/s)
    public float rotationChangeInterval = 2f; // Thời gian đổi hướng/tốc độ xoay (ngẫu nhiên)
    public float maxZRotationAngle = 45f; // Góc xoay Z tối đa từ góc ban đầu

    private Vector3 _initialPosition; // Vị trí ban đầu của tờ giấy
    private float _initialZRotation; // Góc xoay Z ban đầu của tờ giấy

    private Tween _currentMoveTween; // Tween di chuyển hiện tại
    private Tween _currentRotateTween; // Tween xoay hiện tại

    void Awake()
    {
        _initialPosition = transform.position;
        _initialZRotation = transform.localEulerAngles.z; // Lấy góc Z ban đầu
    }

    void Start()
    {
        StartFloatingAnimation();
        StartRotationAnimation(); // Bắt đầu xoay riêng biệt
    }

    void StartFloatingAnimation()
    {
        _currentMoveTween?.Kill(); // Dừng tween di chuyển cũ

        // Tạo vị trí đích ngẫu nhiên trong khoảng nhỏ quanh vị trí ban đầu
        Vector3 randomOffset = new Vector3(
            Random.Range(-maxHorizontalFloatDistance, maxHorizontalFloatDistance),
            Random.Range(-maxVerticalFloatDistance, maxVerticalFloatDistance),
            0 // Giữ nguyên trục Z để không bay vào sâu hay ra xa camera nếu là 2D hoặc UI
        );
        Vector3 targetPosition = _initialPosition + randomOffset;

        // Randomize thời gian bay
        float currentFloatDuration = Random.Range(minFloatDuration, maxFloatDuration);

        // Animate di chuyển đến vị trí đích
        _currentMoveTween = transform.DOMove(targetPosition, currentFloatDuration)
                                     .SetEase(floatEaseType)
                                     .OnComplete(StartFloatingAnimation); // Khi đến đích, gọi lại hàm này để chọn đích mới
    }

    void StartRotationAnimation()
    {
        _currentRotateTween?.Kill(); // Dừng xoay cũ

        // Chọn tốc độ và hướng xoay ngẫu nhiên
        float currentRotationSpeed = Random.Range(minZRotationSpeed, maxZRotationSpeed);
        // Chọn một góc Z ngẫu nhiên trong khoảng cho phép từ góc ban đầu
        float targetZRotation = _initialZRotation + Random.Range(-maxZRotationAngle, maxZRotationAngle);

        // Xoay đến góc Z mới trong một khoảng thời gian
        _currentRotateTween = transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, targetZRotation),
                                                rotationChangeInterval * Random.Range(0.8f, 1.2f)) // Thời gian xoay cũng ngẫu nhiên một chút
                                       .SetEase(Ease.InOutSine)
                                       .OnComplete(StartRotationAnimation); // Khi đến góc, chọn góc mới và xoay tiếp
    }

    // Đảm bảo dừng animation khi GameObject bị hủy hoặc script bị vô hiệu hóa
    void OnDisable()
    {
        _currentMoveTween?.Kill();
        _currentRotateTween?.Kill();
        // Tùy chọn: reset vị trí và xoay về ban đầu khi script dừng
        transform.position = _initialPosition;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _initialZRotation);
    }
}