using UnityEngine;

public class MissaoConcluidaManager : MonoBehaviour
{
    [Tooltip("Painel que será mostrado quando a missão for concluída")]
    public GameObject missaoPanel;  // Painel UI para conclusão de missão

    public float tempoExibicao = 1.5f; // Tempo em segundos que o painel fica visível

    void Start()
    {
        // Garante que comece invisível
        if (missaoPanel != null)
            missaoPanel.SetActive(false);
    }

    // Método público para mostrar o painel
    public void MostrarMissao()
    {
        if (missaoPanel != null)
        {
            missaoPanel.SetActive(true);
            Invoke(nameof(EsconderMissao), tempoExibicao);
        }
    }

    // Método privado para esconder o painel
    void EsconderMissao()
    {
        if (missaoPanel != null)
            missaoPanel.SetActive(false);
    }
}