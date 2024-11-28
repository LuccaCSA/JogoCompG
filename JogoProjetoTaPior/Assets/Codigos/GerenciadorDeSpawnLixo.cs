using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MagicPigGames;

public class GerenciadorDeSpawnLixo : MonoBehaviour
{
    public Transform[] spawnPoints;          // Array de posições de spawn
    public GameObject[] lixoPrefabs;         // Array de diferentes prefabs de lixo
    public float tempoMinimo = 30f;          // Tempo mínimo entre spawns
    public float tempoMaximo = 90f;          // Tempo máximo entre spawns
    public int limiteDeLixoPorSpawn = 10;    // Limite de lixo por spawn point
    public float raioDeSpawn = 2f;           // Raio ao redor do ponto de spawn para o lixo aparecer
    public GameObject placaPrefab;           // Prefab da placa de atenção
    public Transform jogador;                // Referência ao jogador
    public float alturaPlaca = 2f;           // Altura adicional para posicionar a placa
    public float valorIncrementoBarra = 0.05f;  // Valor que cada placa incrementa na barra de poluição

    private Dictionary<Transform, List<GameObject>> lixoPorSpawn; // Para rastrear lixos por spawn point
    private Dictionary<Transform, PlacaAtencao> placasPorSpawn;   // Para controlar as placas de atenção
    private ControleBarraPoluicao controleBarraPoluicao;  // Referência ao controle da barra de poluição

    //att TG
    public SoundController soundController;
    //
    void Start()
    {
        // Inicializa o dicionário para rastrear lixos e placas por spawn point
        lixoPorSpawn = new Dictionary<Transform, List<GameObject>>();
        placasPorSpawn = new Dictionary<Transform, PlacaAtencao>();

        // Pega o componente de controle da barra de poluição
        controleBarraPoluicao = FindObjectOfType<ControleBarraPoluicao>();

        foreach (Transform spawnPoint in spawnPoints)
        {
            lixoPorSpawn[spawnPoint] = new List<GameObject>();

            // Instancia a placa de atenção em cada spawn point com altura adicional
            Vector3 posicaoPlaca = spawnPoint.position + Vector3.up * alturaPlaca;
            GameObject placaInstanciada = Instantiate(placaPrefab, posicaoPlaca, Quaternion.identity, spawnPoint);
            PlacaAtencao placaScript = placaInstanciada.GetComponent<PlacaAtencao>();
            placaScript.jogador = jogador; // Atribui a referência do jogador ao script da placa
            placasPorSpawn[spawnPoint] = placaScript;

            // Assegura que a placa está desativada inicialmente
            placaScript.gameObject.SetActive(false);
        }

        // Inicia o spawn do lixo
        StartCoroutine(SpawnLixo());
    }

    IEnumerator SpawnLixo()
    {
        while (true)
        {
            // Aguarda um tempo aleatório antes de spawnar o próximo lixo
            float tempoDeEspera = Random.Range(tempoMinimo, tempoMaximo);
            yield return new WaitForSeconds(tempoDeEspera);

            // Escolhe um ponto de spawn aleatório
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Verifica se o spawn point atingiu o limite de lixos
            if (lixoPorSpawn[spawnPoint].Count < limiteDeLixoPorSpawn)
            {
                // Escolhe um tipo de lixo aleatório
                GameObject lixoPrefab = lixoPrefabs[Random.Range(0, lixoPrefabs.Length)];

                // Calcula uma posição aleatória dentro do raio ao redor do ponto de spawn (apenas em X e Z)
                Vector2 randomOffset = Random.insideUnitCircle * raioDeSpawn;
                Vector3 spawnPosition = new Vector3(
                    spawnPoint.position.x + randomOffset.x, // X
                    spawnPoint.position.y,                 // Y permanece constante
                    spawnPoint.position.z + randomOffset.y  // Z
                );

                // Instancia o lixo na posição calculada
                GameObject lixoInstanciado = Instantiate(lixoPrefab, spawnPosition, spawnPoint.rotation);

                // Adiciona o lixo à lista de lixos desse spawn point
                lixoPorSpawn[spawnPoint].Add(lixoInstanciado);

                //att TG
                // Toca o som de spawn do lixo utilizando o SoundController
                if (soundController != null)
                {
                    soundController.PlaySound();  // Toca o som padrão (ou você pode passar parâmetros, como startTime e endTime)
                }
                //
                    
                // Desativa a placa, caso ainda não esteja no limite
                if (lixoPorSpawn[spawnPoint].Count < limiteDeLixoPorSpawn)
                {
                    placasPorSpawn[spawnPoint].DesativarPlaca();
                    placasPorSpawn[spawnPoint].gameObject.SetActive(false);
                }

                // Adiciona um listener para remover o lixo da lista quando for destruído
                lixoInstanciado.GetComponent<Lixo>().OnLixoLimpo += () => RemoverLixoDoSpawnPoint(spawnPoint, lixoInstanciado);
            }
            else
            {
                // Ativa a placa de atenção quando o limite é atingido
                if (!placasPorSpawn[spawnPoint].gameObject.activeSelf)
                {
                    placasPorSpawn[spawnPoint].gameObject.SetActive(true);
                    placasPorSpawn[spawnPoint].AtivarPlaca();

                    // **Ativa o incremento da barra de poluição quando a placa é ativada**
                    if (controleBarraPoluicao != null)
                    {
                        controleBarraPoluicao.AtivarIncremento();  // Aumenta o progresso
                    }
                }
            }
        }
    }

    void RemoverLixoDoSpawnPoint(Transform spawnPoint, GameObject lixo)
    {
        if (lixoPorSpawn.ContainsKey(spawnPoint))
        {
            lixoPorSpawn[spawnPoint].Remove(lixo);

            // Desativa a placa de atenção se houver espaço novamente
            if (lixoPorSpawn[spawnPoint].Count < limiteDeLixoPorSpawn)
            {
                placasPorSpawn[spawnPoint].DesativarPlaca();
                placasPorSpawn[spawnPoint].gameObject.SetActive(false);

                // **Desativa o incremento da barra de poluição quando a placa é desativada**
                if (controleBarraPoluicao != null)
                {
                    controleBarraPoluicao.DesativarIncremento();
                }
            }
        }
    }
}
