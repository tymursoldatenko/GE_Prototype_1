using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Transform playerCar; // ������ ������
    public float checkRadius = 10f; // ������ ��������
    public LayerMask copsLayer; // ���� ��� ����������� �����
    public Text scoreText; // ����� ��� ����������� �����

    private int score = 0; // ������� ����
    private float timer = 0f; // ������ ��� ������� �������

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f) // ������ ���, ����� �������� 1 �������
        {
            AddScore();
            timer = 0f;
        }

        UpdateUI();
    }

    void AddScore()
    {
        // �������� ������� ����������� ����� � �������
        Collider[] copsInRange = Physics.OverlapSphere(playerCar.position, checkRadius, copsLayer);

        if (copsInRange.Length > 0)
        {
            // ���� ���� ���� � �������, ��������� 4 ����
            score += 4;
        }
        else
        {
            // ���� ����� ���, ��������� 2 ����
            score += 2;
        }
    }

    void UpdateUI()
    {
        // ��������� ����� �� UI
        scoreText.text = "Score: " + score.ToString();
    }

    void OnDrawGizmosSelected()
    {
        // ������ ������ �������� � ��������� ��� ��������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCar.position, checkRadius);
    }
}
