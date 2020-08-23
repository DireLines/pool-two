using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableZoneManager : MonoBehaviour
{
    public static TableZoneManager instance;

    public TableZone player1Zone;
    public TableZone neutralZone;
    public TableZone player2Zone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
        }
    }
}
