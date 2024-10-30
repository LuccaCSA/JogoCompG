using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject splashEffectPrefab; // Referência ao prefab do efeito de splash no Inspector
    public float splashEffectDuration = 2.0f; // Tempo em segundos antes de destruir o efeito de splash

    // Para colisões físicas
    void OnCollisionEnter(Collision collision)
    {
        AplicarEfeitoSplash(collision.contacts[0].point, collision.contacts[0].normal);
        Destroy(gameObject);
    }

    // Para triggers
    void OnTriggerEnter(Collider other)
    {
        Vector3 collisionPoint = other.ClosestPoint(transform.position);
        Vector3 collisionNormal = (transform.position - collisionPoint).normalized;

        AplicarEfeitoSplash(collisionPoint, collisionNormal);
        Destroy(gameObject);
    }

    // Método para instanciar o efeito de splash
    private void AplicarEfeitoSplash(Vector3 position, Vector3 normal)
    {
        if (splashEffectPrefab != null)
        {
            Quaternion rotation = Quaternion.LookRotation(normal);
            GameObject splashEffectInstance = Instantiate(splashEffectPrefab, position, rotation);

            // Destroi o efeito de splash após o tempo especificado
            Destroy(splashEffectInstance, splashEffectDuration);
        }
    }
}
