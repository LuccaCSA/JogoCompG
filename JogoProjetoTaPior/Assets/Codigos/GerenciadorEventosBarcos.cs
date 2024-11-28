using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MagicPigGames; // Certifique-se que o namespace esteja consistente

public class GerenciadorEventosBarcos : MonoBehaviour
{
    [System.Serializable]
    public class SpawnDeBarco
    {
        public Transform pontoDeSpawn;       // Ponto de spawn do barco
        public Vector3 rotacaoPersonalizada; // Rotação personalizada para o barco
        [HideInInspector] public GameObject barcoAtual; // Referência ao barco atualmente no spawn
    }

    public SpawnDeBarco[] spawnsDeBarcos;    // Array de pontos de spawn
    public List<GameObject> prefabsDeBarcos; // Lista de prefabs de barcos aplicável a todos os spawns
    public float intervaloEntreSpawns = 10f; // Intervalo de tempo entre spawns
    public float intervaloInicial = 5f;      // Tempo inicial antes do primeiro spawn
    public float velocidadeBarco = 5f;       // Velocidade do barco
    public float distanciaInicial = 100f;    // Distância inicial do barco em linha reta do ponto de spawn
    public float tempoDeExpansao = 5f;       // Tempo que o barco leva para atingir a escala máxima
    public Vector3 escalaMinima = new Vector3(0.1f, 0.1f, 0.1f); // Escala inicial do barco (mínima)
    public float tempoDeRotacao = 2f;        // Tempo necessário para a rotação suave
    public float amplitudeBoia = 0.5f;       // Amplitude do movimento de boia (altura do movimento)
    public float tempoBoia = 2f;             // Tempo para completar um ciclo de boia (subir e descer)

    public FarolController farolController;  // Referência ao controlador do farol
    public ControleBarraOuro controleBarraOuro; // Referência ao script de controle da barra de ouro

    public AudioSource audioSource;
    public AudioClip somBarco; 


    void Start()
    {
        // Inicia o ciclo de spawn de barcos após o intervalo inicial
        StartCoroutine(CicloDeSpawns());
    }

    IEnumerator CicloDeSpawns()
    {
        // Espera o tempo inicial antes do primeiro spawn
        yield return new WaitForSeconds(intervaloInicial);

        while (true) // Ciclo infinito para continuar gerando barcos
        {
            // Filtra apenas os spawns desocupados
            List<SpawnDeBarco> spawnsDisponiveis = new List<SpawnDeBarco>();

            foreach (var spawn in spawnsDeBarcos)
            {
                if (spawn.barcoAtual == null) // Verifica se o spawn está desocupado
                {
                    spawnsDisponiveis.Add(spawn);
                }
            }

            // Se não houver spawns disponíveis, aguarda até que um se desocupe
            if (spawnsDisponiveis.Count == 0)
            {
                yield return new WaitForSeconds(intervaloEntreSpawns);
                continue;
            }

            // Seleciona aleatoriamente um ponto de spawn disponível
            SpawnDeBarco spawnEscolhido = spawnsDisponiveis[Random.Range(0, spawnsDisponiveis.Count)];

            // Seleciona aleatoriamente um prefab de barco da lista
            GameObject barcoPrefabEscolhido = prefabsDeBarcos[Random.Range(0, prefabsDeBarcos.Count)];

            // Calcula a posição inicial do barco, partindo de uma linha reta em relação ao ponto de spawn
            Vector3 direcaoParaSpawn = -spawnEscolhido.pontoDeSpawn.forward; // Inverte a direção para spawnar o barco afastado
            Vector3 posicaoInicial = spawnEscolhido.pontoDeSpawn.position + direcaoParaSpawn * distanciaInicial;
            posicaoInicial.y = spawnEscolhido.pontoDeSpawn.position.y; // Mantém a altura do barco ao nível do mar

            // Instancia o barco na posição inicial com a rotação na direção do ponto de spawn
            GameObject barcoInstanciado = Instantiate(barcoPrefabEscolhido, posicaoInicial, Quaternion.LookRotation(-direcaoParaSpawn));

            // Armazena a escala original do barco
            Vector3 escalaOriginal = barcoInstanciado.transform.localScale;

            // Inicia a escala mínima do barco
            barcoInstanciado.transform.localScale = escalaMinima;

            // Marca o spawn como ocupado e armazena a referência ao barco
            spawnEscolhido.barcoAtual = barcoInstanciado;

            // Adiciona o componente de controle para liberar o spawn ao destruir o barco
            barcoInstanciado.AddComponent<ControleDeBarco>().Inicializar(escalaOriginal, spawnEscolhido, this, controleBarraOuro);

            // Inicia a movimentação do barco em direção ao ponto de spawn, a expansão do barco e o movimento de boia
            StartCoroutine(MoverExpandirBarco(barcoInstanciado, spawnEscolhido.pontoDeSpawn.position, velocidadeBarco, tempoDeExpansao, escalaOriginal, spawnEscolhido.rotacaoPersonalizada));

            // Espera o intervalo entre spawns antes de gerar o próximo barco
            yield return new WaitForSeconds(intervaloEntreSpawns);
        }
    }

