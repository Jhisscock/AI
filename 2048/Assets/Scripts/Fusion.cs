using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/*Things to fix:
Sometimes objects don't fuse when they are supposed to
*/

public class Fusion : MonoBehaviour
{
    public GameObject grid;
    public GameObject [,] gridPositions = new GameObject[4,4];
    public List<Vector3> emptyGridPositions = new List<Vector3>();

    public void GridParse(Vector2 direction){
        gridPositions = new GameObject[4,4];
        emptyGridPositions = new List<Vector3> {
            new Vector3 (-3.75f,-3.75f, 10f),
            new Vector3 (-3.75f,-1.25f, 10f),
            new Vector3 (-3.75f, 1.25f, 10f),
            new Vector3 (-3.75f, 3.75f, 10f),
            new Vector3 (-1.25f,-3.75f, 10f),
            new Vector3 (-1.25f,-1.25f, 10f),
            new Vector3 (-1.25f,-1.25f, 10f),
            new Vector3 (-1.25f, 3.75f, 10f),
            new Vector3 ( 1.25f,-3.75f, 10f),
            new Vector3 ( 1.25f,-1.25f, 10f),
            new Vector3 ( 1.25f, 1.25f, 10f),
            new Vector3 ( 1.25f, 3.75f, 10f),
            new Vector3 ( 3.75f,-3.75f, 10f),
            new Vector3 ( 3.75f, 1.25f, 10f),
            new Vector3 ( 3.75f, 1.25f, 10f),
            new Vector3 ( 3.75f, 3.75f, 10f),
        };
        foreach(Transform childTile in grid.transform){
            foreach(Vector3 newEmptyPosition in emptyGridPositions.ToList()){
                if(((int)newEmptyPosition.x == (int)childTile.position.x) && ((int)newEmptyPosition.y == (int)childTile.position.y)){
                    emptyGridPositions.Remove(newEmptyPosition);
                }
            }
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
                            FusionCheck(firstCompare.transform, secondCompare.transform, x, y);
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
                            FusionCheck(firstCompare.transform, secondCompare.transform, x, y);
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
                            FusionCheck(firstCompare.transform, secondCompare.transform, x, y);
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
                            FusionCheck(firstCompare.transform, secondCompare.transform, x, y);
                        }
                    }
                }
            }
        }
        
    }

    //Fusion Sometimes not working
    void FusionCheck(Transform firstComapre, Transform secondCompare , int x, int y){
        Transform firstText = firstComapre.Find("Canvas/Text");
        Transform secondText = secondCompare.Find("Canvas/Text");
        if(firstText.GetComponent<Text>().text == secondText.GetComponent<Text>().text){
            int tmp = int.Parse(firstText.GetComponent<Text>().text) + int.Parse(secondText.GetComponent<Text>().text);
            firstText.GetComponent<Text>().text = tmp.ToString();
            gridPositions[x,y] = null;
            Destroy(secondCompare.gameObject);
        }
    }
}
