using UnityEngine;

public class PontodePlantio : MonoBehaviour
{
    public bool jaPlantado = false;
    public GameObject marcadorVisual; // Opcional: para ocultar o cubo ap�s plantar, se quiser

    public void Plantar(GameObject prefab)
    {
        if (jaPlantado) return;

        // Calcula a posi��o levemente acima do cubo (altura 0.1)
        Vector3 pos = transform.position + Vector3.up * 0.05f;

        // Instancia a �rvore
        Instantiate(prefab, pos, Quaternion.identity);
        jaPlantado = true;

        // Oculta o marcador visual se desejar
        if (marcadorVisual != null)
        {
            marcadorVisual.SetActive(false);
        }
    }
}