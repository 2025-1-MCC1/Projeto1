using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void QuitApp()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit(); // Funciona em build, não no editor
    }
}