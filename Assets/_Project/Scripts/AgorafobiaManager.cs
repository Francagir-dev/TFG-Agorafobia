using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AgorafobiaManager : MonoBehaviour
{

    private float timeAgoraScene;
    [Header("References for Singleton")]
    public CheckSessionID _CheckSessionID;
    public ShowTaskUI _ShowTaskUI;

  
    private void OnDisable()
    {
        timeAgoraScene = Time.timeSinceLevelLoad;
        StartCoroutine(UpdateDatabaseInfo());
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
    
    
    
}