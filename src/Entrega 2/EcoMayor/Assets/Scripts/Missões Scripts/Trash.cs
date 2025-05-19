using UnityEngine;

public class Trash : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    ColetarLixo();
                }
            }
        }
    }

    private void ColetarLixo()
    {
        HUDManager hud = FindFirstObjectByType<HUDManager>();
        if (hud != null)
        {
            hud.CollectTrash(); // Já chama MostrarMissao() internamente, se aplicável
        }
        Destroy(gameObject);
    }
}