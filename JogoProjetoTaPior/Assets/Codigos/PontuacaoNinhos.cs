using UnityEngine;
using TMPro; // Importa o namespace do TextMeshPro

public class PontuacaoNinhos : MonoBehaviour
{
    public TMP_Text textoNinhosSalvos;          // Texto principal dos ninhos salvos
    public TMP_Text textoNinhosSalvosSombra;   // Texto de sombra dos ninhos salvos
    private int ninhosSalvos = 0;              // Número de ninhos salvos

    void Start()
    {
        // Inicializa os textos com o valor inicial
        AtualizarPontuacao();
    }

    // Método para adicionar um ninho salvo
    public void AdicionarNinhoSalvo()
    {
        ninhosSalvos++;
        AtualizarPontuacao();
    }

    // Atualiza ambos os textos
    private void AtualizarPontuacao()
    {
        textoNinhosSalvos.text = ninhosSalvos.ToString();
        textoNinhosSalvosSombra.text = ninhosSalvos.ToString();
    }
}
