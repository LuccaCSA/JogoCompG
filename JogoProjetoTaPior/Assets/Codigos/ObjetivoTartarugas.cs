using UnityEngine;

public class ObjetivoTartarugas : MonoBehaviour
{
    public GameObject tartarugaPrefab;  // Referência ao prefab das tartarugas

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou em contato com a parede é uma instância do prefab de tartaruga
        if (other.gameObject.name.Contains(tartarugaPrefab.name))
        {
            // Log para confirmar a colisão
            Debug.Log($"Tartaruga {other.gameObject.name} chegou ao objetivo!");

            // Atualiza a pontuação
            PontuacaoNinhos pontuacaoNinhos = FindObjectOfType<PontuacaoNinhos>();
            if (pontuacaoNinhos != null)
            {
                pontuacaoNinhos.AdicionarNinhoSalvo(); // Incrementa a pontuação
                Debug.Log("Pontuação atualizada!");
            }
            else
            {
                Debug.LogWarning("PontuacaoNinhos não encontrado na cena!");
            }

            // Destroi o objeto tartaruga
            Destroy(other.gameObject);
        }
    }
}
