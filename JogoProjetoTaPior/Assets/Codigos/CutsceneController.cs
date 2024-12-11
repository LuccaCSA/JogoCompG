using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras; // Arraste suas Virtual Cameras aqui no Inspector
    public float[] cameraDurations; // Duração de cada câmera (em segundos)
    private int currentCameraIndex = 0;

    //att tg
    public ChangeScene changeScene;
    //
    void Start()
    {
        // Desativa todas as Virtual Cameras no início
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = 0;
        }

        // Ativa a primeira câmera
        ActivateCamera(0);

        // Inicia a transição entre as câmeras
        StartCoroutine(SwitchCameras());
    }

    void ActivateCamera(int index)
    {
        if (index < 0 || index >= virtualCameras.Length) return;

        // Define a prioridade para a câmera atual
        virtualCameras[index].Priority = 10; // Maior prioridade ativa a câmera

        // Define prioridade baixa para as outras câmeras
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            if (i != index)
                virtualCameras[i].Priority = 0;
        }
    }

    System.Collections.IEnumerator SwitchCameras()
    {
        while (currentCameraIndex < virtualCameras.Length)
        {
            yield return new WaitForSeconds(cameraDurations[currentCameraIndex]); // Espera o tempo definido para a câmera atual
            currentCameraIndex++; // Passa para a próxima câmera

            if (currentCameraIndex < virtualCameras.Length)
                ActivateCamera(currentCameraIndex); // Ativa a próxima câmera
        }

        // Cutscene terminou, você pode adicionar a lógica para seguir para a próxima cena aqui
        EndCutscene();
    }

    void EndCutscene()
    {
       // Debug.Log("Cutscene terminou!");
        // Se necessário, você pode carregar outra cena:
        changeScene.LoadScene("BossFight");
    }
}
