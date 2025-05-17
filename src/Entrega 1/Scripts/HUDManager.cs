using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Mission
{
    public string name;      // Nome da missão
    public int progress;     // Progresso atual da missão
    public int goal;         // Meta para completar a missão
    public bool completed;   // Indica se a missão foi concluída

    // Construtor para criar uma nova missão com nome e objetivo
    public Mission(string name, int goal)
    {
        this.name = name;
        this.goal = goal;
        this.progress = 0;
        this.completed = false;
    }

    // Retorna o status da missão: se concluída ou progresso atual
    public string GetStatus()
    {
        return completed ? "✅ Missão completa!" : $"Progresso: {progress}/{goal}";
    }
}

public class HUDManager : MonoBehaviour
{
    [Header("Missões")]
    public TextMeshProUGUI missionText;       // Texto que mostra a missão atual e progresso
    public GameObject missionPanel;           // Painel da lista de missões
    public Button missionButton;              // Botão para abrir/fechar painel de missões
    public Button nextMissionButton;          // Botão para alternar missão atual

    private List<Mission> missions = new List<Mission>();  // Lista com as missões do jogo
    private int currentMissionIndex = 0;                   // Índice da missão atualmente mostrada

    [Header("Missão Concluída (painel)")]
    public MissaoConcluidaManager missaoConcluidaManager;  // Gerenciador do painel de missão concluída

    public GameObject missionCompletePanel;           // Painel que aparece ao concluir todas as missões
    public TextMeshProUGUI missionCompleteText;       // Texto que exibe a missão concluída
    [SerializeField] private float missionPanelDuration = 3f; // Tempo que o painel fica visível
    private Coroutine currentMissionCompleteRoutine;         // Rotina para esconder painel após delay

    [Header("Indicadores")]
    public TextMeshProUGUI pollutionText;      // Texto para mostrar a poluição
    public TextMeshProUGUI revenueText;        // Texto para mostrar o dinheiro/revenue
    public TextMeshProUGUI popularityText;     // Texto para mostrar popularidade
    public TextMeshProUGUI popularityEmoji;    // Emoji que representa o humor da população
    public TextMeshProUGUI populationText;     // Texto que mostra população
    public TextMeshProUGUI environmentText;    // Texto para mostrar indicador ambiental

    // Variáveis internas para armazenar valores dos indicadores
    private float pollution = 100f;
    private float popularity = 40f;
    private float revenue = 500000f;
    private float population = 2500f;

    [Header("Game Over")]
    public GameObject gameOverPanel;           // Painel de fim de jogo

    // Emojis para diferentes níveis de popularidade
    private const string EMOJI_HAPPY = "\U0001F604";
    private const string EMOJI_NEUTRAL = "\U0001F610";
    private const string EMOJI_ANGRY = "\U0001F620";
    private const string EMOJI_RAGING = "\U0001F621";

    void Start()
    {
        // Inicializa a lista de missões
        missions.Add(new Mission("🌳 Plante árvores", 16));
        missions.Add(new Mission("♻️ Recolha lixo", 14));
        missions.Add(new Mission("💨 Instale turbinas eólicas", 10));

        missionPanel.SetActive(false); // Painel de missões começa fechado

        // Configura os botões
        missionButton.onClick.AddListener(ToggleMissionPanel);
        nextMissionButton.onClick.AddListener(NextMission);

        UpdateMissionUI(); // Atualiza a missão inicial exibida
    }

    void Update()
    {
        // Atualiza indicadores na HUD a cada frame
        pollutionText.text = pollution.ToString("F1") + "%";
        environmentText.text = (100f - pollution).ToString("F1") + "%";
        revenueText.text = FormatRevenue(revenue);
        populationText.text = FormatPopulation(population);
        popularityText.text = popularity.ToString("F0") + "%";

        if (popularity <= 0 && !gameOverPanel.activeSelf)
        {
            TriggerGameOver(); // Dispara game over se a popularidade zerar
        }

        UpdatePopularityEmoji(); // Atualiza emoji conforme humor da população

        // Popularidade cai de forma contínua proporcional à poluição
        float pollutionImpact = pollution * 0.03f;
        popularity = Mathf.Clamp(popularity - pollutionImpact * Time.deltaTime, 0f, 100f);
    }

    // Ativa painel de fim de jogo e pausa o tempo
    private void TriggerGameOver()
    {
        popularity = 0f;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Alterna a visibilidade do painel de missões
    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(!missionPanel.activeSelf);
        UpdateMissionUI();
    }

    // Avança para próxima missão na lista
    public void NextMission()
    {
        currentMissionIndex = (currentMissionIndex + 1) % missions.Count;
        UpdateMissionUI();
    }

