using System.Collections.Generic;
using UnityEngine;

public class BalaDeCanhao : MonoBehaviour
{
    [Header("Configuração do Efeito de Colisão")]
    public GameObject efeitoColisaoPrefab; // Prefab do efeito de colisão
    public float duracaoEfeito = 1f; // Duração do efeito de colisão

    [Header("Configuração de Lixos")]
    public List<GameObject> prefabsLixo; // Lista de prefabs de lixos
    public float raioDropLixo = 3f; // Raio em que os lixos serão espalhados
    public float alturaDropLixo = 1f; // Altura em que os lixos aparecerão

    private void OnCollisionEnter(Collision collision)
    {
        // Cria o efeito de colisão
        if (efeitoColisaoPrefab != null)
        {
            GameObject efeito = Instantiate(efeitoColisaoPrefab, transform.position, Quaternion.identity);
            Destroy(efeito, duracaoEfeito); // Destrói o efeito após o tempo configurado
        }

        // Espalha os lixos aleatoriamente
        if (prefabsLixo != null && prefabsLixo.Count > 0)
        {
            EspalharLixos();
        }

        // Destrói o projétil após a colisão
        Destroy(gameObject);
    }

    private void EspalharLixos()
    {
        int quantidadeLixos = Random.Range(3, 6); // Define aleatoriamente a quantidade de lixos para espalhar

        for (int i = 0; i < quantidadeLixos; i++)
        {
            // Escolhe um prefab de lixo aleatório
            GameObject lixoPrefab = prefabsLixo[Random.Range(0, prefabsLixo.Count)];

            // Calcula uma posição aleatória dentro do raio
            Vector3 posicaoAleatoria = transform.position + new Vector3(
                Random.Range(-raioDropLixo, raioDropLixo),
                alturaDropLixo,
                Random.Range(-raioDropLixo, raioDropLixo)
            );

            // Instancia o lixo na posição calculada
            Instantiate(lixoPrefab, posicaoAleatoria, Quaternion.identity);
        }
    }
}
