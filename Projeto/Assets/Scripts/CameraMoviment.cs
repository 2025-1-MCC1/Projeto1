using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float edgeSize = 10f;
    public float scrollSpeed = 1000f;
    public float minY = 20f;
    public float maxY = 120f;

    void Update()
    {
        Vector3 pos = transform.position;

        // Movimento com botão direito do mouse pressionado
        if (Input.GetMouseButton(1))
        {
            // Movimento com teclado ou mouse nas bordas
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - edgeSize)
                pos += transform.forward * moveSpeed * Time.deltaTime;

            if (Input.GetKey("s") || Input.mousePosition.y <= edgeSize)
                pos -= transform.forward * moveSpeed * Time.deltaTime;

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - edgeSize)
                pos += transform.right * moveSpeed * Time.deltaTime;

            if (Input.GetKey("a") || Input.mousePosition.x <= edgeSize)
                pos -= transform.right * moveSpeed * Time.deltaTime;
        }

        // Zoom com scroll (independente do botão pressionado)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
