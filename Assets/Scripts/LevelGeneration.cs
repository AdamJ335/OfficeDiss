using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    Vector2 worldSize = new Vector2(7,7);

    Room[,] rooms;
    enum roomNames
    {
        Top,
        Bottom,
        Left,
        Right,
        TopBottom,
        LeftRight,
        TTop,
        TBottom,
        TRight,
        TLeft
    }
    static Random _R = new Random();
    static String getRoomType<String>(){
        var v = roomNames.GetValues (typeof (String));
        return (String) v.GetValue (_R.Next(v.Length));
    }

    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY, numberOfRooms = 10;
    // Start is called before the first frame update
    void Start()
    {
        if(numberOfRooms >= (worldSize.x*2)*(worldSize.y*2)){
            numberOfRooms = Mathf.RoundToInt((worldSize.x*2)*(worldSize.y*2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
        
    }
    void CreateRooms(){
        //setup
        rooms = new Room[gridSizeX*2,gridSizeY*2];
        rooms[gridSizeX,gridSizeY] = new Room(Vector2.zero, "Start");
        takenPositions.Insert(0,Vector2.zero);
        Vector2 checkPos = Vector2.zero;
        //magic numbers
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        //add rooms
        for(int i=0; i<numberOfRooms -1; i++){
            float randomPerc = ((float) i) / (((float)numberOfRooms -1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //grab new position
            checkPos = NewPosition();
            //test new position
            if(NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare){
                int iterations = 0;
                do{
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }while(NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if(iterations >= 50){
                    print("error: couldnt create with fewer neighbours than: " + NumberOfNeighbors(checkPos, takenPositions));
                }
            }
            //finalise position
            rooms[(int) checkPos.x + gridSizeX, (int) checkPos.y + gridSizeY] = new Room(checkPos, getRoomType());
            takenPositions.Insert(0,checkPos);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
