using UnityEngine;
using TMPro; // Importação para TextMeshPro

public class NPCAutoMovement : MonoBehaviour
{
    // Referência ao Animator Controller (já configurado no NPC)
    public Animator npcAnimator;

    // Referência ao texto TMP no Canvas para exibir o estado
    public TMP_Text keyPressedText;

    // Tempo entre mudanças de estado
    public float switchTime = 2f;

    // Variáveis internas para controle
    private float timeCounter = 0f;
    private int currentState = 0; // Estado atual (0 = W, 1 = S, 2 = A, 3 = D)

    void Update()
    {
        // Incrementa o contador de tempo
        timeCounter += Time.deltaTime;

        // Verifica se é hora de trocar o estado
        if (timeCounter >= switchTime)
        {
            timeCounter = 0f; // Reseta o contador
            currentState = (currentState + 1) % 4; // Alterna entre 0, 1, 2, 3
            UpdateAnimatorState();
        }
    }

    // Atualiza o estado do NPC no Animator Controller
    void UpdateAnimatorState()
    {
        npcAnimator.SetInteger("Movimento", currentState);

        // Ativa o parâmetro correto e atualiza o texto TMP
        switch (currentState)
        {
            case 0: // W - Frente
                
                keyPressedText.text = "W";
                break;

            case 1: // S - Trás
                keyPressedText.text = "S";
                break;

            case 2: // A - Esquerda
                keyPressedText.text = "A";
                break;

            case 3: // D - Direita
                keyPressedText.text = "D";
                break;
        
    }
    }

}
