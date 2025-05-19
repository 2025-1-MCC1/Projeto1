using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionPanel;

    void Start()
    {
        instructionPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo enquanto lê instruções
    }

    public void ShowInstructions()
    {
        Debug.Log("Mostrar instruções");
        instructionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HideInstructions()
    {
        Debug.Log("Botão 'Fechar' foi clicado");
        instructionPanel.SetActive(false);
        Time.timeScale = 1f; // Volta ao normal ao fechar instruções
    }
}
