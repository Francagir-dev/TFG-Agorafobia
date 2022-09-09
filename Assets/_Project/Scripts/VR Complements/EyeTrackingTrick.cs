using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using Vector3 = UnityEngine.Vector3;

public class EyeTrackingTrick : MonoBehaviour
{
    //Raycast variables for colliding
    [Header("RAYCAST VARIABLES ")] [SerializeField]
    private LayerMask layerToCollide;

    [Range(0, 10f)] public float maxDistanceToItem;

    private ItemSeen _itemSeen;
    private bool isItemHitted;
    RaycastHit hit;
    GameObject itemHited;
    GameObject prevItem = null;

    //Variables de información
    private string name;
    int timesHitted = 0;
    float timeCount = 0f;
    private float distance;
    private List<ItemSeen> listItems = new List<ItemSeen>();

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                maxDistanceToItem, layerToCollide))
        {
            itemHited = hit.transform.gameObject; //Assign game object as item hit
            if (itemHited == null) return; // If there is not a item hited will start again
            if (prevItem != itemHited)
            {
                isItemHitted = true; //Change boolean to true 
            }
            else
            {
                isItemHitted = false; //Change boolean to false 
                return;
            }

            if (isItemHitted)
            {
                if (listItems.Count > 0)
                {
                    for (int i = 0; i < listItems.Count; i++)
                    {
                        if (listItems[i].name.Equals(hit.collider.gameObject.name))
                        {
                            _itemSeen = listItems[i];
                        }
                    }
                }

                timeCount += Time.deltaTime;
                timesHitted++;
                name = hit.collider.gameObject.name;
                distance = Vector3.Distance(transform.position, hit.collider.transform.position);

                Debug.Log(name);
            }
            else
            {
                prevItem = itemHited;
                if (_itemSeen == null)
                    _itemSeen = new ItemSeen(PersistentManager.infoManager._session.sessionID, name, timeCount,
                        distance, timesHitted);

                listItems.Add(_itemSeen);

                //reset items
                name = "";
                timeCount = 0.0f;
                distance = 0f;
                timesHitted = 0;
            }
        }
        else
        {
            Debug.Log("There is no item with this layer");
        }
    }

    private void OnDisable()
    {
        if (listItems.Count <= 0) return;
        for (int i = 0; i < listItems.Count; i++)
        {
            StartCoroutine(InsertItem(listItems[i]));
        }
    }

    public IEnumerator InsertItem(ItemSeen item)
    {
        WWWForm form = new WWWForm();

        //Add code to form
        form.AddField("sessionID", item.sessionID);
        form.AddField("itemName", item.name);
        form.AddField("duration", item.duration.ToString());
        form.AddField("distance", item.distanceToPlayer.ToString());
        form.AddField("manyTimes", item.manytimesSeen);

        //IPs are saved in Constants Script to scalate easily
        using (UnityWebRequest www = UnityWebRequest.Post(Constants.SERVER_IP + "/PostItem.php", form))
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