using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SqaureMovement : MonoBehaviour
{
    private float currentTime = 0f;
    private float timeToMove = 0.1f;
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if(gameManager.GetComponent<Fusion>().fusionFinish){
            ShiftPieces();
        }
        
    }

    void ShiftPieces(){
        if(gameManager.GetComponent<PieceManager>().dirLeft){
            RaycastHit2D hitLeft = Physics2D.Raycast(this.transform.position, Vector2.left);
            if(this.transform.position.x < 0 && transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitLeft.point + new Vector2(1.375f, 0f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitLeft.point.x + 1.375f, hitLeft.point.y + 0f, 10f);
                currentTime = 0;
                if(this.transform.position.x < 0 && transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }else if(gameManager.GetComponent<PieceManager>().dirRight){
            RaycastHit2D hitRight = Physics2D.Raycast(this.transform.position, Vector2.right);
            if(this.transform.position.x < 0 && transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitRight.point + new Vector2(-1.375f, 0f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitRight.point.x - 1.375f, hitRight.point.y + 0f, 10f);
                currentTime = 0;
                if(this.transform.position.x < 0 && transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }else if(gameManager.GetComponent<PieceManager>().dirDown){
            RaycastHit2D hitDown = Physics2D.Raycast(this.transform.position, Vector2.down);
            if(this.transform.position.x < 0 && transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitDown.point + new Vector2(0f, 1.375f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitDown.point.x + 0f, hitDown.point.y + 1.375f, 10f);
                currentTime = 0;
                if(this.transform.position.x < 0 && transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }else if(gameManager.GetComponent<PieceManager>().dirUp){
            RaycastHit2D hitUp = Physics2D.Raycast(this.transform.position, Vector2.up);
            if(this.transform.position.x < 0 && transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = 0;
            }
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitUp.point + new Vector2(0f, -1.375f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitUp.point.x + 0f, hitUp.point.y - 1.375f, 10f);
                currentTime = 0;
                if(this.transform.position.x < 0 && transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y < 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) - 1);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x < 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) - 1);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }else if(this.transform.position.x > 0 && this.transform.position.y > 0){
                    int positionX = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.x)/2) + 2);
                    int positionY = (int)(Mathf.Floor(Mathf.Abs(this.transform.position.y)/2) + 2);
                    gameManager.GetComponent<Fusion>().intGridPositions[Mathf.Abs(positionX),Mathf.Abs(positionY)] = this.GetComponent<TileValue>().GetTileNum();
                }
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }
    }
}
