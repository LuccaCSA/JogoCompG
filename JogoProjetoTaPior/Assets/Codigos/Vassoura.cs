using UnityEngine;

public class Vassoura : MonoBehaviour
{
    public Camera playerCamera; // Referência à câmera do jogador
    private Lixo lixoAtual;     // Referência ao lixo que está sendo limpo atualmente

    public AudioSource audioSource;  // O AudioSource que irá tocar o som
    public AudioClip limparLixoClip; // O som que será tocado ao limpar o lixo

    void Update()
    {
        if (Input.GetButton("Fire1")) // Botão esquerdo do mouse pressionado
        {
            RaycastHit hit;

            // Cria um raycast da câmera do jogador
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
            {
                // Verifica se o objeto atingido é um lixo
                Lixo lixo = hit.transform.GetComponent<Lixo>();
                if (lixo != null && lixo != lixoAtual)
                {
                    lixoAtual = lixo;
                    lixoAtual.LimparLixo(); // Inicia a limpeza do lixo

                    //att Tg
                     // Se o som ainda não estiver tocando, toca o som
                    if (!audioSource.isPlaying)
                    {
                        audioSource.clip = limparLixoClip;
                        audioSource.Play();  // Reproduz o som
                    }
                    //
                }
            }
        }
        else
        {
            lixoAtual = null; // Reseta o lixoAtual quando o botão não está pressionado

            //att TG

            // Para o som se o botão não estiver pressionado
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            //
        }
    }
}
