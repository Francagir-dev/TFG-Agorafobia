using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager infoManager;
   
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