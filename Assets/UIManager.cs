using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuCanvas;  // Главное меню
    public GameObject gameCamera;  
    public GameObject mainCamera;  
    public GameObject gameCanvas;     // Игровое поле
    public GameObject pauseCanvas;    // Экран паузы
    public GameObject gameOverCanvas; // Экран окончания игры
    public Text countdownText;        // Текст для обратного отсчёта

    private bool isPaused = false;    // Флаг паузы

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {
        // Проверяем нажатие клавиши Esc
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

    // Показать главное меню
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

    // Начать игру
    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        gameCamera.SetActive(true);
        mainCamera.SetActive(false);
        StartCoroutine(CountdownToStart());
    }

    // Корутина для обратного отсчёта перед стартом игры
    private IEnumerator CountdownToStart()
    {
        gameCanvas.SetActive(true);  // Показать игровой экран

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Отобразить текущее число
            yield return new WaitForSeconds(1); // Ждать 1 секунду
        }

        countdownText.text = ""; // Очистить текст после отсчёта
        Time.timeScale = 1;
        // Здесь можно добавить код для запуска игры
    }

    // Пауза игры
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Останавливаем время
        pauseCanvas.SetActive(true); // Показать экран паузы
    }

    // Продолжить игру
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Возобновляем время
        pauseCanvas.SetActive(false); // Скрыть экран паузы
    }

    // Перезапустить игру
    public void RestartGame()
    {
        Time.timeScale = 1; // Возобновляем время на случай паузы
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагружаем текущую сцену
    }

    // Показать экран завершения игры
    public void ShowGameOver()
    {
        gameCanvas.SetActive(false); // Скрыть игровой экран
        gameOverCanvas.SetActive(true); // Показать экран окончания игры
    }
}
