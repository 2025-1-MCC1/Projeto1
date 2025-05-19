using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // N�o destruir ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Evita duplicatas
        }
    }
}
