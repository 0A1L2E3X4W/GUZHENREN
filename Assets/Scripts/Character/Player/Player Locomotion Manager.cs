using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    [Header("MANAGER")]
    private PlayerManager player;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    [Header("MOVEMENT SETTINGS")]
    [SerializeField] private float walkingSpeed = 1.5f;
    [SerializeField] private float runningSpeed = 3.5f;
    [SerializeField] private float rotationSpeed = 15f;
    private Vector3 moveDirection;
    private Vector3 targetRotateDirection;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        HandleRotation();
    }

    // GROUND MOVEMENT
    private void GetMovementParams()
    {
        verticalMovement = PlayerInputManager.Instance.verticalInput;
        horizontalMovement = PlayerInputManager.Instance.horizontalInput;
        moveAmount = PlayerInputManager.Instance.moveAmount;
    }

    private void HandleGroundedMovement()
    {
        if (player.canMove || player.canRotate)
        {
            GetMovementParams();
        }

        if (!player.canMove)
            return;

        moveDirection = PlayerCamera.Instance.transform.forward * verticalMovement;
        moveDirection += PlayerCamera.Instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        if (PlayerInputManager.Instance.moveAmount > 0.5f)
        {
            player.characterController.Move(runningSpeed * Time.deltaTime * moveDirection);
        }
        else if (PlayerInputManager.Instance.moveAmount <= 0.5f)
        {
            player.characterController.Move(walkingSpeed * Time.deltaTime * moveDirection);
        }
    }

    private void HandleRotation()
    {
        //if (player.isDead.Value)
        //    return;

        if (!player.canRotate)
            return;

        targetRotateDirection = Vector3.zero;
        targetRotateDirection = PlayerCamera.Instance.cameraObj.transform.forward * verticalMovement;
        targetRotateDirection += PlayerCamera.Instance.cameraObj.transform.right * horizontalMovement;
        targetRotateDirection.Normalize();
        targetRotateDirection.y = 0f;

        if (targetRotateDirection == Vector3.zero)
        {
            targetRotateDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotateDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }

    // PLAYER ACTIONS
    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            player.isSprinting = false;
        }

        if (moveAmount >= 0.5f)
        {
            player.isSprinting = true;
        }
        else
        {
            player.isSprinting = false;
        }
    }
}
