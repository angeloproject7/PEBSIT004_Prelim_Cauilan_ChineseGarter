using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;
    public int lives = 3;

    public float level1Height = 0.5f;
    public float level2Height = 0.75f;
    public float level3Height = 1.0f;

    public Transform player;
    public Transform garter;
    public Transform ground;

    public TMP_Text levelText;
    public TMP_Text livesText;

    // Actual heart UI objects
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    private Vector3 playerStartPosition;
    private bool gameFinished = false;

    void Awake()
    {
        Instance = this;

        FindUIReferences();
        FindHeartReferences();
    }

    void Start()
    {
        Time.timeScale = 1f;

        playerStartPosition = player.position;

        SetLevelHeight();
        RespawnPlayer();
        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);
    }

    void FindUIReferences()
    {
        TMP_Text[] texts = FindObjectsByType<TMP_Text>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (TMP_Text text in texts)
        {
            if (text.gameObject.name == "LevelText")
                levelText = text;

            if (text.gameObject.name == "LivesText")
                livesText = text;
        }

        GameObject[] objects = FindObjectsByType<GameObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (GameObject obj in objects)
        {
            if (obj.name == "GameOverPanel")
                gameOverPanel = obj;

            if (obj.name == "WinPanel")
                winPanel = obj;
        }
    }

    void FindHeartReferences()
    {
        GameObject[] objects = FindObjectsByType<GameObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (GameObject obj in objects)
        {
            if (obj.name == "Heart1")
                heart1 = obj;

            if (obj.name == "Heart2")
                heart2 = obj;

            if (obj.name == "Heart3")
                heart3 = obj;
        }
    }

    public void LevelCleared()
    {
        if (gameFinished)
            return;

        if (currentLevel < 3)
        {
            currentLevel++;

            SetLevelHeight();
            ResetDetector();
            RespawnPlayer();
            UpdateUI();

            Debug.Log("Level " + currentLevel + " started!");
        }
        else
        {
            gameFinished = true;

            UpdateUI();

            if (winPanel != null)
                winPanel.SetActive(true);

            Debug.Log("YOU WIN! All 3 levels completed!");
        }
    }

    public void LoseLife()
    {
        if (gameFinished)
            return;

        lives--;

        UpdateUI();

        Debug.Log("Life lost! Lives remaining: " + lives);

        if (lives <= 0)
        {
            gameFinished = true;

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            Debug.Log("GAME OVER!");
        }
        else
        {
            ResetDetector();
            RespawnPlayer();
        }
    }

    void RespawnPlayer()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Keep the Player upright after respawn
        player.rotation = Quaternion.identity;

        float respawnY = playerStartPosition.y;

        if (ground != null)
        {
            Collider2D groundCollider = ground.GetComponent<Collider2D>();
            Collider2D playerCollider = player.GetComponent<Collider2D>();

            if (groundCollider != null && playerCollider != null)
            {
                respawnY =
                    groundCollider.bounds.max.y +
                    playerCollider.bounds.extents.y +
                    0.02f;
            }
        }

        if (garter != null)
        {
            float belowGarter = garter.position.y - 0.5f;

            if (respawnY >= belowGarter)
            {
                respawnY = belowGarter;
            }
        }

        player.position = new Vector3(
            playerStartPosition.x,
            respawnY,
            playerStartPosition.z
        );
    }

    void SetLevelHeight()
    {
        if (currentLevel == 1)
        {
            garter.position = new Vector3(
                garter.position.x,
                level1Height,
                garter.position.z
            );
        }
        else if (currentLevel == 2)
        {
            garter.position = new Vector3(
                garter.position.x,
                level2Height,
                garter.position.z
            );
        }
        else if (currentLevel == 3)
        {
            garter.position = new Vector3(
                garter.position.x,
                level3Height,
                garter.position.z
            );
        }
    }

    void ResetDetector()
    {
        GarterDetector detector =
            garter.GetComponent<GarterDetector>();

        if (detector != null)
        {
            detector.ResetDetector();
        }
    }

    void UpdateUI()
    {
        if (levelText != null)
        {
            levelText.text = "LEVEL " + currentLevel;
        }

        if (livesText != null)
        {
            livesText.text = "LIVES:";
        }

        // Show/hide actual heart images
        if (heart1 != null)
            heart1.SetActive(lives >= 1);

        if (heart2 != null)
            heart2.SetActive(lives >= 2);

        if (heart3 != null)
            heart3.SetActive(lives >= 3);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}