    IEnumerator MoverExpandirBarco(GameObject barco, Vector3 destino, float velocidade, float tempoDeExpansao, Vector3 escalaOriginal, Vector3 rotacaoFinal)
    {
        Quaternion rotacaoDesejada = Quaternion.Euler(rotacaoFinal);
        float tempoPassado = 0f;

        // Expande o barco até a escala original
        while (tempoPassado < tempoDeExpansao || Vector3.Distance(barco.transform.position, destino) > 0.1f)
        {
            //att tg
            // Toca o som de movimento do barco, se ainda não estiver tocando
            if (!audioSource.isPlaying)
            {
                audioSource.clip = somBarco;
                audioSource.Play();
            }
            //
            
            // Move o barco
            barco.transform.position = Vector3.MoveTowards(barco.transform.position, destino, velocidade * Time.deltaTime);

            // Expande o barco de escala mínima até a original durante o tempo de expansão
            if (tempoPassado < tempoDeExpansao)
            {
                float t = tempoPassado / tempoDeExpansao;
                barco.transform.localScale = Vector3.Lerp(escalaMinima, escalaOriginal, t);
            }

            tempoPassado += Time.deltaTime;
            yield return null;
        }

        //att tg
        audioSource.Stop();
        //
        // Após o movimento, realiza a rotação suave
        yield return StartCoroutine(RotacionarBarco(barco, rotacaoDesejada, tempoDeRotacao));

        // Garante que a rotação final seja exatamente a desejada
        barco.transform.rotation = rotacaoDesejada;

        // **Inicia o movimento de boia apenas quando o barco chega ao estado final**
        StartCoroutine(MovimentoDeBoia(barco));

        
        // Ativa o decremento da barra de ouro apenas quando o barco estiver parado e no movimento de boia
        if (controleBarraOuro != null)
        {
            Debug.Log("aqui jas um barco");
            controleBarraOuro.AtualizarBarcosAtivos(1); // Adiciona 1 barco ativo
        }
    }

    IEnumerator RotacionarBarco(GameObject barco, Quaternion rotacaoDesejada, float duracao)
    {
        Quaternion rotacaoInicial = barco.transform.rotation;
        float tempoPassado = 0f;

        while (tempoPassado < duracao)
        {
            barco.transform.rotation = Quaternion.Slerp(rotacaoInicial, rotacaoDesejada, tempoPassado / duracao);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        barco.transform.rotation = rotacaoDesejada;
    }

    IEnumerator MovimentoDeBoia(GameObject barco)
    {
        Vector3 posicaoInicial = barco.transform.position;
        float tempoPassado = 0f;

        while (true)
        {
            // Calcula o deslocamento vertical usando uma senoide para criar um movimento de boia
            float deslocamentoY = amplitudeBoia * Mathf.Sin((tempoPassado / tempoBoia) * Mathf.PI * 2f);
            barco.transform.position = new Vector3(barco.transform.position.x, posicaoInicial.y + deslocamentoY, barco.transform.position.z);

            tempoPassado += Time.deltaTime;
            yield return null;
        }
    }

    public void BarcoDestruido()
    {
        // **Desativa o decremento da barra de ouro quando um barco é removido**
        if (controleBarraOuro != null)
        {
            controleBarraOuro.AtualizarBarcosAtivos(-1); // Remove 1 barco ativo
        }
    }
}

public class ControleDeBarco : MonoBehaviour
{
    private Vector3 escalaOriginal;
    private GerenciadorEventosBarcos.SpawnDeBarco spawn;
    private GerenciadorEventosBarcos gerenciador;
    private ControleBarraOuro controleBarraOuro;

    public void Inicializar(Vector3 escala, GerenciadorEventosBarcos.SpawnDeBarco spawnAtual, GerenciadorEventosBarcos gerenciadorAtual, ControleBarraOuro controleBarraOuroAtual)
    {
        escalaOriginal = escala;
        spawn = spawnAtual;
        gerenciador = gerenciadorAtual;
        controleBarraOuro = controleBarraOuroAtual;
    }

    private void OnDestroy()
    {
        // Libera o spawn ao destruir o barco
        if (spawn != null)
        {
            spawn.barcoAtual = null;
        }

        // Notifica o gerenciador que o barco foi destruído
        if (gerenciador != null)
        {
            gerenciador.BarcoDestruido();
        }
    }
}
