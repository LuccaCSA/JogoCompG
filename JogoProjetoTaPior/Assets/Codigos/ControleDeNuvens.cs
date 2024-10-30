using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeNuvens : MonoBehaviour
{
    public List<GameObject> nuvens;      // Lista de nuvens que já estão na cena
    public float velocidade = 5f;        // Velocidade de movimento das nuvens
    public float limiteZ = -600f;        // Posição Z que define o final do trajeto para trás
    public float posicaoInicialZ = 600f; // Posição Z inicial do trajeto

    // Armazena as posições iniciais das nuvens
    private List<Vector3> posicoesIniciais = new List<Vector3>();

    void Start()
    {
        // Armazena as posições iniciais de cada nuvem
        foreach (GameObject nuvem in nuvens)
        {
            if (nuvem != null)
            {
                // Guarda a posição inicial completa da nuvem (X, Y e Z)
                posicoesIniciais.Add(nuvem.transform.position);
            }
        }
    }

    void Update()
    {
        // Move cada nuvem a cada frame
        for (int i = 0; i < nuvens.Count; i++)
        {
            GameObject nuvem = nuvens[i];
            if (nuvem != null)
            {
                // Move a nuvem para trás no eixo Z sem alterar os eixos X e Y
                nuvem.transform.Translate(0, 0, -velocidade * Time.deltaTime);

                // Verifica se a nuvem atingiu o limite no eixo Z
                if (nuvem.transform.position.z <= limiteZ)
                {
                    // Reseta a posição Z para o início do trajeto, mantendo X e Y inalterados
                    Vector3 novaPosicao = new Vector3(posicoesIniciais[i].x, posicoesIniciais[i].y, posicaoInicialZ);
                    nuvem.transform.position = novaPosicao;
                }
            }
        }
    }
}
