using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Torna o cursor visível
        Cursor.visible = true;

        // Garante que o cursor não está bloqueado
        Cursor.lockState = CursorLockMode.None;
    }
}
