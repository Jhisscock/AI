using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceMovementAi : MonoBehaviour
{

    void Start()
    {

    }

    public void ShiftPieces(Vector2 direction , ref int [,] gridPositions){
        if(this.GetComponent<FusionAI>().canMove(gridPositions, direction)){
            if(direction == Vector2.left){
                for(int y = 0; y < 4; y++){
                    for(int x = 0; x < 4; x++){
                        if(gridPositions[x,y] != 0){
                            for(int i = x-1; i >= 0; i--){
                                if(i >= 0){
                                    if(gridPositions[i,y] != 0){
                                        int moveDistance = i - x + 1;
                                        gridPositions[moveDistance + x,y] = gridPositions[x,y];
                                    }else if(i == 0){
                                        int tmp = gridPositions[x,y];
                                        gridPositions[x,y] = 0;
                                        gridPositions[i,y] = tmp;
                                    }
                                }
                            }
                        }
                    }
                }
            }else if(direction == Vector2.right){
                for(int y = 0; y < 4; y++){
                    for(int x = 3; x >= 0; x--){
                        if(gridPositions[x,y] != 0){
                            for(int i = x+1; i < 4; i++){
                                if(i <= 3){
                                    if(gridPositions[i,y] != 0){
                                        int moveDistance = i - x - 1;
                                        gridPositions[moveDistance + x,y] = gridPositions[x,y];
                                    }else if(i == 3){
                                        int tmp = gridPositions[x,y];
                                        gridPositions[x,y] = 0;
                                        gridPositions[i,y] = tmp;
                                    }
                                }
                            }
                        }
                    }
                }
            }else if(direction == Vector2.down){
                for(int x = 0; x < 4; x++){
                    for(int y = 0; y < 4; y++){
                        if(gridPositions[x,y] != 0){
                            for(int i = y-1; i >= 0; i--){
                                if(i >= 0){
                                    if(gridPositions[x,i] != 0){
                                        int moveDistance = i - y + 1;
                                        gridPositions[x,moveDistance + y] = gridPositions[x,y];
                                    }else if(i == 0){
                                        int tmp = gridPositions[x,y];
                                        gridPositions[x,y] = 0;
                                        gridPositions[x,i] = tmp;
                                    }
                                }
                            }
                        }
                    }
                }
            }else if(direction == Vector2.up){
                for(int x = 0; x < 4; x++){
                    for(int y = 3; y >= 0; y--){
                        if(gridPositions[x,y] != 0){
                            for(int i = y+1; i < 4; i++){
                                if(i <= 3){
                                    if(gridPositions[x,i] != 0){
                                        int moveDistance = i - y - 1;
                                        gridPositions[x,moveDistance + y] = gridPositions[x,y];
                                    }else if(i == 3){
                                        int tmp = gridPositions[x,y];
                                        gridPositions[x,y] = 0;
                                        gridPositions[x,i] = tmp;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}
