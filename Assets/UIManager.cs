using System.Collections; // ��� ����� ��� IEnumerator
using UnityEngine;
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
        mainMenu.SetActive(false); // �������� ������� ����
        mainCamera.SetActive(false);
        gameCamera.SetActive(true);
        StartCoroutine(StartCountdown()); // ��������� ������
    }

    private IEnumerator StartCountdown()
    {
        countdownUI.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // �������� ������� �����
            yield return new WaitForSeconds(1f); // ����� 1 �������
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        //countdownUI.SetActive(false); // �������� ����� �������
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        if (countdownText != null && !countdownText.gameObject.activeSelf)
        {
            countdownText.gameObject.SetActive(true); // �������� ����� �����
        }// ������������� ������� �����
    }

    public void OnGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // ������������� �����
    }
}
