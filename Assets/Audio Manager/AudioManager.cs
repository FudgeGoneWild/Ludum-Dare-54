using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Mixers")]
    [SerializeField] AudioMixer audioMixer;

    [Space(10)]

    [Header("Sliders")]
    [Space(5)]
    [SerializeField] Slider master_Slider;
    [SerializeField] Slider sFX_Slider;
    [SerializeField] Slider music_Slider;

    [Space(5)]
    [Header("Audio Source")]
    [SerializeField] AudioSource music_Source;
    [SerializeField] AudioSource Voice_Source;


    //instances
    private static AudioManager instance;

    //Audio volume save
    const string MasterVolumeSave = "SavedMasterVolume";
    const string SFXVolumeSave = "SavedSFXVolume";
    const string MusicVolumeSave = "SavedMusicVolume";


    //Audio mixers names
    const string MasterVolume = "MasterVolume";
    const string MusicVolume = "MusicVolume";
    const string SFXVolume = "SFXVolume";


    private void Awake()
    {


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        master_Slider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
        sFX_Slider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
        music_Slider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();

        master_Slider.onValueChanged.AddListener(SetMasterVolume);
        sFX_Slider.onValueChanged.AddListener(SetSFXVolume);
        music_Slider.onValueChanged.AddListener(SetMusicVolume);

    }

    private void Start()
    {
        OnLoad(); //loads slider values if ever changed
    }



    #region Set Volume
    void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(MasterVolume, volume);
        PlayerPrefs.SetFloat(MasterVolumeSave, master_Slider.value);
    }

    void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MusicVolume, volume);
        PlayerPrefs.SetFloat(SFXVolumeSave, sFX_Slider.value);
    }

    void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFXVolume, volume);
        PlayerPrefs.SetFloat(MusicVolumeSave, music_Slider.value);
    }

    #endregion

    #region Save and Load
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(MasterVolumeSave, master_Slider.value);
        PlayerPrefs.SetFloat(SFXVolumeSave, sFX_Slider.value);
        PlayerPrefs.SetFloat(MusicVolumeSave, music_Slider.value);
    }

    private void OnLoad()
    {
        audioMixer.SetFloat(MasterVolume, PlayerPrefs.GetFloat(MasterVolumeSave, 0));
        master_Slider.value = PlayerPrefs.GetFloat(MasterVolumeSave, 0);

        audioMixer.SetFloat(SFXVolume, PlayerPrefs.GetFloat(SFXVolumeSave, 0));
        sFX_Slider.value = PlayerPrefs.GetFloat(SFXVolumeSave, 0);

        audioMixer.SetFloat(MusicVolume, PlayerPrefs.GetFloat(MusicVolumeSave, 0));
        music_Slider.value = PlayerPrefs.GetFloat(MusicVolumeSave, 0);


    }
    #endregion

    public void PlaySFX(AudioClip clip)
    {
        Voice_Source.PlayOneShot(clip);
    }

    public void SetMusic(AudioClip musicClip)
    {
        music_Source.clip = musicClip;
        music_Source.Play();
    }

    IEnumerator MuteMusic(float delay)
    {
        music_Source.Pause();
        return new WaitForSecondsRealtime(delay);
        music_Source.Play();
    }
}
