using UnityEngine;

public class OscillatingText : MonoBehaviour
{
    [Header("Movement Settings")]
    public float leftLimit = -5f; // Limite à esquerda
    public float rightLimit = 5f; // Limite à direita
    public float speed = 2f; // Velocidade do movimento

    private Vector3 startPosition; // Posição inicial
    private int direction = 1; // Direção do movimento (1 = direita, -1 = esquerda)

    void Start()
    {
        // Salva a posição inicial
        startPosition = transform.localPosition;
    }

    void Update()
    {
        // Move o texto horizontalmente
        transform.localPosition += new Vector3(speed * direction * Time.deltaTime, 0, 0);

        // Verifica os limites e inverte a direção
        if (transform.localPosition.x >= startPosition.x + rightLimit)
        {
            direction = -1; // Muda para esquerda
        }
        else if (transform.localPosition.x <= startPosition.x + leftLimit)
        {
            direction = 1; // Muda para direita
        }
    }
}
