using UnityEngine;
using System.Collections;

public class NuvemDeGaivotas : MonoBehaviour, IDanoso
{
    public Transform centro;          // O centro em torno do qual as gaivotas vão girar
    public Transform[] gaivotas;      // Array de referências para os modelos das gaivotas
    public float velocidadeGiro = 30f; // Velocidade do giro das gaivotas em torno do centro
    public int vidaInicial = 100;
    public float velocidadeAscensao = 20f;
    private int vidaAtual;

    private bool ascensaoIniciada = false; // Controle para não iniciar ascensões múltiplas

    void Start()
    {
        vidaAtual = vidaInicial;
    }

    void Update()
    {
        // Faz cada gaivota girar em torno do centro
        foreach (Transform gaivota in gaivotas)
        {
            // Mantém a gaivota girando ao redor do centro sem alterar sua rotação
            gaivota.RotateAround(centro.position, Vector3.up, velocidadeGiro * Time.deltaTime);
        }
    }

    

    public void ReceberDano(int dano)
    {
        if (ascensaoIniciada) return;

        vidaAtual -= dano;
        Debug.Log("Vida atual da nuvem de gaivotas: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            IniciarAscensao();
        }
    }

    public void IniciarAscensao()
    {
        if (ascensaoIniciada) return;

        ascensaoIniciada = true;
        StartCoroutine(AscenderEDesaparecer());
    }

    private IEnumerator AscenderEDesaparecer()
    {
        while (transform.position.y < 50f) // Ajuste o valor conforme necessário
        {
            transform.position += Vector3.up * velocidadeAscensao * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // Destrói a nuvem de gaivotas após a ascensão
    }
}
