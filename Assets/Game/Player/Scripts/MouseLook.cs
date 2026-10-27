using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] Transform playerCamera;
    [SerializeField] float sensitivity = 0.1f;

    float xRotation;

    public void Look(Vector2 mouseDelta)
    {
        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        // Mirar arriba y abajo
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);

        // Girar el cuerpo a izquierda y derecha
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}