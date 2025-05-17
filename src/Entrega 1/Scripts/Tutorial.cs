using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        tutorialPanel.SetActive(true); // Mostrar tutorial no come�o
        Time.timeScale = 0f; // Pausar o jogo enquanto o tutorial est� ativo (opcional)
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f; // Retomar o jogo
    }
}
