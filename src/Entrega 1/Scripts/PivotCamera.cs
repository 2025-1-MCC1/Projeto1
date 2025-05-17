using UnityEngine;

public class PivotCamera : MonoBehaviour
{
    [Header("Movimentação")]
    public float baseMoveSpeed = 20f;
    public float sprintMultiplier = 2f;
    public float edgeScrollSpeed = 30f;
    public float screenEdgeBorder = 10f;

    [Header("Zoom")]
    public float zoomSpeed = 1000f;
    public Transform cameraTransform;

    [Header("Rotação")]
    public float rotationSpeed = 100f;
    private float currentRotationY = 0f;
    private float currentRotationX = 45f; // Ângulo inicial vertical
    public bool useMouseRotation = true;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (cameraTransform == null) cameraTransform = cam.transform;

        // Travar e esconder cursor
        
      

        currentRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    void HandleMovement()
    {
        float moveSpeed = baseMoveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            moveSpeed *= sprintMultiplier;

        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += transform.forward;
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;
        if (Input.GetKey(KeyCode.D)) move += transform.right;
        if (Input.GetKey(KeyCode.A)) move -= transform.right;

        if (Input.mousePosition.x >= Screen.width - screenEdgeBorder) move += transform.right;
        if (Input.mousePosition.x <= screenEdgeBorder) move -= transform.right;
        if (Input.mousePosition.y >= Screen.height - screenEdgeBorder) move += transform.forward;
        if (Input.mousePosition.y <= screenEdgeBorder) move -= transform.forward;

        move.y = 0;
        transform.position += move.normalized * moveSpeed * Time.deltaTime;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float zoomAmount = scroll * zoomSpeed * Time.deltaTime;
            cameraTransform.localPosition += cameraTransform.forward * zoomAmount;
        }
    }

    void HandleRotation()
    {
        // Teclado (Q e E)
        if (Input.GetKey(KeyCode.Q)) currentRotationY -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) currentRotationY += rotationSpeed * Time.deltaTime;

        // Mouse (botão direito)
        if (useMouseRotation && Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            currentRotationY += mouseX;
            currentRotationX += mouseY;
        }

        // Aplicar rotação: eixo X na câmera, eixo Y no pivot
        currentRotationX = Mathf.Clamp(currentRotationX, -89f, 89f); // evita rotação completa vertical
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
        cameraTransform.localRotation = Quaternion.Euler(currentRotationX, 0f, 0f);
    }
}