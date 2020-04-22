using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fusion : MonoBehaviour
{
    public GameObject grid;
    public GameObject [,] gridPositions = new GameObject[4,4];
    public List<Vector3> emptyGridPositions = new List<Vector3>();
    public bool fusionFinish = false;
    public int score;

    void Start(){
        score = 0;
    }

    public void GridParse(Vector2 direction){
        gridPositions = new GameObject[4,4];
        foreach(Transform childTile in grid.transform){
            childTile.GetComponent<TileValue>().ChangeTileNum(int.Parse(childTile.transform.Find("Canvas/Text").GetComponent<Text>().text));
            if(childTile.position.x < 0 && childTile.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) - 1);
                gridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = childTile.gameObject;
            }else if(childTile.position.x > 0 && childTile.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) - 1);
                gridPositions[positionX,Mathf.Abs(positionY)] = childTile.gameObject;
            }else if(childTile.position.x < 0 && childTile.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) + 2);
                gridPositions[Mathf.Abs(positionX),positionY] = childTile.gameObject;
            }else if(childTile.position.x > 0 && childTile.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(childTile.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(childTile.position.y)/2) + 2);
                gridPositions[positionX,positionY] = childTile.gameObject;
            }
        }

        fusionFinish = false;
        if(direction == Vector2.left){
            for(int y = 0; y < gridPositions.GetLength(1); y++){ 
                GameObject firstCompare = null;
                GameObject secondCompare = null;
                int count = 0;
                for(int x = 0; x < gridPositions.GetLength(0); x++){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y)){
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
                for(int x = gridPositions.GetLength(0)-1; x >= 0; x--){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y)){
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
                for(int y = 0; y < gridPositions.GetLength(1); y++){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y)){
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
                for(int y = gridPositions.GetLength(1)-1; y >= 0; y--){
                    if(gridPositions[x,y] != null){
                        if(count == 0){
                            firstCompare = gridPositions[x,y];
                            count++;
                        }else if(count == 1){
                            secondCompare = gridPositions[x,y];
                            count = 0;
                            if(!FusionCheck(firstCompare.transform, secondCompare.transform, x, y)){
                                y++;
                            }
                        }
                    }
                }
            }
        }
        fusionFinish = true;
        
    }

    bool FusionCheck(Transform firstComapre, Transform secondCompare , int x, int y){
        Transform firstText = firstComapre.Find("Canvas/Text");
        Transform secondText = secondCompare.Find("Canvas/Text");
        if(firstText.GetComponent<Text>().text == secondText.GetComponent<Text>().text){
            int tmp = int.Parse(firstText.GetComponent<Text>().text) + int.Parse(secondText.GetComponent<Text>().text);
            firstText.GetComponent<Text>().text = tmp.ToString();
            if(tmp >= 1000){
                firstText.GetComponent<Text>().fontSize = 40;
            }
            Destroy(secondCompare.gameObject);
            score += tmp;
            return true;
        }else{
            return false;
        }
    }
}
