using UnityEngine;

public class InvisivelParede : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu é um projétil
        if (collision.gameObject.GetComponent<Projetil>() != null)
        {
            // Ignora a colisão entre a parede invisível e o projétil
            Collider paredeCollider = GetComponent<Collider>();
            Collider projetilCollider = collision.collider;

            if (paredeCollider != null && projetilCollider != null)
            {
                Physics.IgnoreCollision(paredeCollider, projetilCollider);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu é um projétil
        if (other.GetComponent<Projetil>() != null)
        {
            // Ignora a colisão entre a parede invisível e o projétil
            Collider paredeCollider = GetComponent<Collider>();
            Collider projetilCollider = other;

            if (paredeCollider != null && projetilCollider != null)
            {
                Physics.IgnoreCollision(paredeCollider, projetilCollider);
            }
        }
    }
}
