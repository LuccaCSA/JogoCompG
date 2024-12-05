using UnityEngine;
using System.Collections;


public class Barco : MonoBehaviour, IDanoso
{
    public int vidaInicial = 100;  // Vida inicial do barco
    public Material contornoMaterial; // Material de contorno vermelho
    private int vidaAtual;         // Vida atual do barco
    private Renderer barcoRenderer; // Referência ao renderizador do barco
    private Material[] materiaisOriginais; // Armazena os materiais originais

    public float tempoAfundando = 2f; // Tempo de afundamento
    public float inclinacaoAfundando = 50f; // Ângulo de inclinação ao afundar
    public Vector3 direcaoAfundar = Vector3.down; // Direção do movimento ao afundar

    void Start()
    {
        // Inicializa a vida do barco com a vida inicial
        vidaAtual = vidaInicial;

        // Configura o renderizador e armazena os materiais originais
        barcoRenderer = GetComponent<Renderer>();
        materiaisOriginais = barcoRenderer.materials;

        // Adiciona o contorno ao barco
        AdicionarContorno();
    }

    void AdicionarContorno()
    {
        // Cria um novo array de materiais que inclui os materiais originais + o contorno
        Material[] novosMateriais = new Material[materiaisOriginais.Length + 1];
        for (int i = 0; i < materiaisOriginais.Length; i++)
        {
            novosMateriais[i] = materiaisOriginais[i];
        }
        novosMateriais[novosMateriais.Length - 1] = contornoMaterial;

        // Aplica o novo array de materiais ao renderizador
        barcoRenderer.materials = novosMateriais;
    }

    // Implementação da interface IDanoso para receber dano
    public void ReceberDano(int dano)
    {
        // Reduz a vida atual com base no dano recebido
        vidaAtual -= dano;
        Debug.Log("Vida atual do barco: " + vidaAtual);

        // Verifica se a vida chegou a zero ou menos
        if (vidaAtual <= 0)
        {
            StartCoroutine(AfundarBarco());
        }
    }

    IEnumerator AfundarBarco()
    {
        Debug.Log("Barco começando a afundar...");

        // Define a rotação final do barco
        Quaternion rotacaoFinal = Quaternion.Euler(inclinacaoAfundando, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        float tempoPassado = 0f;

        // Move e inclina o barco para simular o afundamento
        while (tempoPassado < tempoAfundando)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoFinal, Time.deltaTime * 2);
            transform.position += direcaoAfundar * Time.deltaTime;
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Barco destruído.");
        // Destrói o barco após o afundamento
        Destroy(gameObject);
    }
}
