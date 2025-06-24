using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Managers")]
    [SerializeField] private GameObject fakeEndingManager;
    [SerializeField] private GameObject uiManager;

    public GameObject playerPrefab;
    private GameObject currentPlayer;
    // 레이지 이니셜라이즈

    public void SpawnPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(currentPlayer);
        }
    }

    public GameObject Player => currentPlayer;

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"씬 로드됨: {scene.name}");
        EnsureManagerExists<FakeEndingManager>(fakeEndingManager);
        //EnsureManagerExists<UIManager>(uiManager);

        // 다른 매니저들도 같은 방식으로
        // 추후 스테이지 초기화, UI 초기화 등 여기에 추가
    }

    private void EnsureManagerExists<T>(GameObject prefab) where T : MonoBehaviour
    {
        if (Singleton<T>.Instance == null)
        {
            Instantiate(prefab);
            Debug.Log($"[{typeof(T).Name}] 자동 생성됨");
        }
    }
}
