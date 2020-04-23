using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FusionAI : MonoBehaviour
{
    public GameObject grid;
    public List<Vector3> emptyGridPositions = new List<Vector3>();
    public bool fusionFinishAi = false;
    public float score;

    void Start(){
        score = 0;
    }

    public float GridParse(Vector2 direction, ref int [,] gridPositions){
        score = 0;
        fusionFinishAi = false;
        if(canMove(gridPositions, direction)){
            if(direction == Vector2.left){
                for(int y = 0; y < 4; y++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int count = 0;
                    int firstX = 0;
                    int firstY = 0;
                    for(int x = 0; x < 4; x++){
                        if(gridPositions[x,y] != 0){
                            if(count == 0){
                                firstCompare = gridPositions[x,y];
                                firstX = x;
                                firstY = y;
                                count++;
                            }else if(count == 1){
                                secondCompare = gridPositions[x,y];
                                count = 0;
                                if(!FusionCheck(firstCompare, secondCompare, x, y, firstX, firstY, direction, gridPositions)){
                                    x--;
                                }
                            }
                        }
                    }
                }
            }else if(direction == Vector2.right){
                for(int y = 0; y < 4; y++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int count = 0;
                    int firstX = 0;
                    int firstY = 0;
                    for(int x = 3; x >= 0; x--){
                        if(gridPositions[x,y] != 0){
                            if(count == 0){
                                firstCompare = gridPositions[x,y];
                                firstX = x;
                                firstY = y;
                                count++;
                            }else if(count == 1){
                                secondCompare = gridPositions[x,y];
                                count = 0;
                                if(!FusionCheck(firstCompare, secondCompare, x, y, firstX, firstY, direction, gridPositions)){
                                    x++;
                                }
                            }
                        }
                    }
                }
            }else if(direction == Vector2.down){
                for(int x = 0; x < 4; x++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int count = 0;
                    int firstX = 0;
                    int firstY = 0;
                    for(int y = 0; y < 4; y++){
                        if(gridPositions[x,y] != 0){
                            if(count == 0){
                                firstCompare = gridPositions[x,y];
                                firstX = x;
                                firstY = y;
                                count++;
                            }else if(count == 1){
                                secondCompare = gridPositions[x,y];
                                count = 0;
                                if(!FusionCheck(firstCompare, secondCompare, x, y, firstX, firstY, direction, gridPositions)){
                                    y--;
                                }
                            }
                        }
                    }
                }
            }else if(direction == Vector2.up){
                for(int x = 0; x < 4; x++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int count = 0;
                    int firstX = 0;
                    int firstY = 0;
                    for(int y = 3; y >= 0; y--){
                        if(gridPositions[x,y] != 0){
                            if(count == 0){
                                firstCompare = gridPositions[x,y];
                                firstX = x;
                                firstY = y;
                                count++;
                            }else if(count == 1){
                                secondCompare = gridPositions[x,y];
                                count = 0;
                                if(!FusionCheck(firstCompare, secondCompare, x, y, firstX, firstY , direction, gridPositions)){
                                    y++;
                                }
                            }
                        }
                    }
                }
            }
        }else{
            score = -999999;
        }
        fusionFinishAi = true;
        return score;
        
    }

    bool FusionCheck(int firstComapre, int secondCompare , int x, int y , int firstX, int firstY , Vector2 direction,  int [,] gridPositions){
        if(firstComapre == secondCompare){
            float tmp = firstComapre + secondCompare;
            gridPositions[firstX, firstY] = (int)tmp;
            gridPositions[x,y] = 0;
            score += Mathf.Log(tmp);
            return true;
        }else{
            return false;
        }
    }

    public bool canMove( int [,] gridPositions , Vector2 direction){
        int pieceCount = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(gridPositions[i , j] != 0){
                    pieceCount++;
                }
            }
        }
        if(pieceCount == 1){
            return true;
        }
        if(direction == Vector2.left){
            for(int y = 0; y < 4; y++){
                int firstCompare = 0;
                int secondCompare = 0;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int x = 0; x < 4; x++){
                    if(gridPositions[x,y] != 0){
                        if(count == 0){
                            if(x > 0 && firstCheck){
                                return true;
                            }
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            firstCheck = false;
                            count = 0;
                            if(firstCompare == secondCompare || x - firstX > 1){
                                return true;
                            }else{
                                x--;
                            }
                        }
                    }
                }
            }
            return false;
        }else if(direction == Vector2.right){
            for(int y = 0; y < 4; y++){
                int firstCompare = 0;
                int secondCompare = 0;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int x = 3; x >= 0; x--){
                    if(gridPositions[x,y] != 0){
                        if(count == 0){
                            if(x < 3 && firstCheck){
                                return true;
                            }
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            firstCheck = false;
                            if(firstCompare == secondCompare || firstX - x > 1){
                                return true;
                            }else{
                                x++;
                            }
                        }
                    }
                }
            }
            return false;
        }else if(direction == Vector2.down){
            for(int x = 0; x < 4; x++){
                int firstCompare = 0;
                int secondCompare = 0;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int y = 0; y < 4; y++){
                    if(gridPositions[x,y] != 0){
                        if(count == 0){
                            if(y > 0 && firstCheck){
                                return true;
                            }
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            firstCheck = false;
                            if(firstCompare == secondCompare || y - firstY > 1){
                                return true;
                            }else{
                                y--;
                            }
                        }
                    }
                }
            }
            return false;
        }else if(direction == Vector2.up){
            for(int x = 0; x < 4; x++){
                int firstCompare = 0;
                int secondCompare = 0;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int y = 3; y >= 0; y--){
                    if(gridPositions[x,y] != 0){
                        if(count == 0){
                            if(y < 3 && firstCheck){
                                return true;
                            }
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            firstCheck = false;
                            if(firstCompare == secondCompare  || firstY - y > 1){
                                return true;
                            }else{
                                y++;
                            }
                        }
                    }
                }
            }
            return false;
        }
        return false;
    }
}
