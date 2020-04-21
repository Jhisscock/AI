using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PieceManager : MonoBehaviour
{
    public GameObject square;
    private float [] potentialPositions = new float[] {-3.75f, -1.25f, 1.25f, 3.75f};
    public GameObject grid;
    public bool dirLeft = false;
    public bool dirRight = false;
    public bool dirUp = false;
    public bool dirDown = false;
    public int pieceCount = 0;
    private bool canMove = true;
    private List<Vector3> previousList;
    void Start()
    {
        GameObject tmp = Instantiate(square, transform.position, Quaternion.identity);
        int twoOrFour;
        if(Random.value < 0.9){
            twoOrFour = 2;
        }else{
            twoOrFour = 4;
        }
        tmp.transform.Find("Canvas/Text").transform.GetComponent<Text>().text = twoOrFour.ToString();
        tmp.transform.SetParent(grid.transform, false);
        previousList = new List<Vector3>{new Vector3(100f,100f,100f)};
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && canMove){
            this.GetComponent<Fusion>().GridParse(Vector2.left);
            dirLeft = true;
            canMove = false;
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && canMove){
            this.GetComponent<Fusion>().GridParse(Vector2.right);
            dirRight = true;
            canMove = false;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && canMove){
            this.GetComponent<Fusion>().GridParse(Vector2.up);
            dirUp = true;
            canMove = false;
        }else if(Input.GetKeyDown(KeyCode.DownArrow) && canMove){
            this.GetComponent<Fusion>().GridParse(Vector2.down);
            dirDown = true;
            canMove = false;
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
            canMove = true;
        }
        
    }

    //If time permits implement check to see if there are any possible moves before ending game
    public void CreatePiece(GameObject [,] piecePostion){
        List<Vector3> emptyGridPositions = new List<Vector3> {
            new Vector3 (-3.75f,-3.75f, 10f),
            new Vector3 (-3.75f,-1.25f, 10f),
            new Vector3 (-3.75f, 1.25f, 10f),
            new Vector3 (-3.75f, 3.75f, 10f),
            new Vector3 (-1.25f,-3.75f, 10f),
            new Vector3 (-1.25f,-1.25f, 10f),
            new Vector3 (-1.25f, 1.25f, 10f),
            new Vector3 (-1.25f, 3.75f, 10f),
            new Vector3 ( 1.25f,-3.75f, 10f),
            new Vector3 ( 1.25f,-1.25f, 10f),
            new Vector3 ( 1.25f, 1.25f, 10f),
            new Vector3 ( 1.25f, 3.75f, 10f),
            new Vector3 ( 3.75f,-3.75f, 10f),
            new Vector3 ( 3.75f,-1.25f, 10f),
            new Vector3 ( 3.75f, 1.25f, 10f),
            new Vector3 ( 3.75f, 3.75f, 10f),
        };
        foreach(Transform childTile in grid.transform){
            foreach(Vector3 newEmptyPosition in emptyGridPositions.ToList()){
                if(((int)newEmptyPosition.x == (int)childTile.position.x) && ((int)newEmptyPosition.y == (int)childTile.position.y)){
                    emptyGridPositions.Remove(newEmptyPosition);
                }
            }
        }
        bool canSpawn = true;
        if(Enumerable.SequenceEqual(previousList, emptyGridPositions)){
            canSpawn = false;
        }
        if(emptyGridPositions.Any() && (canSpawn || emptyGridPositions.Count == 1)){
            int listLength = emptyGridPositions.Count;
            int spawnPoint = Random.Range(0, listLength);
            Vector3 spawnPos = emptyGridPositions[spawnPoint];
            GameObject gridChild = Instantiate(square, spawnPos, Quaternion.identity);
            int twoOrFour;
            if(Random.value < 0.9){
                twoOrFour = 2;
            }else{
                twoOrFour = 4;
            }
            square.transform.Find("Canvas/Text").transform.GetComponent<Text>().text = twoOrFour.ToString();
            gridChild.transform.SetParent(grid.transform, false);
            previousList = emptyGridPositions.ToList();
        }else if(!emptyGridPositions.Any()){
            this.GetComponent<GameOver>().EndGame();
        }
        
    }

}
