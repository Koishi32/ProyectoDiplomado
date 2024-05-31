using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISound : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    private void Start()
    {
       Vector2 a = AudioManager.Instance.GetSliderValuesStoredInSO();
        _musicSlider.value= a.x;
        _sfxSlider.value= a.y;
    }
    // Start is called before the first frame update
    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(_sfxSlider.value);
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
}
