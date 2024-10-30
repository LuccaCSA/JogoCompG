using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilidade do mouse
    public Transform npcBody;             // Referência ao corpo do NPC

    private float xRotation = 0f;

    void Start()
    {
        // Travar o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Captura a entrada do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Controle de rotação vertical (pitch) da câmera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Limita a rotação para evitar girar demais

        // Aplica a rotação na câmera (apenas no eixo X para inclinação)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotaciona o corpo do NPC (yaw)
        npcBody.Rotate(Vector3.up * mouseX);
    }
}
