using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AgorafobiaManager : MonoBehaviour
{

    private float timeAgoraScene;


    private void OnApplicationQuit()
    {
        timeAgoraScene = Time.timeSinceLevelLoad;
        StartCoroutine(UpdateDatabaseInfo());
        StartCoroutine(InsertTask());
    }

    private void OnDisable()
    {
        timeAgoraScene = Time.timeSinceLevelLoad;
        StartCoroutine(UpdateDatabaseInfo());
        StartCoroutine(InsertTask());
    }

    IEnumerator UpdateDatabaseInfo()
    {
        WWWForm form = new WWWForm();

        form.AddField("SessionDuration", timeAgoraScene.ToString());
        form.AddField("SessionCode", PersistentManager.infoManager._session.sessionCode);

        using (UnityWebRequest www = UnityWebRequest.Post(Constants.SERVER_IP + "/UpdateSession.php", form))
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
                        Debug.Log("Success");
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

        yield return null;
    }
    
    /// <summary>
    /// Query to "Post" the task done in the database
    /// </summary>
    /// <returns></returns>
    public IEnumerator InsertTask()
    {
        WWWForm form = new WWWForm();
        
        //Add variables to form (each value of INSERT INTO) 
        form.AddField("sessionID", PersistentManager.infoManager._session.sessionID);
        form.AddField("description","Task2: Walk Around the garden");
        form.AddField("duration", Time.timeSinceLevelLoad.ToString());
        form.AddField("anxietyLvlTask", PersistentManager.infoManager._session.phobiaLVL);
        form.AddField("patientFeels", "Feel comfortable and easy, next time more time, or longer distance");

        //IPs are saved in Constants Script to scalate easily
        using (UnityWebRequest www = UnityWebRequest.Post(Constants.SERVER_IP + "/PostTask.php", form))
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
                }
                else
                {
                    Debug.Log("Error");
                }
                
            }
        }
    }
    
    
}