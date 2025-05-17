using UnityEngine;

public class PlantadorDeArvores : MonoBehaviour
{
    public GameObject arvorePrefab;  // Prefab da árvore que será plantada no ponto de plantio
    public HUDManager hudManager;    // Referência para o HUDManager para atualizar missões e HUD

    void Update()
    {
        // Detecta clique do mouse (botão esquerdo)
        if (Input.GetMouseButtonDown(0))
        {
            // Cria um raio da câmera na posição do mouse na tela
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Verifica se o raio colidiu com algum objeto no mundo
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Tenta pegar o componente PontodePlantio no objeto que foi clicado
                PontodePlantio ponto = hit.collider.GetComponent<PontodePlantio>();

                // Verifica se encontrou um ponto válido para plantar e se ainda não foi plantado
                if (ponto != null && !ponto.jaPlantado)
                {
                    // Executa o método de plantar árvore no ponto com o prefab da árvore
                    ponto.Plantar(arvorePrefab);

                    // Se a referência para o HUDManager estiver configurada
                    if (hudManager != null)
                    {
                        // Atualiza o progresso da missão de plantar árvore na HUD
                        hudManager.PlantTree(); // Também chama MostrarMissao() se a missão for concluída
                    }
                }
            }
        }
    }
}
