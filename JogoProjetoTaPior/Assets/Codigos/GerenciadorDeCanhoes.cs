using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeCanhoes : MonoBehaviour
{
    [Header("Lista de Canhões")]
    public List<Canhao> canhoes; // Lista de canhões controlados

    [Header("Configuração do Intervalo")]
    public float intervaloEntreDisparos = 3f; // Intervalo entre disparos

    private int ultimoCanhaoIndex = -1; // Índice do último canhão disparado

    private void Start()
    {
        StartCoroutine(GerenciarDisparos());
    }

    private IEnumerator GerenciarDisparos()
    {
        while (true)
        {
            // Garante que ao menos 2 canhões estejam disponíveis
            if (canhoes.Count > 1)
            {
                int novoCanhaoIndex = EscolherCanhaoAleatorio();

                // Dispara o canhão selecionado
                canhoes[novoCanhaoIndex].Disparar();

                // Atualiza o índice do último canhão disparado
                ultimoCanhaoIndex = novoCanhaoIndex;

                // Aguarda o intervalo antes de disparar novamente
                yield return new WaitForSeconds(intervaloEntreDisparos);
            }
            else
            {
                Debug.LogWarning("É necessário pelo menos dois canhões para evitar repetição.");
                yield break;
            }
        }
    }

    private int EscolherCanhaoAleatorio()
    {
        int novoIndex;
        do
        {
            novoIndex = Random.Range(0, canhoes.Count); // Escolhe um índice aleatório
        } while (novoIndex == ultimoCanhaoIndex); // Garante que não seja o último canhão escolhido

        return novoIndex;
    }
}
