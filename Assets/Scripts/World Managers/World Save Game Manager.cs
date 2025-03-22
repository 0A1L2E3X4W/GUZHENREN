using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager Instance;

    [Header("WORLD SCENE INDEX")]
    [SerializeField] private int testWorldSceneIndex = 1;

    [Header("PLAYER PERFAB")]
    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadPlayerPerfab()
    {
        Instantiate(playerPrefab);
    }

    public void LoadWorldScene(int worldSceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
    }

    public int GetTestWorldSceneIndex()
    {
        return testWorldSceneIndex;
    }
}
