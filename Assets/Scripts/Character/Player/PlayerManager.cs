using UnityEngine;

public class PlayerManager : CharacterManager
{
    [Header("MANAGERS")]
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;

    protected override void Awake()
    {
        base.Awake();

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
    }

    protected override void Start()
    {
        base.Start();

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

        // CAMERA
        PlayerCamera.Instance.HandleAllCameraActions();
    }
}
