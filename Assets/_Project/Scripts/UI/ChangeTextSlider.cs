using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextSlider : MonoBehaviour
{
    public int value;
   [SerializeField] private TextMeshProUGUI textSlider;
   [SerializeField] private Slider sliderOptions;
   
   public void ChangeText()
   {
      textSlider.text = sliderOptions.value.ToString();
   }




}
