using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI pollutionText;       // Exibe o valor do meio ambiente (plantinha)
    public TextMeshProUGUI revenueText;         // Exibe o valor do dinheiro (cifrão)
    public TextMeshProUGUI popularityEmoji;     // Exibe a popularidade em forma de emoji
    public TextMeshProUGUI populationText;      // Exibe o valor da população
    public TextMeshProUGUI environmentText;     // Exibe a % do meio ambiente

    public GameObject missionPanel;             // Painel que mostra as missões
    public TextMeshProUGUI missionText;         // Texto da missão
    public Button missionButton;                // Botão que abre o painel de missão (ícone de alvo)

    private float pollution = 0f;               // Valor de poluição atual (0 a 100, quanto maior, pior)
    private float environment = 100f;           // Valor de meio ambiente, calculado com base na poluição
    private float revenue = 0f;                 // Receita (dinheiro)
    private float popularity = 0f;              // Popularidade (influenciada pela poluição)
    private float population = 0f;              // População atual

    // Emojis usados para representar os níveis de popularidade
    private const string EMOJI_HAPPY = "\U0001F642";    // 🙂 Feliz
    private const string EMOJI_NEUTRAL = "\U0001F610";  // 😐 Neutro
    private const string EMOJI_ANGRY = "\U0001F620";    // 😠 Irritado
    private const string EMOJI_RAGING = "\U0001F621";   // 😡 Furioso

    // Dados da missão de plantar árvores
    private int treesPlanted = 0;               // Número de árvores plantadas
    private int missionGoal = 10;               // Objetivo da missão: plantar 10 árvores

    void Start()
    {
        // Inicialização com valores de teste
        AddPollution(12f);                      // Define uma poluição inicial
        AddRevenue(2400000f);                   // Define um valor de receita inicial
        AddPopularity(50f);                     // Define uma popularidade inicial

        // Atualiza o texto da missão
        missionText.text = $"\uD83C\uDF33 Plante {missionGoal} árvores\nProgresso: {treesPlanted}/{missionGoal}";
        missionPanel.SetActive(false);          // Esconde o painel de missão no início

        // Adiciona evento de clique no botão de missão
        missionButton.onClick.AddListener(ToggleMissionPanel);
    }

    void Update()
    {
        // Atualiza os elementos visuais da HUD a cada frame
        pollutionText.text = GetEnvironmentPercentage();   // Atualiza o meio ambiente
        environmentText.text = GetEnvironmentPercentage(); // Mostra a % do meio ambiente
        revenueText.text = FormatRevenue(revenue);         // Atualiza a receita
        populationText.text = FormatPopulation(population); // Atualiza a população
        UpdatePopularityEmoji();                           // Atualiza o emoji da popularidade

        // Reduz a popularidade com base na poluição atual (efeito passivo)
        float pollutionImpact = pollution * 0.01f; // exemplo: 50 poluição = -0.5 por frame
        popularity = Mathf.Clamp(popularity - pollutionImpact * Time.deltaTime, 0f, 100f);
    }

    // Converte a poluição em um valor de meio ambiente (quanto menor a poluição, melhor o ambiente)
    private string GetEnvironmentPercentage()
    {
        environment = Mathf.Clamp(100f - pollution, 0f, 100f);
        return environment.ToString("F1") + "%";
    }

    // Atualiza o emoji baseado na popularidade
    private void UpdatePopularityEmoji()
    {
        if (popularity >= 75)
            popularityEmoji.text = EMOJI_HAPPY;
        else if (popularity >= 50)
            popularityEmoji.text = EMOJI_NEUTRAL;
        else if (popularity >= 25)
            popularityEmoji.text = EMOJI_ANGRY;
        else
            popularityEmoji.text = EMOJI_RAGING;
    }

    // Formata os valores de receita para mostrar M ou K
    private string FormatRevenue(float value)
    {
        if (value >= 1000000)
            return (value / 1000000).ToString("F1") + "M";
        else if (value >= 1000)
            return (value / 1000).ToString("F1") + "K";
        else
            return value.ToString("F1");
    }

    // Formata os valores da população
    private string FormatPopulation(float value)
    {
        if (value >= 1000000)
            return (value / 1000000).ToString("F1") + "M";
        else if (value >= 1000)
            return (value / 1000).ToString("F1") + "K";
        else
            return value.ToString("F0");
    }

    // Alterna a visibilidade do painel de missão
    public void ToggleMissionPanel()
    {
        missionPanel.SetActive(!missionPanel.activeSelf);
    }

    // Chamada ao plantar uma árvore na missão
    public void PlantTree()
    {
        treesPlanted++;
        missionText.text = $"\uD83C\uDF33 Plante {missionGoal} árvores\nProgresso: {treesPlanted}/{missionGoal}";

        AddPopularity(2);          // Popularidade aumenta ao plantar árvores
        ReducePollution(2);        // Poluição reduz ao plantar árvores

        if (treesPlanted >= missionGoal)
        {
            missionText.text += "\n✅ Missão completa!";
            AddPopularity(5);      // Bônus de popularidade por concluir a missão
        }
    }

    // Aumenta a poluição
    public void AddPollution(float amount)
    {
        pollution += amount;
        pollution = Mathf.Clamp(pollution, 0f, 100f);
    }

    // Reduz a poluição
    public void ReducePollution(float amount)
    {
        pollution -= amount;
        pollution = Mathf.Clamp(pollution, 0f, 100f);
    }

    // Aumenta a receita
    public void AddRevenue(float amount)
    {
        revenue += amount;
    }

    // Aumenta a popularidade de forma segura
    public void AddPopularity(float amount)
    {
        popularity = Mathf.Clamp(popularity + amount, 0f, 100f);
    }

    // Aumenta a população
    public void AddPopulation(float amount)
    {
        population += amount;
    }
}
