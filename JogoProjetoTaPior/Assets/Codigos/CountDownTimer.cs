using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Arraste o componente TextMeshProUGUI no Inspector
    public string nextScene; // Nome da próxima cena
    public float countdownTime = 300f; // Tempo inicial em segundos (5 minutos por padrão)
    public float expandScale = 1.5f; // Escala máxima para o texto ao expandir
    public float shrinkScale = 1f; // Escala mínima para o texto ao retrair (normal)
    public float animationSpeed = 2f; // Velocidade da expansão/retração

    private float currentTime;
    private bool isAnimating = false;

    void Start()
    {
        currentTime = countdownTime; // Define o tempo inicial
        UpdateTimerText(); // Atualiza o texto inicialmente
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrementa o tempo
            UpdateTimerText();
            // Inicia a animação nos últimos 10 segundos
            if (currentTime <= 10f && !isAnimating)
            {
                isAnimating = true;
                StartCoroutine(AnimateText());
            }
        }
        else
        {
            // Se o tempo acabou e a animação ainda não foi ativada, trocar de cena
            if (!isAnimating)
            {
                ChangeScene();
            }
        }
    }

    void UpdateTimerText()
    {
        if (currentTime < 0) currentTime = 0; // Evita valores negativos
        int minutes = Mathf.FloorToInt(currentTime / 60); // Calcula os minutos restantes
        int seconds = Mathf.FloorToInt(currentTime % 60); // Calcula os segundos restantes
        timerText.text = $"Tempo até a tempestade: {minutes:00}:{seconds:00}";
    }

    System.Collections.IEnumerator AnimateText()
    {
        float elapsedTime = 0f;
        Vector3 originalScale = timerText.rectTransform.localScale;
        Vector3 expandedScale = originalScale * expandScale;
        Vector3 shrunkScale = originalScale * shrinkScale;

        // Loop de expansão/retração nos últimos 10 segundos
        while (currentTime > 0)
        {
            if (currentTime <= 10f) // Inicia a animação quando estiver nos últimos 10 segundos
            {
                float t = Mathf.PingPong(elapsedTime * animationSpeed, 1f); // PingPong para alternar entre expandir/retrair
                timerText.rectTransform.localScale = Vector3.Lerp(shrunkScale, expandedScale, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Volta ao tamanho original após a animação
        timerText.rectTransform.localScale = shrunkScale;

        // Troca de cena ao final da animação
        ChangeScene();
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextScene); // Troca para a próxima cena
    }
}
