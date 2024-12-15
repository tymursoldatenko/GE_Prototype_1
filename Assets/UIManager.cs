using System.Collections; // Это важно для IEnumerator
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameCamera;
    public GameObject mainCamera;
    public GameObject mainMenu;      // Главный экран
    public GameObject countdownUI;  // Экран отсчёта
    public GameObject pauseMenu;    // Экран паузы
    public GameObject gameOverMenu; // Экран окончания игры
    public Text countdownText;      // Текст для отсчёта

    private bool isGamePaused = false; // Флаг паузы
    public static bool isGameStarted = false; // Флаг начала игры
    public static UIManager Instance;

    void Awake()
    {
        // Проверяем, если экземпляр уже существует, то удаляем этот компонент
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this; // Устанавливаем текущий экземпляр как Instance
            DontDestroyOnLoad(gameObject); // Убедимся, что UIManager не уничтожится при смене сцен
        }
    }


    void Start()
    {
        // Устанавливаем начальные состояния
        mainMenu.SetActive(true);
        mainCamera.SetActive(true);
        gameCamera.SetActive(false);
        countdownUI.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        isGameStarted = false; // Игра не началась
    }
    void Update()
    {
        // Проверяем нажатие клавиши Esc
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
        mainMenu.SetActive(false); // Скрываем главное меню
        mainCamera.SetActive(false);
        gameCamera.SetActive(true);
        StartCoroutine(StartCountdown()); // Запускаем отсчёт
    }

    private IEnumerator StartCountdown()
    {
        countdownUI.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Показать текущее число
            yield return new WaitForSeconds(1f); // Ждать 1 секунду
        }

        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        //countdownUI.SetActive(false); // Скрываем экран отсчёта
        isGameStarted = true; // Игра началась
    }

    public void OnPauseButtonClicked()
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время
    }

    public void OnResumeButtonClicked()
    {
        isGamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Возобновляем время
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f; // Восстанавливаем время
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        if (countdownText != null && !countdownText.gameObject.activeSelf)
        {
            countdownText.gameObject.SetActive(true); // Включаем текст снова
        }// Перезапускаем текущую сцену
    }

    public void OnGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время
    }
}
