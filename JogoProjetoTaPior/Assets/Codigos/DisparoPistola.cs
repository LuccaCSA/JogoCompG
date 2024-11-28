using UnityEngine;

public class DisparoPistola : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float fireRate = 0.1f;
    public int dano = 10;  // Dano customizável no Inspector

    private float nextFireTime = 0f;

//alteracao TG
    public AudioSource audioSource; // Referência ao AudioSource
    public AudioClip disparoClip;   // Referência ao AudioClip do disparo
//

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Disparar();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Disparar()
    {
        Vector3 spawnPosition = firePoint.position + firePoint.forward * 0.5f;

        GameObject proj = Instantiate(projectilePrefab, spawnPosition, firePoint.rotation);

        Collider playerCollider = GetComponent<Collider>();
        Collider projectileCollider = proj.GetComponent<Collider>();

        if (playerCollider != null && projectileCollider != null)
        {
            Physics.IgnoreCollision(projectileCollider, playerCollider);
        }

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;

        proj.AddComponent<Projetil>().ConfigurarDano(dano);

        //Alteracao TG
         // Reproduzindo o som de disparo
        if (audioSource != null && disparoClip != null)
        {
            audioSource.PlayOneShot(disparoClip);
        }
        //
    }
}

public class Projetil : MonoBehaviour
{
    private int dano;

    public void ConfigurarDano(int danoProj)
    {
        dano = danoProj;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto colidido implementa IDanoso
        IDanoso danoso = other.GetComponent<IDanoso>();
        if (danoso != null)
        {
            danoso.ReceberDano(dano);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Objeto colidido não implementa IDanoso.");
        }
    }
}