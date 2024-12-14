using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Transform playerCar; // Машина игрока
    public float checkRadius = 10f; // Радиус проверки
    public LayerMask copsLayer; // Слой для полицейских машин
    public Text scoreText; // Текст для отображения очков

    private int score = 0; // Текущие очки
    private float timer = 0f; // Таймер для отсчета времени

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f) // Каждый раз, когда проходит 1 секунда
        {
            AddScore();
            timer = 0f;
        }

        UpdateUI();
    }

    void AddScore()
    {
        // Проверка наличия полицейских машин в радиусе
        Collider[] copsInRange = Physics.OverlapSphere(playerCar.position, checkRadius, copsLayer);

        if (copsInRange.Length > 0)
        {
            // Если копы есть в радиусе, добавляем 4 очка
            score += 4;
        }
        else
        {
            // Если копов нет, добавляем 2 очка
            score += 2;
        }
    }

    void UpdateUI()
    {
        // Обновляем текст на UI
        scoreText.text = "Score: " + score.ToString();
    }

    void OnDrawGizmosSelected()
    {
        // Рисуем радиус проверки в редакторе для удобства
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCar.position, checkRadius);
    }
}
