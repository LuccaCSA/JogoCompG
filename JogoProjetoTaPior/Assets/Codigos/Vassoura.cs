using UnityEngine;

public class Vassoura : MonoBehaviour
{
    public Camera playerCamera; // Referência à câmera do jogador
    private Lixo lixoAtual;     // Referência ao lixo que está sendo limpo atualmente

    void Update()
    {
        if (Input.GetButton("Fire1")) // Botão esquerdo do mouse pressionado
        {
            RaycastHit hit;

            // Cria um raycast da câmera do jogador
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
            {
                // Verifica se o objeto atingido é um lixo
                Lixo lixo = hit.transform.GetComponent<Lixo>();
                if (lixo != null && lixo != lixoAtual)
                {
                    lixoAtual = lixo;
                    lixoAtual.LimparLixo(); // Inicia a limpeza do lixo
                }
            }
        }
        else
        {
            lixoAtual = null; // Reseta o lixoAtual quando o botão não está pressionado
        }
    }
}