    // Atualiza texto da missão atual
    private void UpdateMissionUI()
    {
        Mission mission = missions[currentMissionIndex];
        missionText.text = $"{mission.name}\n{mission.GetStatus()}";
    }

    // =====================
    // MÉTODOS DE MISSÃO
    // =====================

    public void PlantTree()
    {
        Mission treeMission = missions[0];
        if (!treeMission.completed)
        {
            treeMission.progress++;
            if (treeMission.progress >= treeMission.goal)
            {
                treeMission.completed = true;
                AddPopularity(5);
                MostrarMissaoConcluida();
            }
            else
            {
                AddPopularity(1.1f);
                ReducePollution(1.5f);
            }
        }
        UpdateMissionUI();
    }

    public void CollectTrash()
    {
        Mission trashMission = missions[1];
        if (!trashMission.completed)
        {
            trashMission.progress++;
            if (trashMission.progress >= trashMission.goal)
            {
                trashMission.completed = true;
                AddPopularity(5);
                MostrarMissaoConcluida();
            }
            else
            {
                AddPopularity(1.1f);
                ReducePollution(2.5f);
            }
        }
        UpdateMissionUI();
    }

    public void InstallWindTurbine()
    {
        Mission windMission = missions[2];
        if (!windMission.completed)
        {
            windMission.progress++;
            if (windMission.progress >= windMission.goal)
            {
                windMission.completed = true;
                AddPopularity(10);
                MostrarMissaoConcluida();
            }
            else
            {
                AddPopularity(1.1f);
                ReducePollution(4.5f);
            }
        }
        UpdateMissionUI();
    }

    // Mostra painel de missão concluída
    private void MostrarMissaoConcluida()
    {
        if (missaoConcluidaManager != null)
            missaoConcluidaManager.MostrarMissao();

        CheckAllMissionsCompleted();
    }

    // Verifica se todas missões foram concluídas
    private void CheckAllMissionsCompleted()
    {
        bool allCompleted = missions.TrueForAll(m => m.completed);
        if (allCompleted && missionCompletePanel != null && !missionCompletePanel.activeSelf)
        {
            ShowMissionCompletePanel();
        }
    }

    // Mostra painel de todas as missões concluídas
    private void ShowMissionCompletePanel()
    {
        missionCompletePanel.SetActive(true);
        Invoke("HideMissionCompletePanel", missionPanelDuration);
    }

    // Esconde o painel de missão completa
    private void HideMissionCompletePanel()
    {
        missionCompletePanel.SetActive(false);
    }

    // Atualiza emoji de popularidade baseado na faixa
    private void UpdatePopularityEmoji()
    {
        if (popularity >= 70)
            popularityEmoji.text = EMOJI_HAPPY;
        else if (popularity >= 30)
            popularityEmoji.text = EMOJI_NEUTRAL;
        else if (popularity >= 20)
            popularityEmoji.text = EMOJI_ANGRY;
        else
            popularityEmoji.text = EMOJI_RAGING;
    }

    // =====================
    // MÉTODOS AUXILIARES
    // =====================

    public void AddPopularity(float amount)
    {
        popularity = Mathf.Clamp(popularity + amount, 0f, 100f);
    }

    public void AddPollution(float amount)
    {
        pollution = Mathf.Clamp(pollution + amount, 0f, 100f);
    }

    public void ReducePollution(float amount)
    {
        pollution = Mathf.Clamp(pollution - amount, 0f, 100f);
    }

    public void AddRevenue(float amount)
    {
        revenue += amount;
        UpdateUI();
    }

    public void SpendRevenue(float amount)
    {
        revenue = Mathf.Max(0f, revenue - amount);
    }

    // Formata valor de dinheiro para K e M
    private string FormatRevenue(float value)
    {
        if (value >= 1000000) return (value / 1000000f).ToString("F1") + "M";
        if (value >= 1000) return (value / 1000f).ToString("F1") + "K";
        return value.ToString("F1");
    }

    // Formata população
    private string FormatPopulation(float value)
    {
        if (value >= 1000000) return (value / 1000000f).ToString("F1") + "M";
        if (value >= 1000) return (value / 1000f).ToString("F1") + "K";
        return value.ToString("F0");
    }

    // Atualiza indicadores manuais
    public void UpdateUI()
    {
        revenueText.text = "Dinheiro: " + FormatRevenue(revenue);
        populationText.text = FormatPopulation(population);
        // Atualize outros indicadores se quiser
    }

    public float GetRevenue()
    {
        return revenue;
    }

    // Reinicia o jogo do zero
    public void RestartGame()
    {
        popularity = 35f;
        pollution = 100f;
        revenue = 500000f;
        population = 2500f;

        foreach (var mission in missions)
        {
            mission.progress = 0;
            mission.completed = false;
        }

        currentMissionIndex = 0;
        UpdateMissionUI();

        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
