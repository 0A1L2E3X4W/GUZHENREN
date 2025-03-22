using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    [Header("MANAGER")]
    private CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
}
