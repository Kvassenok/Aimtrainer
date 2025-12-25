using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1f;  // Публичное для теста

    private float xRotation = 0f;

    void Start()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (GameManager.IsPaused) return;  // Не двигать при паузе

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100f;  // Горизонталь
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100f;  // Вертикаль

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Лимит вертикали

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // Вертикаль на камеру
        transform.parent.Rotate(Vector3.up * mouseX);  // Горизонталь на игроке (если камера child)
    }
}