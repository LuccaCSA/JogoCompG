using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string scene; // O nome da cena a ser carregada

    public void LoadMainJogo()
    {
        SceneManager.LoadScene(scene);
        // Aguarde a próxima frame e atualize a iluminação
        StartCoroutine(UpdateLighting());
    }

    private System.Collections.IEnumerator UpdateLighting()
    {
        yield return null; // Aguarda um frame
        DynamicGI.UpdateEnvironment(); // Atualiza a iluminação global
    }
}
