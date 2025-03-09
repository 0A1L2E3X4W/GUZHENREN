using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("MANAGERS")]
    [HideInInspector] public CharacterLocomotionManager characterLocomotionManager;

    [Header("CHARACTER ESSENTICALS")]
    [HideInInspector] public Animator anim;
    [HideInInspector] public CharacterController characterController;

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void LateUpdate()
    {
        
    }
}
