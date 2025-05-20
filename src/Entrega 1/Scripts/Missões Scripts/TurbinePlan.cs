using UnityEngine;
using UnityEngine.UI;

public class TurbinePlacer : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject turbinePrefab;           // Prefab da turbina final
    public GameObject turbinePreviewPrefab;    // Prefab de pré-visualização

    [Header("Custos e Benefícios")]
    public float turbineCost = 25000f;         // Custo para colocar turbina
    public float pollutionReduction = 3f;      // Redução de poluição por instalação
    public float popularityIncrease = 5f;      // Aumento de popularidade

    public static TurbinePlacer Instance { get; private set; }

    [Header("UI")]
    public Button activatePlacementButton;     // Botão para ativar modo de posicionamento

    private GameObject previewInstance;       // Instância da pré-visualização
    private HUDManager hudManager;            // Referência ao gerenciador de HUD
    private bool isPlacing = false;           // Flag de posicionamento ativo?
    private float currentRotation = 0f;       // Rotação atual para instanciar

    public bool IsPlacing => isPlacing;       // Propriedade de leitura externa

    private Animator previewAnimator;         // Animator para feedback de posição válida

    void Awake()
    {
        // Singleton básico
        Instance = this;
    }

    void Start()
    {
        // Pega referência ao HUDManager na cena
        hudManager = FindObjectOfType<HUDManager>();

        // Instancia pré-visualização e esconde
        previewInstance = Instantiate(turbinePreviewPrefab);
        previewInstance.SetActive(false);

        // Pega Animator para mudar cor/estado
        previewAnimator = previewInstance.GetComponent<Animator>();

        // Configura botão de ativar
        activatePlacementButton.onClick.AddListener(TogglePlacementMode);
    }

    void Update()
    {
        // Se não está posicionando, nada a fazer
        if (!isPlacing) return;

        UpdatePreviewPosition();
        HandleRotation();

        // Clique para tentar colocar
        if (Input.GetMouseButtonDown(0))
            TryPlaceTurbine();

        // Esc cancela posicionamento
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelPlacement();
    }

    // Alterna entre modo de posicionamento e normal
    public void TogglePlacementMode()
    {
        isPlacing = !isPlacing;
        previewInstance.SetActive(isPlacing);
        if (isPlacing && previewAnimator != null)
            previewAnimator.Play("ValidPlacement");
    }

    // Atualiza posição e feedback visual da pré-visualização
    void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                // Move e rotaciona preview
                previewInstance.SetActive(true);
                previewInstance.transform.position = hit.point;
                previewInstance.transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

                // Seta estado se jogador pode pagar
                bool canAfford = hudManager.GetRevenue() >= turbineCost;
                previewAnimator.SetBool("CanPlace", canAfford);
            }
            else
            {
                previewInstance.SetActive(false);
            }
        }
        else previewInstance.SetActive(false);
    }

    // Rotaciona preview a cada clique direito
    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentRotation = (currentRotation + 45f) % 360f;
        }
    }

    // Tenta instanciar turbina
    void TryPlaceTurbine()
    {
        if (!previewInstance.activeSelf) return;
        float currentRevenue = hudManager.GetRevenue();
        if (currentRevenue >= turbineCost)
        {
            // Deduz custo, instancia objeto e atualiza HUD
            hudManager.AddRevenue(-turbineCost);
            Instantiate(turbinePrefab, previewInstance.transform.position, Quaternion.Euler(0f, currentRotation, 0f));
            hudManager.ReducePollution(pollutionReduction);
            hudManager.AddPopularity(popularityIncrease);
            hudManager.InstallWindTurbine();
        }
        else
        {
            Debug.Log("Dinheiro insuficiente!");
        }
    }

    // Cancela posicionamento e esconde preview
    public void CancelPlacement()
    {
        isPlacing = false;
        previewInstance.SetActive(false);
    }
}