using UnityEngine;

public class CityCameraController : MonoBehaviour
{
    public Transform cameraTransform; // arraste a câmera aqui no Inspector
    public float moveSpeed = 20f;
    public float rotationSpeed = 5f;
    public float scrollSpeed = 1000f;

    public float minY = 20f;
    public float maxY = 120f;
    public float minTilt = 20f;
    public float maxTilt = 80f;

    private float currentTilt;

    void Start()
    {
        currentTilt = cameraTransform.localEulerAngles.x;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    void HandleMovement()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey("w")) direction += transform.forward;
        if (Input.GetKey("s")) direction -= transform.forward;
        if (Input.GetKey("d")) direction += transform.right;
        if (Input.GetKey("a")) direction -= transform.right;

        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Rotaciona o pivot (horizontal - Y)
            transform.Rotate(0f, mouseX, 0f);

            // Tilt (rotação no eixo X da câmera)
            currentTilt -= mouseY;
            currentTilt = Mathf.Clamp(currentTilt, minTilt, maxTilt);
            cameraTransform.localEulerAngles = new Vector3(currentTilt, 0f, 0f);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = cameraTransform.localPosition;

        pos.y -= scroll * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        cameraTransform.localPosition = pos;
    }
}
