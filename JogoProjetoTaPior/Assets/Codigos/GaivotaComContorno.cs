using UnityEngine;

public class GaivotaComContorno : MonoBehaviour
{
    public Material contornoMaterial;  // Material de contorno a ser aplicado
    private Material materialOriginal; // Armazena o material original

    void Start()
    {
        // Armazena o material original e aplica o contorno
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            materialOriginal = renderer.material;
            renderer.material = contornoMaterial;
        }
    }

    // Método para restaurar o material original, se necessário
    public void RestaurarMaterialOriginal()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && materialOriginal != null)
        {
            renderer.material = materialOriginal;
        }
    }
}
