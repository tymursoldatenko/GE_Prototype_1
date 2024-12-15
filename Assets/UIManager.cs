using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;  // ������� ����
    public GameObject gameCamera;  
    public GameObject mainCamera;  
    public GameObject gameCanvas;     // ������� ����
    public GameObject pauseCanvas;    // ����� �����
    public GameObject gameOverCanvas; // ����� ��������� ����
    public Text countdownText;        // ����� ��� ��������� �������

    private bool isPaused = false;    // ���� �����

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {
        // ��������� ������� ������� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // �������� ������� ����
    public void ShowMainMenu()
    {
        Time.timeScale = 0;
        mainMenuCanvas.SetActive(true);
        mainCamera.SetActive(true);
        gameCamera.SetActive(false);
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // ������ ����
    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        gameCamera.SetActive(true);
        mainCamera.SetActive(false);
        StartCoroutine(CountdownToStart());
    }

    // �������� ��� ��������� ������� ����� ������� ����
    private IEnumerator CountdownToStart()
    {
        gameCanvas.SetActive(true);  // �������� ������� �����

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // ���������� ������� �����
            yield return new WaitForSeconds(1); // ����� 1 �������
        }

        countdownText.text = ""; // �������� ����� ����� �������
        Time.timeScale = 1;
        // ����� ����� �������� ��� ��� ������� ����
    }

    // ����� ����
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // ������������� �����
        pauseCanvas.SetActive(true); // �������� ����� �����
    }

    // ���������� ����
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // ������������ �����
        pauseCanvas.SetActive(false); // ������ ����� �����
    }

    // ������������� ����
    public void RestartGame()
    {
        Time.timeScale = 1; // ������������ ����� �� ������ �����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������� ������� �����
    }

    // �������� ����� ���������� ����
    public void ShowGameOver()
    {
        gameCanvas.SetActive(false); // ������ ������� �����
        gameOverCanvas.SetActive(true); // �������� ����� ��������� ����
    }
}
