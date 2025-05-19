using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionPanel;

    void Start()
    {
        instructionPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo enquanto l� instru��es
    }

    public void ShowInstructions()
    {
        Debug.Log("Mostrar instru��es");
        instructionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideInstructions()
    {
        Debug.Log("Bot�o 'Fechar' foi clicado");
        instructionPanel.SetActive(false);
        Time.timeScale = 1f; // Volta ao normal ao fechar instru��es
    }
}
