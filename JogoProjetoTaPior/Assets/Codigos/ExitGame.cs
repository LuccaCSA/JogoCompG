using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        // Fecha o jogo quando for executado como build
        Application.Quit();
        
        // Para fins de teste no editor do Unity (opcional)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
