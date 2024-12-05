using UnityEngine;
using System.Collections.Generic;

public class LixoDetector : MonoBehaviour
{
    [Header("Referência ao Controle da Barra de Lixo")]
    public MagicPigGames.ControleBarraLixo controleBarraLixo; // Referência ao script que controla a barra de lixo

    // Lista para rastrear os lixos que estão em contato
    private List<GameObject> lixosNoChao = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou em contato possui a tag "Lixo"
        if (other.CompareTag("Lixo"))
        {
            // Adiciona o lixo à lista, se ainda não estiver lá
            if (!lixosNoChao.Contains(other.gameObject))
            {
                lixosNoChao.Add(other.gameObject);
                Debug.Log($"Lixo detectado: {other.name}");
            }

            // Atualiza o controle da barra com o novo número de lixos
            controleBarraLixo?.AtualizarNumeroDeLixos(lixosNoChao.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove o lixo da lista ao sair do trigger
        if (other.CompareTag("Lixo") && lixosNoChao.Contains(other.gameObject))
        {
            lixosNoChao.Remove(other.gameObject);
            Debug.Log($"Lixo removido da detecção: {other.name}");

            // Atualiza o controle da barra com o novo número de lixos
            controleBarraLixo?.AtualizarNumeroDeLixos(lixosNoChao.Count);
        }
    }

    private void Update()
    {
        // Verifica se algum lixo foi destruído diretamente e atualiza a lista
        for (int i = lixosNoChao.Count - 1; i >= 0; i--)
        {
            if (lixosNoChao[i] == null) // Se o objeto foi destruído
            {
                lixosNoChao.RemoveAt(i);
                Debug.Log("Lixo destruído detectado e removido da lista.");
            }
        }

        // Atualiza o número de lixos no controle da barra
        controleBarraLixo?.AtualizarNumeroDeLixos(lixosNoChao.Count);
    }
}
