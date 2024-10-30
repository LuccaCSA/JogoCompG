using UnityEngine;
using System.Collections;

public class FarolController : MonoBehaviour
{
    public GameObject coneDeLuz;      // Referência ao cone de luz
    public Transform centro;          // O centro em torno do qual o cone de luz vai girar
    public float velocidadeRotacao = 30f; // Velocidade de rotação do cone de luz
    public float duracaoRotacao = 5f; // Duração da rotação quando ativada

    private bool estaGirando = false; // Controle para evitar múltiplas rotações

    void Start()
    {
        // Desativa o cone de luz no início
        if (coneDeLuz != null)
        {
            coneDeLuz.SetActive(false);
        }
    }

    // Método chamado pelo Gerenciador de Barcos quando um barco entra em cena
    public void GirarFarol()
    {
        if (!estaGirando && coneDeLuz != null)
        {
            coneDeLuz.SetActive(true);  // Ativa o cone de luz
            StartCoroutine(RotacionarConeDeLuz());
        }
    }

    private IEnumerator RotacionarConeDeLuz()
    {
        estaGirando = true;
        float tempoPassado = 0f;

        // Faz o cone de luz girar ao redor do centro
        while (tempoPassado < duracaoRotacao)
        {
            coneDeLuz.transform.RotateAround(centro.position, Vector3.up, velocidadeRotacao * Time.deltaTime);
            tempoPassado += Time.deltaTime;
            yield return null;
        }

        // Desativa o cone de luz após a rotação
        if (coneDeLuz != null)
        {
            coneDeLuz.SetActive(false);
        }

        estaGirando = false;
    }
}
