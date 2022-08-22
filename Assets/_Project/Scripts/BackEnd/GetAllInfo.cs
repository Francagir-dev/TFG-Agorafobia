using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class GetAllInfo : MonoBehaviour
{
    public Action<string> _createSessionCallback;
    public CheckSessionID checkID;
    public JSONArray sessionINFO;
    public TextMeshProUGUI patientName, patientPhobia;
    public GameObject uiInfo, btnYes,btnNo;
    
    
    public void CheckID()
    {
    _createSessionCallback = (jsonArrayString) =>
        {
            StartCoroutine(CheckSessionCode(jsonArrayString));
        };
    }

    IEnumerator CheckSessionCode(string jsonArrayString)
    {
     
        sessionINFO = JSON.Parse(jsonArrayString) as JSONArray;
        if(sessionINFO == null) yield return null;

        for (int i = 0; i < sessionINFO.Count; i++)
        {
           
            //string id = sessionINFO[i].AsObject["SessionCode"];
            JSONObject sessionInfo = sessionINFO[i] as JSONObject;

            //Fill info and activate items
            patientName.text = sessionInfo["PatientName"];
            patientPhobia.text = sessionInfo["PatientPhobia"];
           
            //Active items 
            uiInfo.SetActive(true);
            btnYes.SetActive(true);
            btnNo.SetActive(true);
            
            //save in persistent Manager
            PersistentManager.infoManager._session.patientName = sessionInfo["PatientName"];
            PersistentManager.infoManager._session.phobiaName = sessionInfo["PatientPhobia"];
            PersistentManager.infoManager._session.sessionID = (int)sessionInfo["SessionID"];
            PersistentManager.infoManager._session.phobiaLVL = (int)sessionInfo["PhobiaLevel"];
            
        }
    }

   
}
