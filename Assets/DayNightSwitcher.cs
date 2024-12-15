using UnityEngine;
using UnityEngine.UI;

public class DayNightSwitcher : MonoBehaviour
{
    public Toggle dayNightToggle; // Ссылка на Toggle
    public Light mainLight;       // Основной дневной свет (Directional Light)

    [SerializeField] private string streetLightTag = "Street Light"; // Тег для фонарей
    private Light[] streetLights; // Массив всех фонарей

    void Start()
    {
        // Находим все объекты с тегом "StreetLight" и извлекаем их компоненты Light
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag(streetLightTag);
        streetLights = new Light[lightObjects.Length];

        for (int i = 0; i < lightObjects.Length; i++)
        {
            streetLights[i] = lightObjects[i].GetComponent<Light>();
        }

        // Настраиваем начальное состояние Toggle
        dayNightToggle.onValueChanged.AddListener(OnToggleChanged);
        OnToggleChanged(dayNightToggle.isOn); // Обновляем сцену под текущий статус
    }

    // Метод вызывается при изменении Toggle
    public void OnToggleChanged(bool isDay)
    {
        if (isDay)
        {
            SetDayMode();
        }
        else
        {
            SetNightMode();
        }
    }

    // Включаем дневной режим
    void SetDayMode()
    {
        mainLight.enabled = true; // Включаем основной свет
        SetStreetLights(false);  // Выключаем фонари
    }

    // Включаем ночной режим
    void SetNightMode()
    {
        mainLight.enabled = false; // Выключаем основной свет
        SetStreetLights(true);    // Включаем фонари
    }

    // Переключение состояния фонарей
    void SetStreetLights(bool state)
    {
        foreach (Light light in streetLights)
        {
            if (light != null)
            {
                light.enabled = state;
            }
        }
    }
}
