using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening; // Đảm bảo bạn đã import DOTween vào project của mình

public class UIButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Animation Settings")]
    public float scaleFactor = 1.1f;
    public float clickScaleFactor = 0.9f;
    public float animationDuration = 0.15f;
    public Ease easeType = Ease.OutBack;

    [Header("Idle Animation Settings")]
    public bool enableIdleAnimation = true; // Bật/tắt idle animation
    public float idleScaleAmount = 0.05f; // Lượng phóng to/thu nhỏ nhẹ khi idle (ví dụ: 5%)
    public float idleAnimationDuration = 1.5f; // Thời gian một chu kỳ của idle animation
    public Ease idleEaseType = Ease.InOutSine; // Kiểu easing cho idle animation (thường là nhẹ nhàng hơn)

    [Header("Color Settings (Optional)")]
    public bool changeColorOnHover = true;
    public Color hoverColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    public Color clickColor = new Color(0.6f, 0.6f, 0.6f, 1f);

    private Vector3 _originalScale;
    private Color _originalColor;
    private Image _buttonImage;
    private Tween _currentTween; // Cho hover, click
    private Tween _idleTween; // Cho idle animation

    void Awake()
    {
        _buttonImage = GetComponent<Image>();
        if (_buttonImage == null)
        {
            Debug.LogError("UIButtonAnimator requires an Image component on the same GameObject.", this);
            enabled = false;
            return;
        }

        _originalScale = transform.localScale;
        _originalColor = _buttonImage.color;
    }

    void OnEnable()
    {
        // Bắt đầu idle animation khi script được bật
        StartIdleAnimation();
    }

    // Hàm bắt đầu idle animation
    void StartIdleAnimation()
    {
        if (!enableIdleAnimation) return;

        // Dừng idle tween cũ nếu có
        _idleTween?.Kill();

        // Tạo một sequence để lặp lại animation
        _idleTween = DOTween.Sequence()
            .Append(transform.DOScale(_originalScale * (1f + idleScaleAmount), idleAnimationDuration / 2f).SetEase(idleEaseType))
            .Append(transform.DOScale(_originalScale * (1f - idleScaleAmount), idleAnimationDuration / 2f).SetEase(idleEaseType))
            .SetLoops(-1, LoopType.Yoyo) // Lặp vô hạn, đi tới rồi lùi lại
            .SetId("IdleAnimation") // Đặt ID để dễ quản lý
            .Play();
    }

    // Hàm dừng idle animation
    void StopIdleAnimation()
    {
        _idleTween?.Kill();
        // Đảm bảo button trở về scale gốc sau khi dừng idle
        transform.DOScale(_originalScale, animationDuration).SetEase(easeType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Dừng idle animation và mọi tween hiện tại
        StopIdleAnimation(); // Dừng idle
        _currentTween?.Kill();

        // Animate Scale
        _currentTween = transform.DOScale(_originalScale * scaleFactor, animationDuration)
                                 .SetEase(easeType);

        // Animate Color (nếu được bật)
        if (changeColorOnHover)
        {
            _buttonImage.DOColor(hoverColor, animationDuration);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Dừng tween hiện tại
        _currentTween?.Kill();

        // Animate Scale về lại original
        _currentTween = transform.DOScale(_originalScale, animationDuration)
                                 .SetEase(easeType)
                                 .OnComplete(StartIdleAnimation); // Bắt đầu lại idle animation khi animation exit hoàn tất

        // Animate Color về lại original (nếu được bật)
        if (changeColorOnHover)
        {
            _buttonImage.DOColor(_originalColor, animationDuration);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Dừng idle animation và mọi tween hiện tại
        StopIdleAnimation();
        _currentTween?.Kill();

        // Animate Scale thu nhỏ
        _currentTween = transform.DOScale(_originalScale * clickScaleFactor, animationDuration)
                                 .SetEase(easeType);

        // Animate Color (nếu được bật)
        if (changeColorOnHover)
        {
            _buttonImage.DOColor(clickColor, animationDuration);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Dừng tween hiện tại
        _currentTween?.Kill();

        // Kiểm tra xem con trỏ chuột còn đang ở trên button không
        if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
        {
            // Vẫn đang hover
            _currentTween = transform.DOScale(_originalScale * scaleFactor, animationDuration)
                                     .SetEase(easeType);
            if (changeColorOnHover)
            {
                _buttonImage.DOColor(hoverColor, animationDuration);
            }
        }
        else
        {
            // Đã rời khỏi button
            _currentTween = transform.DOScale(_originalScale, animationDuration)
                                     .SetEase(easeType)
                                     .OnComplete(StartIdleAnimation); // Bắt đầu lại idle animation
            if (changeColorOnHover)
            {
                _buttonImage.DOColor(_originalColor, animationDuration);
            }
        }
    }

    void OnDisable()
    {
        _currentTween?.Kill(); // Đảm bảo dừng mọi tween đang chạy
        _idleTween?.Kill();    // Đảm bảo dừng idle tween
        transform.localScale = _originalScale; // Reset scale về mặc định
        if (_buttonImage != null)
        {
            _buttonImage.color = _originalColor; // Reset màu về mặc định
        }
    }
}