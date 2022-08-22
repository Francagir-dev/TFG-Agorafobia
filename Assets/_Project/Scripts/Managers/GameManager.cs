using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float sessionDuration = 0f;

   private void OnDisable()
    {
        sessionDuration = Time.timeSinceLevelLoad;
        Debug.Log(sessionDuration);
    }
}