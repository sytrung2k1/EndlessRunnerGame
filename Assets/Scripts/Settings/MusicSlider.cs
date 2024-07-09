using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = GameManager.gameManager.music;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
