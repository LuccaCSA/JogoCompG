using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

   public string scene; // O nome da cena a ser carregada

    public void LoadMainJogo()
    {
        SceneManager.LoadScene(scene);
    }
}
