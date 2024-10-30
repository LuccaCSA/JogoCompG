using UnityEngine;

public class CaminhadaNPC : MonoBehaviour
{
    public float distanciaDeCaminhada = 5f; // Distância que o NPC deve caminhar
    public float velocidade = 2f;           // Velocidade de caminhada do NPC
    public float velocidadeRotacao = 180f;  // Velocidade de rotação em graus por segundo

    private Vector3 pontoInicial;
    private Vector3 pontoDestino;
    private bool indo = true;
    private bool rotacionando = false;
    private Quaternion rotacaoInicial;
    private Quaternion rotacaoFinal;

    void Start()
    {
        pontoInicial = transform.position;
        pontoDestino = pontoInicial + transform.forward * distanciaDeCaminhada;
    }

    void Update()
    {
        if (rotacionando)
        {
            // Executa a rotação suave
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacaoFinal, velocidadeRotacao * Time.deltaTime);

            // Verifica se a rotação foi concluída
            if (Quaternion.Angle(transform.rotation, rotacaoFinal) < 0.1f)
            {
                rotacionando = false;
            }
        }
        else
        {
            // Caminha em direção ao ponto de destino
            if (indo)
            {
                CaminharPara(pontoDestino);
                if (Vector3.Distance(transform.position, pontoDestino) < 0.1f)
                {
                    PrepararMeiaVolta();
                    indo = false;
                }
            }
            else
            {
                CaminharPara(pontoInicial);
                if (Vector3.Distance(transform.position, pontoInicial) < 0.1f)
                {
                    PrepararMeiaVolta();
                    indo = true;
                }
            }
        }
    }

    void CaminharPara(Vector3 destino)
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);
    }

    void PrepararMeiaVolta()
    {
        rotacaoInicial = transform.rotation;
        rotacaoFinal = rotacaoInicial * Quaternion.Euler(0f, 180f, 0f);
        rotacionando = true;
    }
}
