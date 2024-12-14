using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject destructionEffect; // ������ ������� ������
    public float destroyDelay = 1f; // ����� �������� ��� �������� ������� ������

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ���������� �� ����� � ���������� ��������
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            // ������� ������� � ����� ������������
            GameObject effect = Instantiate(destructionEffect, collision.transform.position, Quaternion.identity);

            // ���������� ������ ����� destroyDelay ������
            Destroy(collision.gameObject);

            // ���������� ������� ������ ����� destroyDelay ������
            Destroy(effect, destroyDelay);
        }
    }
}
