using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    [Header("MANAGER")]
    public PlayerManager player;

    [Header("CAMERA OBJECTS")]
    public Camera cameraObj;
    public Transform cameraPivotTransform;

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
