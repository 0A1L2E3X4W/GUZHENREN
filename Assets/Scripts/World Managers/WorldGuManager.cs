using UnityEngine;

public class WorldGuManager : MonoBehaviour
{
    public static WorldGuManager Instance;

    [Header("RANK ONE GU")]
    public MortalGu[] rankOneGu;

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
