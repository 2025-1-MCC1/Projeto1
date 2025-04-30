using UnityEngine;

public class PlantadorDeArvores : MonoBehaviour
{
    public GameObject arvorePrefab; // Prefab da Árvore
    public int arvoresPlantadas = 0; // Contador de árvores
    public int objetivo = 10; // Quantas árvores precisa plantar para completar a missão

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botão esquerdo do mouse
        {
            PlantarArvore();
        }
    }

    void PlantarArvore()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Instancia a árvore no local clicado
            Instantiate(arvorePrefab, hit.point, Quaternion.identity);

            arvoresPlantadas++;

            // Verifica se completou a missão
            if (arvoresPlantadas >= objetivo)
            {
                MissaoConcluida();
            }
        }
    }

    void MissaoConcluida()
    {
        Debug.Log("Missão Concluída! Você plantou 10 árvores!");
        // Aqui você pode ativar uma HUD de vitória, próxima missão, etc.
    }
}
