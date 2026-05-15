using UnityEngine;
using UnityEngine.UI;


public class SafeAreaHandler : MonoBehaviour
{
    public RectTransform uiRootPanel; // Kéo thả RectTransform của uiRootPanel vào đây
    public RectTransform leftMarginPanel; // Kéo thả RectTransform của tấm che bên trái vào đây
    public RectTransform rightMarginPanel; // Kéo thả RectTransform của tấm che bên phải vào đây
    public RectTransform topMarginPanel; // Kéo thả RectTransform của tấm che bên trái vào đây
    public RectTransform bottomMarginPanel; // Kéo thả RectTransform của tấm che bên phải vào đây

    public CanvasScaler canvasScaler;
    public float scaleOffset = 200;  
    
    public float originalRootWidth = 1920f;
    public float originalRootHeight = 1080f; 

    void Start()
    {
        AdjustSafeArea();
    }

    void AdjustSafeArea()
    {
        // Lấy kích thước màn hình hiện tại
        float screenHeight = Screen.height > originalRootHeight ? originalRootHeight : Screen.height;

        if (Screen.width / Screen.height > (16f / 9f))
        {
            // Tính toán tỷ lệ mới cho background dựa trên chiều cao màn hình
            float scaleFactor = screenHeight / originalRootHeight;

            // Cập nhật chiều cao và chiều rộng của background để giữ tỷ lệ
            float newHeight = screenHeight;
            float newWidth = originalRootWidth * scaleFactor;
            
            uiRootPanel.sizeDelta = new Vector2(newWidth, newHeight);

            // Đặt vị trí của uiRootPanel (thường là ở giữa)
            uiRootPanel.anchoredPosition = Vector2.zero; // Đặt tâm uiRootPanel vào giữa Canvas
            
            float newMarginWidth = ((Screen.width - newWidth+scaleFactor) / 2f) + scaleOffset;
            if (newMarginWidth > 0)
            {
                leftMarginPanel.gameObject.SetActive(true);
                leftMarginPanel.sizeDelta = new Vector2(newMarginWidth, screenHeight);
                
                rightMarginPanel.gameObject.SetActive(true);
                rightMarginPanel.sizeDelta = new Vector2(newMarginWidth, screenHeight);
            }
            else
            {
                leftMarginPanel.gameObject.SetActive(false); // Ẩn nếu không cần
                rightMarginPanel.gameObject.SetActive(false); // Ẩn nếu không cần
            }
            
            canvasScaler.matchWidthOrHeight =  1;
        }
        else
        {
            /*// Tính toán tỷ lệ mới cho background dựa trên chiều cao màn hình
            float scaleFactor = screenWidth / originalRootWidth;

            // Cập nhật chiều cao và chiều rộng của background để giữ tỷ lệ
            float newWidth = screenWidth;
            float newHeight = originalRootHeight * scaleFactor;
            
            uiRootPanel.sizeDelta = new Vector2(newWidth, newHeight);

            uiRootPanel.anchoredPosition = Vector2.zero; // Đặt tâm uiRootPanel vào giữa Canvas
            
            float newMarginHeight = ((screenHeight - newHeight) / 2f) + scaleOffset;
            
            if (newMarginHeight > 0)
            {
                topMarginPanel.gameObject.SetActive(true);
                topMarginPanel.sizeDelta = new Vector2(screenWidth, newMarginHeight);
                
                bottomMarginPanel.gameObject.SetActive(true);
                bottomMarginPanel.sizeDelta = new Vector2(screenWidth, newMarginHeight);
            }
            else
            {
                topMarginPanel.gameObject.SetActive(false); // Ẩn nếu không cần
                bottomMarginPanel.gameObject.SetActive(false); // Ẩn nếu không cần
            }*/
            
            canvasScaler.matchWidthOrHeight =  0;
        }
    }
}
