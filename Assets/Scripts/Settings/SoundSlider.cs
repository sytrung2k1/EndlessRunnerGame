using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = GameManager.gameManager.sound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
