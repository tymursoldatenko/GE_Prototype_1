using UnityEngine;

public class InactivityTracker : MonoBehaviour
{
    public float inactivityThreshold = 4f; // Время без движения (в секундах) для гейм овер
    private float lastMovementTime = 0f;
    private bool isGameOver = false;

    private Rigidbody rb; // Для отслеживания движения (если у машины есть Rigidbody)

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем компонент Rigidbody (если он есть)
    }

    void Update()
    {
        // Если игра не началась или уже наступил гейм овер, ничего не делаем
        if (!UIManager.isGameStarted || isGameOver)
        {
            return;
        }

        // Если есть движение, сбрасываем время простоя
        if (rb.velocity.magnitude > 3f) // Проверяем скорость
        {
            lastMovementTime = Time.time; // Обновляем время последнего движения
        }

        // Если прошло время без движения (4 секунды)
        if (Time.time - lastMovementTime > inactivityThreshold)
        {
            TriggerGameOver(); // Вызываем гейм овер
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true; // Устанавливаем флаг гейм овер
        UIManager.Instance.OnGameOver(); // Вызываем метод гейм овер в UIManager
    }
}
