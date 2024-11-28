using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Audio Configurations")]
    public AudioSource audioSource; // O componente AudioSource onde o som será reproduzido
    public AudioClip audioClip;     // O arquivo de som que será reproduzido
    public bool soundEnabled = true; // Flag para habilitar ou desabilitar o som
    public Transform jogador; // Referência ao Transform do jogador
    public float maxDistancia = 10f; // Distância máxima para o som ser ouvido
    public bool adjustVolume = true; // Flag para ativar ou desativar o ajuste do volume

    private bool isPlaying = false; // Flag para verificar se o som está sendo reproduzido

    void Update()
    {
        // Verifica a distância entre o jogador e o objeto que emite o som
        float distancia = Vector3.Distance(transform.position, jogador.position);

        // Se o som estiver ativado e o jogador estiver dentro da distância máxima
        if (soundEnabled && distancia <= maxDistancia)
        {
            // Se a flag de ajuste de volume estiver ativada, ajusta o volume
            if (adjustVolume)
            {
                // Ajusta o volume com base na distância
                float volume = Mathf.Clamp01(1 - (distancia / maxDistancia));
                audioSource.volume = volume;
            }

            // Se o som não estiver tocando, inicia a reprodução
            if (!isPlaying)
            {
                PlaySound();
            }
        }
        else
        {
            // Se o jogador estiver fora da área de alcance ou o som não está habilitado, para o som
            if (isPlaying)
            {
                StopSound();
            }
        }
    }

    /// <summary>
    /// Toca o som a partir do tempo inicial, se especificado.
    /// </summary>
    public void PlaySound(float? startTime = null)
    {
        // Verifica se o som não está tocando e a fonte de áudio está configurada corretamente
        if (audioSource != null && audioClip != null && !audioSource.isPlaying)
        {
            // Configura o áudio
            audioSource.clip = audioClip;

            // Define o tempo inicial (se especificado)
            if (startTime.HasValue)
                audioSource.time = Mathf.Clamp(startTime.Value, 0, audioClip.length);
            else
                audioSource.time = 0;

            // Inicia o som
            audioSource.Play();
            isPlaying = true; // Marca que o som está tocando
        }
        //else
        //{
            ///Debug.LogWarning("AudioSource ou AudioClip não configurado corretamente.");
        //}
    }

    /// <summary>
    /// Para o som manualmente.
    /// </summary>
    private void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            isPlaying = false; // Marca que o som parou
        }
    }

    /// <summary>
    /// Exemplo de como alternar a flag de som (pode ser chamado por eventos).
    /// </summary>
    public void ToggleSound(bool enabled)
    {
        soundEnabled = enabled;
    }
}
