using UnityEngine;

public class PlacaAtencao : MonoBehaviour
{
    public Transform jogador;           // Referência ao jogador
    public Transform placaTransform;    // Transform da placa
    public float escalaMinima = 1f;     // Tamanho mínimo da placa
    public float escalaMaxima = 1.5f;   // Tamanho máximo da placa
    public float velocidadeOscilacao = 2f; // Velocidade da oscilação de tamanho

    private bool placaAtiva = false;    // Controle de ativação da placa

    void Update()
    {
        if (placaAtiva)
        {
            // Faz a placa olhar para o jogador
            Vector3 direcao = jogador.position - transform.position;
            direcao.y = 0; // Mantém a rotação apenas no plano horizontal
            transform.rotation = Quaternion.LookRotation(direcao);

            // Faz a placa aumentar e diminuir de tamanho
            float escala = Mathf.Lerp(escalaMinima, escalaMaxima, Mathf.PingPong(Time.time * velocidadeOscilacao, 1));
            placaTransform.localScale = new Vector3(escala, escala, escala);
        }
    }

    public void AtivarPlaca()
    {
        placaAtiva = true;
        placaTransform.gameObject.SetActive(true);
    }

    public void DesativarPlaca()
    {
        placaAtiva = false;
        placaTransform.gameObject.SetActive(false);
    }
}
