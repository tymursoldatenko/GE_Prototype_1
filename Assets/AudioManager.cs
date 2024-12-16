using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // �������� ��� ����������� ���������
    public Slider masterSlider;
    public Slider carSlider;
    public Slider copsSlider;

    // ������ ��� �������� ���� ������ (AudioSource) ����� � �����
    public List<AudioSource> carAudioSources = new List<AudioSource>(); // ����� ���� �����
    public List<AudioSource> copsAudioSources = new List<AudioSource>(); // ����� ���� �����
    public AudioSource backgroundAudioSource;  // ������� ������ ��� ����� ���� ����

    void Start()
    {
        // ��������� ��������� �������� ���������
        masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterSlider.value); });
        carSlider.onValueChanged.AddListener(delegate { SetCarVolume(carSlider.value); });
        copsSlider.onValueChanged.AddListener(delegate { SetCopsVolume(copsSlider.value); });

        // ��������� ��������� ���������
        SetMasterVolume(masterSlider.value);
        SetCarVolume(carSlider.value);
        SetCopsVolume(copsSlider.value);
    }

    // ����� ��� ��������� ��������� ��� �������� �����
    public void SetMasterVolume(float volume)
    {
        backgroundAudioSource.volume = volume;
    }

    // ����� ��� ��������� ��������� ������ ���� �����
    public void SetCarVolume(float volume)
    {
        foreach (var audioSource in carAudioSources)
        {
            audioSource.volume = volume;
        }
    }

    // ����� ��� ��������� ��������� ������ ���� �����
    public void SetCopsVolume(float volume)
    {
        foreach (var audioSource in copsAudioSources)
        {
            audioSource.volume = volume;
        }
    }
}
