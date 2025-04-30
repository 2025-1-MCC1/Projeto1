using UnityEngine;

public class PlantadorDeArvores : MonoBehaviour
{
    public GameObject arvorePrefab; // Prefab da �rvore
    public int arvoresPlantadas = 0; // Contador de �rvores
    public int objetivo = 10; // Quantas �rvores precisa plantar para completar a miss�o

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Bot�o esquerdo do mouse
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
            // Instancia a �rvore no local clicado
            Instantiate(arvorePrefab, hit.point, Quaternion.identity);

            arvoresPlantadas++;

            // Verifica se completou a miss�o
            if (arvoresPlantadas >= objetivo)
            {
                MissaoConcluida();
            }
        }
    }

    void MissaoConcluida()
    {
        Debug.Log("Miss�o Conclu�da! Voc� plantou 10 �rvores!");
        // Aqui voc� pode ativar uma HUD de vit�ria, pr�xima miss�o, etc.
    }
}
