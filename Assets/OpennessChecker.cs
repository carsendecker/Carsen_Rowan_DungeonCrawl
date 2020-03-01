using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OpennessChecker : MonoBehaviour
{
    public TextAsset dungeonMap;
    public TextMesh textDisplay;

    string map;

    Stack<Vector2Int> foundEmptyPositions;

    int width,height;

    List<Vector2Int> ortho =  new List<Vector2Int>() {Vector2Int.up,Vector2Int.right,Vector2Int.down,Vector2Int.left};
    List<Vector2Int> allDirections = new List<Vector2Int>(){Vector2Int.up,new Vector2Int(1,1),Vector2Int.right,new Vector2Int(1,-1),Vector2Int.down,new Vector2Int(-1,-1),Vector2Int.left,new Vector2Int(-1,1)};
    List<Vector2Int> foundPositions = new List<Vector2Int>();

    // Start is called before the first frame update
    void Start()
    {
        map = dungeonMap.text;

        string[] lineArray = map.Split('\n');
        width = lineArray[0].Length+1;
        height = lineArray.Length;
        
        int emptyCount = 0; // count how many empty tiles there are
        foreach(char c in map){
            if(c==' '){
                emptyCount++;
            }
        }
        //there is no empty space
        if(emptyCount == 0){
            return;
        }

        Vector2Int pos = Vector2Int.zero;
        while(map[pos.x+(width*pos.y)] != ' '){
            pos.x+=1;
            if(pos.x >= width){
                pos.x = 0;
                pos.y+=1;
            }
        }
        int foundEmpties = 1;
        foundPositions.Add(pos);
        FindOrthogonalNeighbors(pos);
        if(foundPositions.Count >= emptyCount){
            Debug.Log("A");
        }else{
            foundPositions.Clear();
            foundPositions.Add(pos);
            FindOrthoAndDiagonalNeighbors(pos);
            if(foundPositions.Count >= emptyCount){
                Debug.Log("B");
            }else{
                Debug.Log("C");
            }
            
        }




        textDisplay.text = map;
    }
        void FindOrthogonalNeighbors(Vector2Int pos){
            foreach(Vector2Int direction in ortho){

                Vector2Int newPos = pos+direction;
                if(newPos.x < 0 || newPos.x > width-2 || newPos.y < 0 || newPos.y > height-1){
                    continue;
                }
                //Debug.Log(newPos+","+map[newPos.x+(newPos.y*(width))]);
                if(foundPositions.Contains(newPos)){
                    continue;
                }
                if(map[newPos.x +(newPos.y*(width))] == ' '){
                    //this means its an empty neighbor
                    foundPositions.Add(newPos);
                    FindOrthogonalNeighbors(newPos);//this makes it so it won't check for a neighbor in the same direciton it just came from
                }
            }

        }
        void FindOrthoAndDiagonalNeighbors(Vector2Int pos){
            foreach(Vector2Int direction in allDirections){

                Vector2Int newPos = pos+direction;
                if(newPos.x < 0 || newPos.x > width-1 || newPos.y < 0 || newPos.y > height){
                    continue;
                }
                //Debug.Log(newPos+","+map[newPos.x+(newPos.y*(width))]);
                if(foundPositions.Contains(newPos)){
                    continue;
                }
                if(map[newPos.x +(newPos.y*(width))] == ' '){
                    //this means its an empty neighbor
                    foundPositions.Add(newPos);
                    FindOrthoAndDiagonalNeighbors(newPos);//this makes it so it won't check for a neighbor in the same direciton it just came from
                }
            }
        }


    // Update is called once per frame
    void Update()
    {
        
    }


}
