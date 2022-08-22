
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyboard : MonoBehaviour
{
    public TouchScreenKeyboard keyboard;
    public void InitializeKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
        keyboard.active = true;
    }
    
    public void CloseKeyboard()
    {
        keyboard.active = false;
    }
}
