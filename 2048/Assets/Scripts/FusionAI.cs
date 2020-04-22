using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FusionAI : MonoBehaviour
{
    public GameObject grid;
    public List<Vector3> emptyGridPositions = new List<Vector3>();
    public bool fusionFinishAi = false;
    public int score;

    void Start(){
        score = 0;
    }

    public int GridParse(Vector2 direction,ref GameObject [,] gridPositions){
        score = 0;
        fusionFinishAi = false;
        if(direction == Vector2.left){
            for(int y = 0; y < gridPositions.GetLength(1); y++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                int count = 0;
                int firstX = 0;
                int firstY = 0;
                for(int x = 0; x < gridPositions.GetLength(0); x++){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY, direction, ref gridPositions)){
                                x--;
                            }
                        }
                    }
                }
            }
        }else if(direction == Vector2.right){
            for(int y = 0; y < gridPositions.GetLength(1); y++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                int count = 0;
                int firstX = 0;
                int firstY = 0;
                for(int x = gridPositions.GetLength(0)-1; x >= 0; x--){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY, direction, ref gridPositions)){
                                x++;
                            }
                        }
                    }
                }
            }
        }else if(direction == Vector2.down){
            for(int x = 0; x < gridPositions.GetLength(0); x++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                int count = 0;
                int firstX = 0;
                int firstY = 0;
                for(int y = 0; y < gridPositions.GetLength(1); y++){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY, direction, ref gridPositions)){
                                y--;
                            }
                        }
                    }
                }
            }
        }else if(direction == Vector2.up){
            for(int x = 0; x < gridPositions.GetLength(0); x++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                int count = 0;
                int firstX = 0;
                int firstY = 0;
                for(int y = gridPositions.GetLength(1)-1; y >= 0; y--){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            firstX = x;
                            firstY = y;
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY , direction, ref gridPositions)){
                                y++;
                            }
                        }
                    }
                }
            }
        }
        fusionFinishAi = true;
        return score;
        
    }

    bool FusionCheck(Transform firstComapre, Transform secondCompare , int x, int y , int firstX, int firstY , Vector2 direction, ref GameObject [,] gridPositions){
        score = 0;
        int firstNum = int.Parse(firstComapre.Find("Canvas/Text").transform.GetComponent<Text>().text);
        int secondNum = int.Parse(secondCompare.Find("Canvas/Text").transform.GetComponent<Text>().text);
        if(firstNum == secondNum){
            int tmp = firstNum + secondNum;
            gridPositions[firstX, firstY].GetComponent<TileValue>().ChangeTileNum(tmp);
            gridPositions[x,y] = null;
            score += tmp;
            return true;
        }else{
            return false;
        }
    }

    public bool canMove(ref GameObject [,] gridPositions , Vector2 direction){
        int pieceCount = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(gridPositions[i , j] != null){
                    pieceCount++;
                }
            }
        }
        if(pieceCount == 1){
            return true;
        }
        if(direction == Vector2.left){
            for(int y = 0; y < gridPositions.GetLength(1); y++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int x = 0; x < gridPositions.GetLength(0); x++){
                    if(gridPositions[x,y] != null){
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
                            if(firstCompare.transform.Find("Canvas/Text").GetComponent<Text>().text == secondCompare.transform.Find("Canvas/Text").GetComponent<Text>().text || x - firstX > 1){
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
            for(int y = 0; y < gridPositions.GetLength(1); y++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int x = gridPositions.GetLength(0)-1; x >= 0; x--){
                    if(gridPositions[x,y] != null){
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
                            if(firstCompare.transform.Find("Canvas/Text").GetComponent<Text>().text == secondCompare.transform.Find("Canvas/Text").GetComponent<Text>().text || firstX - x > 1){
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
            for(int x = 0; x < gridPositions.GetLength(0); x++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int y = 0; y < gridPositions.GetLength(1); y++){
                    if(gridPositions[x,y] != null){
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
                            if(firstCompare.transform.Find("Canvas/Text").GetComponent<Text>().text == secondCompare.transform.Find("Canvas/Text").GetComponent<Text>().text || y - firstY > 1){
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
            for(int x = 0; x < gridPositions.GetLength(0); x++){
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                bool firstCheck = true;
                int firstX = -1;
                int firstY = -1;
                int count = 0;
                for(int y = gridPositions.GetLength(1)-1; y >= 0; y--){
                    if(gridPositions[x,y] != null){
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
                            if(firstCompare.transform.Find("Canvas/Text").GetComponent<Text>().text == secondCompare.transform.Find("Canvas/Text").GetComponent<Text>().text  || firstY - y > 1){
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
