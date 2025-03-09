using UnityEngine;

public class WorldAnimationManager : MonoBehaviour
{
    public static WorldAnimationManager Instance;

    [Header("PLAYER ANIMATIONS")]
    public string deathAnim = "Death_01";

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
