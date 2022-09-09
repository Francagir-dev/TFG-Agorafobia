using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager infoManager;
     bool forceVR = false;
    private void Awake()
    {
       
        if (infoManager == null)
        {
            DontDestroyOnLoad(gameObject);
            infoManager = this;
        }
        else if (infoManager != this)
        {
            Destroy(gameObject);
        }
    }

    public SessionData _session = new SessionData();
}

[Serializable]
public class SessionData
{
    public int sessionID;
    public int sessionCode;
    public string patientName;
    public string phobiaName;
    public int phobiaLVL;
}