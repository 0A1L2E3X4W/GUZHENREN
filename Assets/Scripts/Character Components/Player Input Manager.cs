using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    [Header("MANAGER")]
    public PlayerManager player;

    [Header("INPUT SYSTEM")]
    private PlayerControls playerControls;

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] private Vector2 movementInput;
    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float moveAmount;

    [Header("PLAYER CAMERA INPUT")]
    [SerializeField] private Vector2 cameraInput;
    [HideInInspector] public float cameraVerticalInput;
    [HideInInspector] public float cameraHorizontalInput;

    [Header("PLAYER ACTION INPUT")]
    [SerializeField] private bool sprintInput = false;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChange;
        Instance.enabled = false;

        if (playerControls != null)
        {
            playerControls.Disable();
        }
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        if (newScene.buildIndex == WorldSaveGameManager.Instance.GetTestWorldSceneIndex())
        {
            Instance.enabled = true;

            if (playerControls != null)
            {
                playerControls.Enable();
            }
        }
        else
        {
            Instance.enabled = false;

            if (playerControls != null)
            {
                playerControls.Disable();
            }
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

            // HOLD ACTION
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
        }

        playerControls.Enable();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus) { playerControls.Enable(); }
            else { playerControls.Disable(); }
        }
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {
        HandleMovementInput();
        HandleCameraInput();

        HandleSprintInput();
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

        if (moveAmount != 0f) { player.isMoving = true; }
        else { player.isMoving = false; }

        //if (!player.playerNetworkManager.isLockedOn.Value || player.playerNetworkManager.isSprinting.Value)
        //{
        //    player.playerAnimatorManager.UpdateAnimatorMovementParams(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
        //}
        //else
        //{
        //    player.playerAnimatorManager.UpdateAnimatorMovementParams(horizontalInput, verticalInput, player.playerNetworkManager.isSprinting.Value);
        //}

        player.playerAnimatorManager.UpdateAnimatorMovementParams(0, moveAmount, player.isSprinting);
    }

    // CAMERA
    private void HandleCameraInput()
    {
        cameraHorizontalInput = cameraInput.x;
        cameraVerticalInput = cameraInput.y;
    }

    private void HandleSprintInput()
    {
        if (sprintInput)
        {
            player.playerLocomotionManager.HandleSprinting();
        }
        else
        {
            player.isSprinting = false;
        }
    }
}
