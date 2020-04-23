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
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int firstX = -1;
                    int firstY = -1;
                    int count = 0;
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
                                int positionShift = x - firstX - 1;
                                gridPositions[x - positionShift, y] = secondCompare;
                                gridPositions[x,y] = 0;
                            }
                        }
                    }
                }
            }else if(direction == Vector2.right){
                for(int y = 0; y < 4; y++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int firstX = -1;
                    int firstY = -1;
                    int count = 0;
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
                                int positionShift = x + firstX - 1;
                                gridPositions[positionShift - x, y] = secondCompare;
                                gridPositions[x,y] = 0;
                            }
                        }
                    }
                }
            }else if(direction == Vector2.down){
                for(int x = 0; x < 4; x++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int firstX = -1;
                    int firstY = -1;
                    int count = 0;
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
                                int positionShift = y - firstY - 1;
                                gridPositions[x, y - positionShift] = secondCompare;
                                gridPositions[x,y] = 0;
                            }
                        }
                    }
                }
            }else if(direction == Vector2.up){
                for(int x = 0; x < 4; x++){
                    int firstCompare = 0;
                    int secondCompare = 0;
                    int firstX = -1;
                    int firstY = -1;
                    int count = 0;
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
                                int positionShift = y + firstY - 1;
                                gridPositions[x , positionShift - y] = secondCompare;
                                gridPositions[x,y] = 0;
                            }
                        }
                    }
                }
            }
        }
        
    }
}
