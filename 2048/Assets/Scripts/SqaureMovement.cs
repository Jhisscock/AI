using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SqaureMovement : MonoBehaviour
{
    private bool dirLeft = false;
    private bool dirRight = false;
    private bool dirUp = false;
    private bool dirDown = false;
    private bool canMove = true;
    private bool moveRight = true;
    private float currentTime = 0f;
    private float timeToMove = 0.1f;
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && canMove){
            dirLeft = true;
            canMove = false;
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && canMove){
            dirRight = true;
            canMove = false;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && canMove){
            dirUp = true;
            canMove = false;
        }else if(Input.GetKeyDown(KeyCode.DownArrow) && canMove){
            dirDown = true;
            canMove = false;
        }

        ShiftPieces();
        
    }

    void ShiftPieces(){
        if(dirLeft){
            RaycastHit2D hitLeft = Physics2D.Raycast(this.transform.position, Vector2.left);
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitLeft.point + new Vector2(1.375f, 0f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitLeft.point.x + 1.375f, hitLeft.point.y + 0f, 10f);
                canMove = true;
                dirLeft = false;
                currentTime = 0;
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }else if(dirRight){
            RaycastHit2D hitRight = Physics2D.Raycast(this.transform.position, Vector2.right);
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitRight.point + new Vector2(-1.375f, 0f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitRight.point.x - 1.375f, hitRight.point.y + 0f, 10f);
                canMove = true;
                dirRight = false;
                currentTime = 0;
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }else if(dirDown){
            RaycastHit2D hitDown = Physics2D.Raycast(this.transform.position, Vector2.down);
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitDown.point + new Vector2(0f, 1.375f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitDown.point.x + 0f, hitDown.point.y + 1.375f, 10f);
                canMove = true;
                dirDown = false;
                currentTime = 0;
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }else if(dirUp){
            RaycastHit2D hitUp = Physics2D.Raycast(this.transform.position, Vector2.up);
            if(currentTime <= timeToMove){
                currentTime += Time.deltaTime;
                this.transform.position = Vector2.Lerp(this.transform.position, hitUp.point + new Vector2(0f, -1.375f), currentTime/timeToMove);
            }else{
                this.transform.position = new Vector3(hitUp.point.x + 0f, hitUp.point.y - 1.375f, 10f);
                canMove = true;
                dirUp = false;
                currentTime = 0;
                gameManager.GetComponent<PieceManager>().pieceCount++;
            }
        }
    }
}
