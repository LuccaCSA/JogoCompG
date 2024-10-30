using UnityEngine;

public class AlinharArticulacoesComCamera : MonoBehaviour
{
    public Transform cameraTransform;        // Referência à câmera do jogador
    public Transform[] articulacoes;         // Array das articulações a serem controladas
    public Vector3[] posicaoOffsets;         // Array de offsets de posição para cada articulação
    public Vector3[] rotacaoOffsets;         // Array de offsets de rotação para cada articulação

    void LateUpdate()
    {
        // Alinha cada articulação com a câmera
        for (int i = 0; i < articulacoes.Length; i++)
        {
            // Verifica se há offset de posição e rotação para a articulação
            if (i < posicaoOffsets.Length)
            {
                articulacoes[i].position = cameraTransform.position + cameraTransform.TransformDirection(posicaoOffsets[i]);
            }
            if (i < rotacaoOffsets.Length)
            {
                articulacoes[i].rotation = cameraTransform.rotation * Quaternion.Euler(rotacaoOffsets[i]);
            }
        }
    }
}
