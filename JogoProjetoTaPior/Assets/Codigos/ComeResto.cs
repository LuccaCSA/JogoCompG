using UnityEngine;

public class ComeResto : MonoBehaviour
{
    // Método chamado ao detectar colisão com outro objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido tem a tag "Lixo"
        if (other.CompareTag("Lixo")) // Certifique-se de que os prefabs de lixo têm a tag "Lixo"
        {
            Debug.Log("Objeto destruído: " + other.name);
            Destroy(other.gameObject); // Destrói o objeto que colidiu
        }
    }
}
