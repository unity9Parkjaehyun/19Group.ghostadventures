using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Managers")]
    public GameObject playerPrefab;
    private GameObject currentPlayer;

    private void Start()
    {
        SpawnPlayer();

    }

    public void SpawnPlayer()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(currentPlayer);
        }

        // Player 생성 후 EnemyAI에 할당
        AssignPlayerToEnemies();
    }

    public GameObject Player => currentPlayer;

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"씬 로드됨: {scene.name}");
        // 씬이 바뀔 때마다 EnemyAI에 Player를 다시 할당
        AssignPlayerToEnemies();
    }

    private void EnsureManagerExists<T>(GameObject prefab) where T : MonoBehaviour
    {
        if (Singleton<T>.Instance == null)
        {
            Instantiate(prefab);
            Debug.Log($"[{typeof(T).Name}] 자동 생성됨");
        }
    }

    // 중복되는 할당 코드를 메서드로 분리
    private void AssignPlayerToEnemies()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (var enemy in enemies)
        {
            if (currentPlayer != null)
                enemy.Player = currentPlayer.transform;
        }
    }
}
