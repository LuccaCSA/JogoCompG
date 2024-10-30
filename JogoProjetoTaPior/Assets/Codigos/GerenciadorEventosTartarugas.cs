using UnityEngine;
using System.Collections;
using System.Collections.Generic;  // Adiciona essa linha

public class GerenciadorEventosTartarugas : MonoBehaviour
{
    public BuracoTartaruga[] buracos;       // Array de buracos
    public float tempoInicialEvento = 60f;  // Tempo para o início do primeiro evento
    public float intervaloEntreEventos = 30f; // Intervalo entre os eventos

    private List<BuracoTartaruga> buracosDisponiveis;

    void Start()
    {
        // Cria uma lista de buracos disponíveis para serem usados
        buracosDisponiveis = new List<BuracoTartaruga>(buracos);

        // Inicia a rotina que escolhe um buraco aleatório após o tempo inicial
        StartCoroutine(IniciarEventos());
    }

    IEnumerator IniciarEventos()
    {
        // Espera o tempo inicial para começar o primeiro evento
        yield return new WaitForSeconds(tempoInicialEvento);

        while (buracosDisponiveis.Count > 0)
        {
            // Escolhe um buraco aleatório
            int index = Random.Range(0, buracosDisponiveis.Count);
            BuracoTartaruga buracoEscolhido = buracosDisponiveis[index];

            // Ativa o evento no buraco escolhido
            buracoEscolhido.IniciarEvento();

            // Remove o buraco escolhido da lista de disponíveis
            buracosDisponiveis.RemoveAt(index);

            // Espera o intervalo antes de iniciar o próximo evento, se ainda houver buracos disponíveis
            if (buracosDisponiveis.Count > 0)
            {
                yield return new WaitForSeconds(intervaloEntreEventos);
            }
        }
    }
}
