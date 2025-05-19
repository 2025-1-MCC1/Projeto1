using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Este método será chamado quando o jogador clicar em "Jogar"
    public void PlayGame()
    {
        // Carrega a cena do jogo (certifique-se de adicionar a cena nas configurações de build!)
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}