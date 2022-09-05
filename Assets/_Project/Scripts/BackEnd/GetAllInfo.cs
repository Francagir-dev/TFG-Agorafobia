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
    
    /// <summary>
    /// Function to Make the callback
    /// </summary>
    public void CheckID()
    {
    _createSessionCallback = (jsonArrayString) =>
        {
            StartCoroutine(CheckSessionCode(jsonArrayString));
        };
    }

    /// <summary>
    /// Read Json from server, and assign to items
    /// </summary>
    /// <param name="jsonArrayString"></param>
    /// <returns></returns>
    IEnumerator CheckSessionCode(string jsonArrayString)
    {
     
        sessionINFO = JSON.Parse(jsonArrayString) as JSONArray;
        if(sessionINFO == null) yield return null;

        for (int i = 0; i < sessionINFO.Count; i++)
        {
            //Create a reference of Item to receive data. 
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
