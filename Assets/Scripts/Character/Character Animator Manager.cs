using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    [Header("MANAGER")]
    private CharacterManager character;

    private int anim_vertical;
    private int anim_horizontal;
    private int anim_isMoving;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();

        anim_vertical = Animator.StringToHash("Vertical");
        anim_horizontal = Animator.StringToHash("Horizontal");
        anim_isMoving = Animator.StringToHash("IsMoving");
    }

    public virtual void UpdateAllAnimatorParams()
    {
        character.anim.SetBool(anim_isMoving, character.isMoving);
    }

    // LOCOMOTION
    public void UpdateAnimatorMovementParams(float horizontalVal, float verticalVal, bool isSprinting)
    {
        float snappedHorizontal = horizontalVal;
        float snappedVertical = verticalVal;

        if (horizontalVal > 0f && horizontalVal <= 0.5f) { snappedHorizontal = 0.5f; }
        else if (horizontalVal > 0.5f && horizontalVal <= 1f) { snappedHorizontal = 1f; }
        else if (horizontalVal < -0f && horizontalVal >= -0.5f) { snappedHorizontal = -0.5f; }
        else if (horizontalVal < -0.5f && horizontalVal >= -1f) { snappedHorizontal = -1f; }
        else { snappedHorizontal = 0f; }

        if (verticalVal > 0f && verticalVal <= 0.5f) { snappedVertical = 0.5f; }
        else if (verticalVal > 0.5f && verticalVal <= 1f) { snappedVertical = 1f; }
        else if (verticalVal < -0f && verticalVal >= -0.5f) { snappedVertical = -0.5f; }
        else if (verticalVal < -0.5f && verticalVal >= -1f) { snappedVertical = -1f; }
        else { snappedVertical = 0f; }

        if (isSprinting) { snappedVertical = 2; }

        character.anim.SetFloat(anim_horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        character.anim.SetFloat(anim_vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
