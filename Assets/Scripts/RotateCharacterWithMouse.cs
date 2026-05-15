using UnityEngine;

public class RotateCharacterWithMouse : MonoBehaviour
{
    public float rotationSpeed = 5f; // Tốc độ xoay
    private float yaw = 0f; // Góc xoay quanh trục Y (ngang)

    void Update()
    {
        // Kiểm tra xem nút chuột trái có đang được giữ không
        if (Input.GetMouseButton(0))
        {
            // Lấy sự thay đổi của trục X của chuột
            float mouseX = Input.GetAxis("Mouse X");

            // Cập nhật góc yaw dựa trên sự di chuyển của chuột
            yaw += mouseX * rotationSpeed;

            // Áp dụng phép quay vào transform của nhân vật
            // Quaternion.Euler tạo một phép quay từ các góc Euler
            transform.localRotation = Quaternion.Euler(0f, yaw, 0f);
        }
    }
}