using UnityEngine;

public class Boss : MonoBehaviour, IDanoso
{
    [Header("Configurações do Boss")]
    public int vidaInicial = 100; // Vida inicial do boss
    private int vidaAtual; // Vida atual do boss

    [Header("Referência à Barra de Vida")]
    public MagicPigGames.BarraVidaBoss barraDeVidaBoss; // Referência ao script que controla a barra de vida

    [Header("Configurações da Animação de Morte")]
    public float tempoAfundar = 3f; // Tempo que leva para o objeto afundar completamente
    public float anguloEmpinar = 50f; // Ângulo para empinar no eixo X
    public float distanciaAfundar = 30f; // Distância no eixo Y para o objeto descer

    private bool estaAfundando = false; // Controle para evitar múltiplas animações

    void Start()
    {
        // Inicializa a vida do boss
        vidaAtual = vidaInicial;

        // Configura a barra de vida inicial como cheia (0% de progresso, pois barra cheia significa vida completa)
        if (barraDeVidaBoss != null)
        {
            barraDeVidaBoss.AtualizarBarraDeVida(0f); // 0.0 = 100% de vida (barra cheia)
        }
    }

    public void ReceberDano(int dano)
    {
        if (estaAfundando) return; // Ignora danos adicionais enquanto está afundando

        // Aplica o dano ao boss
        vidaAtual -= dano;

        // Garante que a vida não seja menor que zero
        vidaAtual = Mathf.Max(vidaAtual, 0);

        // Calcula a porcentagem de vida perdida
        float porcentagemVidaPerdida = 1f - (float)vidaAtual / vidaInicial;

        // Atualiza a barra de vida do boss com base na porcentagem de vida perdida
        if (barraDeVidaBoss != null)
        {
            barraDeVidaBoss.AtualizarBarraDeVida(porcentagemVidaPerdida);
        }

        Debug.Log($"Boss recebeu {dano} de dano. Vida atual: {vidaAtual}");

        // Verifica se o boss morreu
        if (vidaAtual <= 0 && !estaAfundando)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        Debug.Log("Boss derrotado!");

        // Inicia a animação de afundar
        estaAfundando = true;
        StartCoroutine(AfundarBoss());
    }

    private System.Collections.IEnumerator AfundarBoss()
    {
        Debug.Log("Iniciando animação de empinar e afundar...");

        // Configurações de rotação e posição
        Quaternion rotacaoInicial = transform.rotation;
        Quaternion rotacaoFinal = Quaternion.Euler(anguloEmpinar, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Vector3 posicaoInicial = transform.position;
        Vector3 posicaoFinal = transform.position - new Vector3(0f, distanciaAfundar, 0f);

        float tempoPassado = 0f;

        // Interpola a rotação e a posição do GameObject durante o tempo definido
        while (tempoPassado < tempoAfundar)
        {
            float t = tempoPassado / tempoAfundar;

            transform.rotation = Quaternion.Slerp(rotacaoInicial, rotacaoFinal, t);
            transform.position = Vector3.Lerp(posicaoInicial, posicaoFinal, t);

            tempoPassado += Time.deltaTime;
            yield return null;
        }

        // Garante que o objeto esteja na posição final e rotação final
        transform.rotation = rotacaoFinal;
        transform.position = posicaoFinal;

        Debug.Log("Boss completamente afundado. Destruindo o objeto.");

        // Destroi o GameObject ao final da animação
        Destroy(gameObject);
    }
}
