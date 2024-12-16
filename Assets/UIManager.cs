using System.Collections; // Это важно для IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement;
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
        // Проверяем, существуют ли объекты, к которым мы пытаемся обратиться
        if (mainMenu != null) mainMenu.SetActive(false); // Скрываем главное меню
        if (mainCamera != null) mainCamera.SetActive(false);
        if (gameCamera != null) gameCamera.SetActive(true);

        // Запускаем отсчет
        StartCoroutine(StartCountdown());
    }


    private IEnumerator StartCountdown()
    {
        if (countdownUI != null) countdownUI.SetActive(true); // Показываем экран отсчета

        for (int i = 3; i > 0; i--)
        {
            if (countdownText != null) countdownText.text = i.ToString(); // Показать текущее число
            yield return new WaitForSeconds(1f); // Ждем 1 секунду
        }

        if (countdownText != null) countdownText.text = "Go!"; // Показываем "Go!"
        yield return new WaitForSeconds(1f);
        if (countdownText != null) countdownText.text = ""; // Очищаем текст

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

        // Прячем все UI элементы перед перезагрузкой сцены
        if (mainMenu != null) mainMenu.SetActive(false);
        if (mainCamera != null) mainCamera.SetActive(false);
        if (gameCamera != null) gameCamera.SetActive(false);
        if (countdownUI != null) countdownUI.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverMenu != null) gameOverMenu.SetActive(false);

        // Загружаем текущую сцену заново
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // После перезагрузки активируем необходимые UI элементы
        StartCoroutine(WaitForSceneReload());
    }

    // Это корутина для того, чтобы подождать, пока сцена полностью загрузится
    private IEnumerator WaitForSceneReload()
    {
        // Даем время для загрузки сцены
        yield return new WaitForEndOfFrame();

        // Проверяем на null и активируем нужные UI элементы
        if (mainMenu != null) mainMenu.SetActive(true);
        if (mainCamera != null) mainCamera.SetActive(true);
        if (gameCamera != null) gameCamera.SetActive(false);
        if (countdownUI != null) countdownUI.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverMenu != null) gameOverMenu.SetActive(false);

        isGameStarted = false; // Игра не началась
    }

    public void OnGameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // Останавливаем время
    }
}
