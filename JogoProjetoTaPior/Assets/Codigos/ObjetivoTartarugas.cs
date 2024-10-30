using UnityEngine;

public class ObjetivoTartarugas : MonoBehaviour
{
    public GameObject tartarugaPrefab;  // Referência ao prefab das tartarugas

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou em contato com a parede é uma instância do prefab de tartaruga
        if (other.gameObject.name.Contains(tartarugaPrefab.name))
        {
            // Destroi o objeto tartaruga
            Destroy(other.gameObject);
        }
    }
}
