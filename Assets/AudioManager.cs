using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // Слайдеры для регулировки громкости
    public Slider masterSlider;
    public Slider carSlider;
    public Slider copsSlider;

    // Списки для хранения всех звуков (AudioSource) машин и копов
    public List<AudioSource> carAudioSources = new List<AudioSource>(); // Звуки всех машин
    public List<AudioSource> copsAudioSources = new List<AudioSource>(); // Звуки всех копов
    public AudioSource backgroundAudioSource;  // Фоновая музыка или общий звук игры

    void Start()
    {
        // Настройка начальных значений слайдеров
        masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterSlider.value); });
        carSlider.onValueChanged.AddListener(delegate { SetCarVolume(carSlider.value); });
        copsSlider.onValueChanged.AddListener(delegate { SetCopsVolume(copsSlider.value); });

        // Настройка начальной громкости
        SetMasterVolume(masterSlider.value);
        SetCarVolume(carSlider.value);
        SetCopsVolume(copsSlider.value);
    }

    // Метод для установки громкости для фонового звука
    public void SetMasterVolume(float volume)
    {
        backgroundAudioSource.volume = volume;
    }

    // Метод для установки громкости звуков всех машин
    public void SetCarVolume(float volume)
    {
        foreach (var audioSource in carAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    // Метод для установки громкости звуков всех копов
    public void SetCopsVolume(float volume)
    {
        foreach (var audioSource in copsAudioSources)
        {
            audioSource.volume = volume;
        }
    }
}
