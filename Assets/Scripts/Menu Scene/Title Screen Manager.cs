using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager Instance;

    [Header("MENUS")]
    [SerializeField] private GameObject mainMenu;

    [Header("BUTTONS")]
    [SerializeField] private Button button_startGame;
    [SerializeField] private Button button_enterTestWorld;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void StartGame()
    {
        button_startGame.gameObject.SetActive(false);
        mainMenu.SetActive(true);
        button_enterTestWorld.Select();
    }

    public void StartTesting()
    {
        WorldSaveGameManager.Instance.LoadWorldScene(WorldSaveGameManager.Instance.GetTestWorldSceneIndex());
        WorldSaveGameManager.Instance.LoadPlayerPerfab();
    }
}
