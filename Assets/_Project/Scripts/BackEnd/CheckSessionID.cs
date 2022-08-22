using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class CheckSessionID : MonoBehaviour
{

  public string SessionCode;
  public GetAllInfo info;
  public void GetAllInfoSession()
  {
      info.CheckID();
      StartCoroutine(GetSessionInfo(PersistentManager.infoManager._session.sessionCode.ToString(), info._createSessionCallback));
  }

  public void GetSessionID(string SessionCode)
    {
        this.SessionCode = SessionCode;
        int.TryParse(SessionCode, out PersistentManager.infoManager._session.sessionCode);
      
    }

   /// <summary>
   /// Query to backend
   /// </summary>
   /// <param name="SessionCode">Code of session, introduced by patient</param>
   /// <param name="callback">Callback Function to receive JSON</param>
   /// <returns></returns>
    public IEnumerator GetSessionInfo(string SessionCode, Action<string> callback)
    {
        
        WWWForm form = new WWWForm();
        
        //Add code to form
        form.AddField("codeSession", SessionCode);

    //IPs are saved in Constants Script to scalate easily
        using (UnityWebRequest www = UnityWebRequest.Post(Constants.SERVER_IP + "/CheckSessionID.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (!www.downloadHandler.text.Contains("Error"))
                {
                    Debug.Log(www.downloadHandler.text);
                    try
                    {
                       
                        //CallBack function
                        callback(www.downloadHandler.text);
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                }
                else
                {
                    Debug.Log("Error");
                }
                
            }
        }
    }
    
}
