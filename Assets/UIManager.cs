using System.Collections; // ��� ����� ��� IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject mainCamera;
    public GameObject mainMenu;      // ������� �����
    public GameObject countdownUI;  // ����� �������
    public GameObject pauseMenu;    // ����� �����
    public GameObject gameOverMenu; // ����� ��������� ����
    public Text countdownText;      // ����� ��� �������

    private bool isGamePaused = false; // ���� �����
    public static bool isGameStarted = false; // ���� ������ ����
    public static UIManager Instance;

    void Awake()
    {

        // ���������, ���� ��������� ��� ����������, �� ������� ���� ���������
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; // ������������� ������� ��������� ��� Instance
            DontDestroyOnLoad(gameObject); // ��������, ��� UIManager �� ����������� ��� ����� ����
        }
    }


    void Start()
    {
        // ������������� ��������� ���������
        mainMenu.SetActive(true);
        mainCamera.SetActive(true);
        gameCamera.SetActive(false);
        countdownUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        isGameStarted = false; // ���� �� ��������
    }
    void Update()
    {
        // ��������� ������� ������� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                OnPauseButtonClicked();
            }
            else
            {
                OnResumeButtonClicked();
            }
        }
    }


    public void OnStartButtonClicked()
    {
        // ���������, ���������� �� �������, � ������� �� �������� ����������
        if (mainMenu != null) mainMenu.SetActive(false); // �������� ������� ����
        if (mainCamera != null) mainCamera.SetActive(false);
        if (gameCamera != null) gameCamera.SetActive(true);

        // ��������� ������
        StartCoroutine(StartCountdown());
    }


    private IEnumerator StartCountdown()
    {
        if (countdownUI != null) countdownUI.SetActive(true); // ���������� ����� �������

        for (int i = 3; i > 0; i--)
        {
            if (countdownText != null) countdownText.text = i.ToString(); // �������� ������� �����
            yield return new WaitForSeconds(1f); // ���� 1 �������
        }

        if (countdownText != null) countdownText.text = "Go!"; // ���������� "Go!"
        yield return new WaitForSeconds(1f);
        if (countdownText != null) countdownText.text = ""; // ������� �����

        isGameStarted = true; // ���� ��������
    }
    public void OnPauseButtonClicked()
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // ������������� �����
    }

    public void OnResumeButtonClicked()
    {
        isGamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // ������������ �����
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f; // ��������������� �����

        // ������ ��� UI �������� ����� ������������� �����
        if (mainMenu != null) mainMenu.SetActive(false);
        if (mainCamera != null) mainCamera.SetActive(false);
        if (gameCamera != null) gameCamera.SetActive(false);
        if (countdownUI != null) countdownUI.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverMenu != null) gameOverMenu.SetActive(false);

        // ��������� ������� ����� ������
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // ����� ������������ ���������� ����������� UI ��������
        StartCoroutine(WaitForSceneReload());
    }

    // ��� �������� ��� ����, ����� ���������, ���� ����� ��������� ����������
    private IEnumerator WaitForSceneReload()
    {
        // ���� ����� ��� �������� �����
        yield return new WaitForEndOfFrame();

        // ��������� �� null � ���������� ������ UI ��������
        if (mainMenu != null) mainMenu.SetActive(true);
        if (mainCamera != null) mainCamera.SetActive(true);
        if (gameCamera != null) gameCamera.SetActive(false);
        if (countdownUI != null) countdownUI.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverMenu != null) gameOverMenu.SetActive(false);

        isGameStarted = false; // ���� �� ��������
    }

    public void OnGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // ������������� �����
    }
}
