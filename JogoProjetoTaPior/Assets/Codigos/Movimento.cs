using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movimento : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Define a detecção de colisão para contínua
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Calcula a nova posição, mas agora o Rigidbody gerencia o movimento e as colisões
        Vector3 newPosition = rb.position + move * moveSpeed * Time.deltaTime;

        rb.MovePosition(newPosition);
    }
}
