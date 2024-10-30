using UnityEngine;
using System;
using System.Collections;

public class Lixo : MonoBehaviour
{
    public event Action OnLixoLimpo; // Evento para notificar quando o lixo é limpo
    private bool isBeingCleaned = false;

    private Renderer renderer;
    private Material[] originalMaterials;
    public Material contornoMaterial; // Material de contorno

    void Start()
    {
        // Inicializa o renderer e armazena os materiais originais
        renderer = GetComponent<Renderer>();
        originalMaterials = renderer.materials;

        // Ativa o contorno desde o início
        AtivarContorno();
    }

    public void LimparLixo()
    {
        if (!isBeingCleaned)
        {
            isBeingCleaned = true;
            StartCoroutine(LimparLixoCoroutine());
        }
    }

    private IEnumerator LimparLixoCoroutine()
    {
        // Aqui você pode adicionar uma animação ou som para simular a limpeza
        yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos para a limpeza

        // Notifica que o lixo foi limpo
        OnLixoLimpo?.Invoke();

        // Remove o objeto de lixo do mapa
        Destroy(gameObject);
    }

    private void AtivarContorno()
    {
        // Cria um novo array de materiais com espaço para o contorno
        Material[] materialsWithContorno = new Material[originalMaterials.Length + 1];

        // Copia os materiais originais
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            materialsWithContorno[i] = originalMaterials[i];
        }

        // Adiciona o material de contorno no final do array
        materialsWithContorno[materialsWithContorno.Length - 1] = contornoMaterial;

        // Aplica os materiais ao Renderer
        renderer.materials = materialsWithContorno;
    }
}
