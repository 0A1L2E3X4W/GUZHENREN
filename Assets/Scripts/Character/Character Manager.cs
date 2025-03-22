using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("CHARACTER COMPONENTS")]
    [HideInInspector] public Animator anim;
    [HideInInspector] public CharacterController characterController;

    [Header("MANAGERS")]
    [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;
    [HideInInspector] public CharacterLocomotionManager characterLocomotionManager;

    [Header("FLAGS")]
    public bool isPerformingAction = false;
    public bool applyRootMotion = false;
    public bool canRotate = true;
    public bool canMove = true;
    public bool isGrounded = true;

    [Header("STATUS")]
    public bool isMoving = false;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        characterAnimatorManager.UpdateAllAnimatorParams();
    }

    protected virtual void LateUpdate()
    {
        
    }
}
