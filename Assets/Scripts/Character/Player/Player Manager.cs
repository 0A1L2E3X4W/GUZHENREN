using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Header("MANAGERS")]
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;

    [Header("PLAYER STATUS")]
    public bool isSprinting = false;

    protected override void Awake()
    {
        base.Awake();

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    protected override void Start()
    {
        base.Start();

        PlayerInputManager.Instance.player = this;
        PlayerCamera.Instance.player = this;
    }

    protected override void Update()
    {
        base.Update();

        playerLocomotionManager.HandleAllMovement();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        PlayerCamera.Instance.HandleAllCameraActions();
    }
}
