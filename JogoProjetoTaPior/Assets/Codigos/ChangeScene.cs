using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    public Image transitionImage; // A referência para a imagem usada na transição
    public float transitionDuration = 1f; // Duração do fade

    private void Start()
    {
        // Certifique-se de que a imagem começa desativada
        transitionImage.gameObject.SetActive(false);
        // Executa o FadeIn ao iniciar a cena
        StartCoroutine(FadeIn());
    }

    // Método público para carregar uma nova cena
    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    // Corrotina para realizar o FadeIn
    private IEnumerator FadeIn()
    {
        // Ativa o objeto da imagem antes de começar o fade
        transitionImage.gameObject.SetActive(true);

        // Realiza o fade de opaco (alpha = 1) para transparente (alpha = 0)
        yield return StartCoroutine(Fade(1f, 0f, 5));

        // Após o FadeIn, desativa o objeto novamente
        transitionImage.gameObject.SetActive(false);
    }

    // Corrotina para realizar o FadeOut e carregar a nova cena
    private IEnumerator FadeOut(string sceneName)
    {
        // Ativa o objeto da imagem antes de começar o fade
        transitionImage.gameObject.SetActive(true);

        // Realiza o fade de transparente (alpha = 0) para opaco (alpha = 1)
        yield return StartCoroutine(Fade(0f, 1f, transitionDuration));

        // Carrega a nova cena após o fade
        SceneManager.LoadScene(sceneName);
    }

    // Corrotina genérica para realizar o efeito de fade
    private IEnumerator Fade(float startAlpha, float endAlpha, float timesmooth)
{
    float elapsedTime = 0f;
    Color color = transitionImage.color;

    while (elapsedTime < transitionDuration)
    {
        elapsedTime += Time.deltaTime;
        // Usando SmoothStep para suavizar a transição
        float t = Mathf.SmoothStep(0f, 1f, elapsedTime / timesmooth);
        color.a = Mathf.Lerp(startAlpha, endAlpha, t);
        transitionImage.color = color;
        yield return null;
    }

    // Garante que o alpha final seja aplicado
    color.a = endAlpha;
    transitionImage.color = color;
}

}