using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TrocaDeArmas : MonoBehaviour
{
    public GameObject pistola;  // Referência à pistola de água
    public GameObject vassoura; // Referência à vassoura

    public Sprite miraPistola;  // Sprite da mira para a pistola
    public Sprite miraVassoura; // Sprite da mira para a vassoura
    public Image crosshairImage; // Referência à imagem da mira na interface

    public Vector2 tamanhoMiraVassoura = new Vector2(100, 100); // Tamanho da mira da vassoura
    public Vector2 tamanhoOriginal; // Tamanho original da mira
    public float expansaoMaxima = 1.2f; // Fator de expansão máxima
    public float expansaoMinima = 0.8f; // Fator de retração mínima
    public float tempoAnimacao = 0.3f; // Tempo total da animação

    public TextMeshProUGUI textoGrande;  // Use TextMeshProUGUI para UI TextMeshPro
    public float tempoExibicao = 2f;     // Tempo que o texto grande permanece na tela
    public float tempoTransicao = 0.5f;  // Tempo para o fade in e fade out

    public Image slotPistola; // Referência ao ícone de slot da pistola
    public Image slotVassoura; // Referência ao ícone de slot da vassoura
    public Sprite slotSelecionadoSprite; // Sprite para o fundo quando o item é selecionado
    public Sprite slotNaoSelecionadoSprite; // Sprite para o fundo quando o item não está selecionado

    private Coroutine textoCorrotina;    // Referência para a coroutine atual do texto

    void Start()
    {
        // Armazena o tamanho original da mira
        tamanhoOriginal = crosshairImage.rectTransform.sizeDelta;

        // Oculta o texto grande no início
        textoGrande.gameObject.SetActive(false);

        // Ativar a pistola e desativar a vassoura no início, e definir a mira correspondente
        AtivarArma(pistola, miraPistola, tamanhoOriginal, "Pistola de Água", slotPistola, slotVassoura);
    }

    void Update()
    {
        // Tecla 1 para selecionar a pistola de água
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AtivarArma(pistola, miraPistola, tamanhoOriginal, "Pistola de Água", slotPistola, slotVassoura);
        }
        // Tecla 2 para selecionar a vassoura
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AtivarArma(vassoura, miraVassoura, tamanhoMiraVassoura, "Vassoura", slotVassoura, slotPistola);
        }
    }

    void AtivarArma(GameObject armaParaAtivar, Sprite mira, Vector2 tamanhoMira, string nomeArma, Image slotSelecionado, Image slotNaoSelecionado)
    {
        // Desativar todas as armas
        pistola.SetActive(false);
        vassoura.SetActive(false);

        // Ativar a arma selecionada
        armaParaAtivar.SetActive(true);

        // Trocar a mira na interface e iniciar a animação
        if (crosshairImage != null && mira != null)
        {
            crosshairImage.sprite = mira;
            crosshairImage.rectTransform.sizeDelta = tamanhoMira;
            StartCoroutine(AnimarMira(tamanhoMira));
        }

        // Atualizar o sprite de fundo do slot selecionado e não selecionado
        slotSelecionado.sprite = slotSelecionadoSprite;
        slotNaoSelecionado.sprite = slotNaoSelecionadoSprite;

        // Reiniciar a exibição do texto grande para evitar cortes abruptos
        if (textoCorrotina != null)
        {
            StopCoroutine(textoCorrotina);
        }
        textoCorrotina = StartCoroutine(ExibirTextoGrandeComTransicao(nomeArma));
    }

    IEnumerator AnimarMira(Vector2 tamanhoOriginal)
    {
        Vector2 tamanhoMaximo = tamanhoOriginal * expansaoMaxima;
        Vector2 tamanhoMinimo = tamanhoOriginal * expansaoMinima;
        float tempoAtual = 0f;

        // Expande a mira
        while (tempoAtual < tempoAnimacao / 2f)
        {
            crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(tamanhoOriginal, tamanhoMaximo, tempoAtual / (tempoAnimacao / 2f));
            tempoAtual += Time.deltaTime;
            yield return null;
        }

        // Retrai a mira
        tempoAtual = 0f;
        while (tempoAtual < tempoAnimacao / 2f)
        {
            crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(tamanhoMaximo, tamanhoMinimo, tempoAtual / (tempoAnimacao / 2f));
            tempoAtual += Time.deltaTime;
            yield return null;
        }

        // Retorna ao tamanho original
        crosshairImage.rectTransform.sizeDelta = tamanhoOriginal;
    }

    IEnumerator ExibirTextoGrandeComTransicao(string texto)
    {
        textoGrande.text = texto;

        // Fade in
        textoGrande.gameObject.SetActive(true);
        textoGrande.alpha = 0f;
        float tempoAtual = 0f;

        while (tempoAtual < tempoTransicao)
        {
            textoGrande.alpha = Mathf.Lerp(0f, 1f, tempoAtual / tempoTransicao);
            tempoAtual += Time.deltaTime;
            yield return null;
        }

        textoGrande.alpha = 1f;

        // Espera o tempo de exibição completo
        yield return new WaitForSeconds(tempoExibicao);

        // Fade out
        tempoAtual = 0f;

        while (tempoAtual < tempoTransicao)
        {
            textoGrande.alpha = Mathf.Lerp(1f, 0f, tempoAtual / tempoTransicao);
            tempoAtual += Time.deltaTime;
            yield return null;
        }

        textoGrande.alpha = 0f;
        textoGrande.gameObject.SetActive(false);
    }
}
