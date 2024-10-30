using UnityEngine;

public class Movimento : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento

    void Update()
    {
        // Captura a entrada do jogador nos eixos horizontal e vertical
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Cria um vetor de movimento baseado na entrada do jogador
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Aplica o movimento ao jogador
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
