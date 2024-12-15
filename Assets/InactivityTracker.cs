using UnityEngine;

public class InactivityTracker : MonoBehaviour
{
    public float inactivityThreshold = 4f; // ����� ��� �������� (� ��������) ��� ���� ����
    private float lastMovementTime = 0f;
    private bool isGameOver = false;

    private Rigidbody rb; // ��� ������������ �������� (���� � ������ ���� Rigidbody)

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // �������� ��������� Rigidbody (���� �� ����)
    }

    void Update()
    {
        // ���� ���� �� �������� ��� ��� �������� ���� ����, ������ �� ������
        if (!UIManager.isGameStarted || isGameOver)
        {
            return;
        }

        // ���� ���� ��������, ���������� ����� �������
        if (rb.velocity.magnitude > 3f) // ��������� ��������
        {
            lastMovementTime = Time.time; // ��������� ����� ���������� ��������
        }

        // ���� ������ ����� ��� �������� (4 �������)
        if (Time.time - lastMovementTime > inactivityThreshold)
        {
            TriggerGameOver(); // �������� ���� ����
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true; // ������������� ���� ���� ����
        UIManager.Instance.OnGameOver(); // �������� ����� ���� ���� � UIManager
    }
}
