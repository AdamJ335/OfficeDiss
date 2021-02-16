using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public Vector2 gridPos;
    public String type;
    public bool, doorTop, doorBot, doorLeft, doorRight;

    public Room(Vector2 _gridPos, String _type){
        gridPos = _gridPos;
        type = _type;
    }
}
