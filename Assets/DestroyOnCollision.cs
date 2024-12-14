using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject destructionEffect; // Префаб системы частиц
    public float destroyDelay = 1f; // Время задержки для удаления системы частиц

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли игрок с разрушимым объектом
        if (collision.gameObject.CompareTag("Destroyable"))
        {
            // Создаем частицу в месте столкновения
            GameObject effect = Instantiate(destructionEffect, collision.transform.position, Quaternion.identity);

            // Уничтожаем объект через destroyDelay секунд
            Destroy(collision.gameObject);

            // Уничтожаем систему частиц через destroyDelay секунд
            Destroy(effect, destroyDelay);
        }
    }
}
