using UnityEngine;
using UnityEngine.UI;

public class DayNightSwitcher : MonoBehaviour
{
    public Toggle dayNightToggle; // ������ �� Toggle
    public Light mainLight;       // �������� ������� ���� (Directional Light)

    [SerializeField] private string streetLightTag = "Street Light"; // ��� ��� �������
    private Light[] streetLights; // ������ ���� �������

    void Start()
    {
        // ������� ��� ������� � ����� "StreetLight" � ��������� �� ���������� Light
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag(streetLightTag);
        streetLights = new Light[lightObjects.Length];

        for (int i = 0; i < lightObjects.Length; i++)
        {
            streetLights[i] = lightObjects[i].GetComponent<Light>();
        }

        // ����������� ��������� ��������� Toggle
        dayNightToggle.onValueChanged.AddListener(OnToggleChanged);
        OnToggleChanged(dayNightToggle.isOn); // ��������� ����� ��� ������� ������
    }

    // ����� ���������� ��� ��������� Toggle
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

    // �������� ������� �����
    void SetDayMode()
    {
        mainLight.enabled = true; // �������� �������� ����
        SetStreetLights(false);  // ��������� ������
    }

    // �������� ������ �����
    void SetNightMode()
    {
        mainLight.enabled = false; // ��������� �������� ����
        SetStreetLights(true);    // �������� ������
    }

    // ������������ ��������� �������
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
