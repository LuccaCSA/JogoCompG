using UnityEngine;
using System.Collections;

public class BuracoTartaruga : MonoBehaviour
{
    public GameObject nuvemDeGaivotasPrefab; // Prefab da nuvem de gaivotas
    public GameObject ovosPrefab;            // Prefab dos ovos (presente na cena desde o início)
    public GameObject tartarugaPrefab;       // Referência ao prefab da tartaruga
    public float alturaInicialTartarugas = 0.5f; // Altura inicial das tartarugas
    public float alturaInicialGaivotas = 10f;    // Altura inicial das gaivotas
    public float velocidadeGaivotas = 5f;    // Velocidade de descida das gaivotas
    public float velocidadeTartarugas = 2f;  // Velocidade das tartarugas ao subir o buraco

    public Collider jogadorCollider; // Referência ao collider do jogador

    private GameObject nuvemDeGaivotas;
    private GameObject tartarugaInstanciada;

    void Start()
    {
        // Desativa os prefabs de gaivotas e tartarugas na cena até que o evento comece
        tartarugaPrefab.SetActive(false);
        nuvemDeGaivotasPrefab.SetActive(false);
    }

    public void IniciarEvento()
    {
        // Ativa o evento imediatamente
        StartCoroutine(ExecutarEvento());
    }

    IEnumerator ExecutarEvento()
    {
        // Instancia a nuvem de gaivotas sobre o buraco e ativa
        Vector3 posicaoNuvem = transform.position + Vector3.up * alturaInicialGaivotas; // Ajusta a altura da nuvem
        nuvemDeGaivotas = Instantiate(nuvemDeGaivotasPrefab, posicaoNuvem, Quaternion.identity);
        nuvemDeGaivotas.SetActive(true);

        // Remove os ovos do buraco
        if (ovosPrefab != null)
        {
            Destroy(ovosPrefab);
        }

        // Ativa e move as tartarugas em direção ao mar
        IniciarMovimentoTartaruga();

        // Move a nuvem de gaivotas para seguir as tartarugas
        StartCoroutine(SeguirTartarugaComGaivotas());

        yield return null;
    }

    void IniciarMovimentoTartaruga()
    {
        // Instancia a tartaruga na posição inicial
        Vector3 posicaoInicial = transform.position;
        posicaoInicial.y = alturaInicialTartarugas;

        tartarugaInstanciada = Instantiate(tartarugaPrefab, posicaoInicial, Quaternion.identity);
        tartarugaInstanciada.SetActive(true);

        // Ignora a colisão com o jogador
        Collider tartarugaCollider = tartarugaInstanciada.GetComponent<Collider>();
        if (tartarugaCollider != null && jogadorCollider != null)
        {
            Physics.IgnoreCollision(tartarugaCollider, jogadorCollider);
        }

        // Movimenta a tartaruga usando a função Update
        tartarugaInstanciada.AddComponent<MovimentoTartaruga>().ConfigurarMovimento(nuvemDeGaivotas, velocidadeTartarugas);
    }

    IEnumerator SeguirTartarugaComGaivotas()
    {
        while (tartarugaInstanciada != null)
        {
            if (nuvemDeGaivotas != null)
            {
                // Atualiza a posição horizontal das gaivotas
                Vector3 novaPosicaoGaivotas = nuvemDeGaivotas.transform.position;
                novaPosicaoGaivotas.x = Mathf.MoveTowards(novaPosicaoGaivotas.x, tartarugaInstanciada.transform.position.x, velocidadeGaivotas * Time.deltaTime);
                novaPosicaoGaivotas.z = Mathf.MoveTowards(novaPosicaoGaivotas.z, tartarugaInstanciada.transform.position.z, velocidadeGaivotas * Time.deltaTime);

                // Atualiza a posição vertical das gaivotas para descerem lentamente
                novaPosicaoGaivotas.y = Mathf.MoveTowards(novaPosicaoGaivotas.y, alturaInicialTartarugas, velocidadeGaivotas * Time.deltaTime);

                nuvemDeGaivotas.transform.position = novaPosicaoGaivotas;
            }

            yield return null;
        }

        // Quando as tartarugas desaparecerem, o controle das gaivotas será feito pelo script NuvemDeGaivotas
        if (nuvemDeGaivotas != null)
        {
            var scriptGaivotas = nuvemDeGaivotas.GetComponent<NuvemDeGaivotas>();
            if (scriptGaivotas != null)
            {
                scriptGaivotas.IniciarAscensao();
            }
        }
    }

    public class MovimentoTartaruga : MonoBehaviour
    {
        private GameObject nuvemDeGaivotas;
        private float velocidade;

        public void ConfigurarMovimento(GameObject gaivotas, float vel)
        {
            nuvemDeGaivotas = gaivotas;
            velocidade = vel;
        }

        void Update()
        {
            // Movimenta a tartaruga para frente constantemente
            transform.position += Vector3.forward * velocidade * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Verifica se a tartaruga colidiu com a nuvem de gaivotas
            if (other.gameObject == nuvemDeGaivotas)
            {
                Destroy(gameObject); // Destrói a tartaruga ao colidir com as gaivotas
            }
        }
    }
}
