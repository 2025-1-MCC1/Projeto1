using UnityEngine;

public class MissaoConcluidaManager : MonoBehaviour
{
    [Tooltip("Painel que ser� mostrado quando a miss�o for conclu�da")]
    public GameObject missaoPanel;  // Painel UI para conclus�o de miss�o

    public float tempoExibicao = 1.5f; // Tempo em segundos que o painel fica vis�vel

    void Start()
    {
        // Garante que comece invis�vel
        if (missaoPanel != null)
            missaoPanel.SetActive(false);
    }

    // M�todo p�blico para mostrar o painel
    public void MostrarMissao()
    {
        if (missaoPanel != null)
        {
            missaoPanel.SetActive(true);
            Invoke(nameof(EsconderMissao), tempoExibicao);
        }
    }

    // M�todo privado para esconder o painel
    void EsconderMissao()
    {
        if (missaoPanel != null)
            missaoPanel.SetActive(false);
    }
}