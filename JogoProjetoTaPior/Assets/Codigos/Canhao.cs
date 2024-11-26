using UnityEngine;

public class Canhao : MonoBehaviour
{
    [Header("Referências")]
    public GameObject projetilPrefab; // Referência ao prefab do projétil
    public Transform bocaCanhao; // Posição de saída do disparo
    public GameObject efeitoExplosao; // Prefab do efeito de explosão

    [Header("Configurações do Tiro")]
    public float forcaDisparo = 500f; // Força aplicada ao projétil
    public float tempoVidaExplosao = 2f; // Tempo de vida do efeito de explosão

    // Método para disparar, chamado pelo gerenciador de canhões
    public void Disparar()
    {
        if (projetilPrefab != null && bocaCanhao != null)
        {
            // Instancia o projétil na boca do canhão
            GameObject projetil = Instantiate(projetilPrefab, bocaCanhao.position, bocaCanhao.rotation);

            // Aplica força no Rigidbody do projétil
            Rigidbody rb = projetil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(bocaCanhao.forward * forcaDisparo);
            }

            // Instancia o efeito de explosão
            if (efeitoExplosao != null)
            {
                GameObject explosao = Instantiate(efeitoExplosao, bocaCanhao.position, bocaCanhao.rotation);
                Destroy(explosao, tempoVidaExplosao); // Destrói o efeito de explosão após o tempo configurado
            }
        }
    }
}