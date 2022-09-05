using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeen : MonoBehaviour
{
  
    public int sessionID;
    public string itemName;
    public float duration;
    public float distanceToPlayer;
    public int manytimesSeen;


    public ItemSeen()
    {
        
    }
    public ItemSeen(int sessionID, string itemName, float duration, float distanceToPlayer, int manytimesSeen)
    {
        this.sessionID = sessionID;
        this.itemName = itemName;
        this.duration = duration;
        this.distanceToPlayer = distanceToPlayer;
        this.manytimesSeen = manytimesSeen;
    }

}
