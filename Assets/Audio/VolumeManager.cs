using Singletons;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeManager : SimpleSingleton<VolumeManager>
    {
        [SerializeField] private Slider     masterSlider;        
        [SerializeField] private Slider     musicSlider;
        [SerializeField] private Slider     sfxSlider;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private GameObject soundsSettingsPanel;

        public void SwitchVisibility()
        {
            soundsSettingsPanel.SetActive(!soundsSettingsPanel.activeSelf);
        }
        
        public void OnMasterVolumeChanged()
        {
            var value = masterSlider.value;
            audioMixer.SetFloat("MasterVolume", Mathf.Log(value) * 20f);
        }
        
        public void OnMusicVolumeChanged()
        {
            var value = musicSlider.value;
            audioMixer.SetFloat("MusicVolume", Mathf.Log(value) * 20f);
        }
        
        public void OnSFXsVolumeChanged()
        {
            var value = sfxSlider.value;
            audioMixer.SetFloat("SFXsVolume", Mathf.Log(value) * 20f);
        }
    }
}
