using UnityEngine;

public class PlantadorDeArvores : MonoBehaviour
{
    public GameObject arvorePrefab;  // Prefab da �rvore que ser� plantada no ponto de plantio
    public HUDManager hudManager;    // Refer�ncia para o HUDManager para atualizar miss�es e HUD

    void Update()
    {
        // Detecta clique do mouse (bot�o esquerdo)
        if (Input.GetMouseButtonDown(0))
        {
            // Cria um raio da c�mera na posi��o do mouse na tela
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Verifica se o raio colidiu com algum objeto no mundo
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Tenta pegar o componente PontodePlantio no objeto que foi clicado
                PontodePlantio ponto = hit.collider.GetComponent<PontodePlantio>();

                // Verifica se encontrou um ponto v�lido para plantar e se ainda n�o foi plantado
                if (ponto != null && !ponto.jaPlantado)
                {
                    // Executa o m�todo de plantar �rvore no ponto com o prefab da �rvore
                    ponto.Plantar(arvorePrefab);

                    // Se a refer�ncia para o HUDManager estiver configurada
                    if (hudManager != null)
                    {
                        // Atualiza o progresso da miss�o de plantar �rvore na HUD
                        hudManager.PlantTree(); // Tamb�m chama MostrarMissao() se a miss�o for conclu�da
                    }
                }
            }
        }
    }
}
