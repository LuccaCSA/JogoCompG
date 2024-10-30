using UnityEngine;

public class CirculoTubarao : MonoBehaviour
{
    public Transform centro;          // O centro em torno do qual os tubarões vão girar
    public Transform[] tubaroes;      // Array de referências para os modelos dos tubarões
    public float velocidadeGiro = 30f; // Velocidade do giro dos tubarões em torno do centro

    void Update()
    {
        // Faz cada tubarão girar em torno do centro
        foreach (Transform tubarao in tubaroes)
        {
            // Mantém o tubarão girando ao redor do centro sem alterar sua rotação
            tubarao.RotateAround(centro.position, Vector3.up, velocidadeGiro * Time.deltaTime);
        }
    }
}
