using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Order of actions:
Check Fusion
Fuse if pairs
Move
Spawn new piece
*/

/*Things to fix:
Objects spawing cause other pieces not to move that are on the new pieces path
*/
public class PieceManager : MonoBehaviour
{
    public GameObject [] sqaures = new GameObject[2];
    private float [] potentialPositions = new float[] {-3.75f, -1.25f, 1.25f, 3.75f};
    private int count;
    public GameObject grid;
    private bool dirLeft = false;
    private bool dirRight = false;
    private bool dirUp = false;
    private bool dirDown = false;
    private static bool hasMoved;
    public int pieceCount = 0;
    void Start()
    {
        count = 0;
        sqaures[0].name = "0";
        GameObject tmp = Instantiate(sqaures[0], transform.position, Quaternion.identity);
        tmp.name = sqaures[0].name.Replace("(Clone)", "");
        tmp.transform.SetParent(grid.transform, false);
        count++;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            this.GetComponent<Fusion>().GridParse(Vector2.left);
            dirLeft = true;
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            this.GetComponent<Fusion>().GridParse(Vector2.right);
            dirRight = true;
        }else if(Input.GetKeyDown(KeyCode.UpArrow)){
            this.GetComponent<Fusion>().GridParse(Vector2.up);
            dirUp = true;
        }else if(Input.GetKeyDown(KeyCode.DownArrow)){
            this.GetComponent<Fusion>().GridParse(Vector2.down);
            dirDown = true;
        }

        if(pieceCount == grid.transform.childCount){
            pieceCount = 0;
            if(dirLeft){
                CreatePiece(this.GetComponent<Fusion>().gridPositions);
                dirLeft = false;
            }else if(dirRight){
                CreatePiece(this.GetComponent<Fusion>().gridPositions);
                dirRight = false;
            }else if(dirDown){
                CreatePiece(this.GetComponent<Fusion>().gridPositions);
                dirDown = false;
            }else if(dirUp){
                CreatePiece(this.GetComponent<Fusion>().gridPositions);
                dirUp = false;
            }
        }
        
    }

    public void CreatePiece(GameObject [,] piecePostion){
        int listLength = this.GetComponent<Fusion>().emptyGridPositions.Count;
        int spawnPoint = Random.Range(0, listLength);
        Vector3 spawnPos = this.GetComponent<Fusion>().emptyGridPositions[spawnPoint];
        GameObject tmp = sqaures[Random.Range(0,1)];
        tmp.name = count.ToString();
        GameObject nameChange = Instantiate(tmp, spawnPos, Quaternion.identity);
        nameChange.name = tmp.name.Replace("(Clone)","");
        nameChange.transform.SetParent(grid.transform, false);
        count++;
    }

}
