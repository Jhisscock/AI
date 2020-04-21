using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FusionAI : MonoBehaviour
{
    public GameObject grid;
    public GameObject [,] gridPositions = new GameObject[4,4];
    public int [,] gridNumbers = new int[4,4];
    public List<Vector3> emptyGridPositions = new List<Vector3>();
    public bool fusionFinish = false;
    public int score;

    void Start(){
        score = 0;
    }

    public int GridParse(Vector2 direction){
        gridPositions = new GameObject[4,4];
        foreach(Transform childTile in grid.transform){
            if(childTile.position.x < 0 && childTile.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) - 1);
                gridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = childTile.gameObject;
                gridNumbers[Mathf.Abs(positionX),Mathf.Abs(positionY)] = int.Parse(childTile.gameObject.transform.Find("Canvas/Text").GetComponent<Text>().text);
            }else if(childTile.position.x > 0 && childTile.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) - 1);
                gridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = childTile.gameObject;
                gridNumbers[Mathf.Abs(positionX),Mathf.Abs(positionY)] = int.Parse(childTile.gameObject.transform.Find("Canvas/Text").GetComponent<Text>().text);
            }else if(childTile.position.x < 0 && childTile.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) + 2);
                gridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = childTile.gameObject;
                gridNumbers[Mathf.Abs(positionX),Mathf.Abs(positionY)] = int.Parse(childTile.gameObject.transform.Find("Canvas/Text").GetComponent<Text>().text);
            }else if(childTile.position.x > 0 && childTile.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) + 2);
                gridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = childTile.gameObject;
                gridNumbers[Mathf.Abs(positionX),Mathf.Abs(positionY)] = int.Parse(childTile.gameObject.transform.Find("Canvas/Text").GetComponent<Text>().text);
            }
        }

        fusionFinish = false;
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
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY)){
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
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY)){
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
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY)){
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
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y, firstX, firstY)){
                                y++;
                            }
                        }
                    }
                }
            }
        }
        fusionFinish = true;
        return score;
        
    }

    bool FusionCheck(Transform firstComapre, Transform secondCompare , int x, int y , int firstX, int firstY){
        Transform firstText = firstComapre.Find("Canvas/Text");
        Transform secondText = secondCompare.Find("Canvas/Text");
        if(firstText.GetComponent<Text>().text == secondText.GetComponent<Text>().text){
            int tmp = int.Parse(firstText.GetComponent<Text>().text) + int.Parse(secondText.GetComponent<Text>().text);
            gridPositions[firstX, firstY].transform.Find("Canvas/Text").GetComponent<Text>().text = tmp.ToString();
            gridPositions[x,y] = null;
            score += tmp;
            return true;
        }else{
            return false;
        }
    }
}
