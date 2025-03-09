using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    [Header("MANAGER")]
    public PlayerManager player;

    [Header("INPUT SYSTEM")]
    private PlayerControls playerControls;

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("PLAYER CAMERA INPUT")]
    [SerializeField] private Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus) { playerControls.Enable(); }
            else { playerControls.Disable(); }
        }
    }

    private void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();
    }

    // PLAYER MOVEMENT
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (moveAmount <= 0.5f && moveAmount > 0) { moveAmount = 0.5f; }
        else if (moveAmount <= 1f && moveAmount > 0.5f) { moveAmount = 1f; }

        if (player == null)
            return;
    }

    // CAMERA
    private void HandleCameraInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }
}
