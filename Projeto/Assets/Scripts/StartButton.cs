using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    public void IniciarJogo()
    {
        SceneManager.LoadScene(1);
    }
}
