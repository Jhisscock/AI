using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/* To Do:
Implement minimax giving higher scores to combined tiles (maybe to tiles in the top left corner)
    Check moves in all four directions to see which yeild highest score
    Maybe implement future moves as well
Use A* to find best score based on position of highest point tiles in corners


Example ExpectiMax:
function expectimax(node, depth, player)
 if node is a terminal node or depth = 0
    return the heuristic value of node
 if the adversary is to play at node
    // Return value of minimum-valued child node
    let α := +∞
    foreach child of node
        α := min(α, expectimax(child, depth-1))
 else if we are to play at node
    // Return value of maximum-valued child node
    let α := -∞
    foreach child of node
        α := max(α, expectimax(child, depth-1))
 else if random event at node
    // Return weighted average of all child nodes' values
    let α := 0
    foreach child of node
        α := α + (Probability[child] * expectimax(child,depth-1))
 return α

*/
public class MiniMax : MonoBehaviour
{
    public GameObject square;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 bestMove(ref GameObject [,] grid){
        this.GetComponent<PieceManager>().aiComplete = false;
        int bestScore = int.MinValue;
        Vector2 bestMove = Vector2.zero;
        for(int i = 0; i < 4; i++){
            GameObject [,] copyGrid = new GameObject[4,4];
            for(int x = 0; x < 4; x++){
                for(int y = 0; y < 4; y++){
                    copyGrid[x,y] = grid[x,y];
                }
            }
            int mark = 0;
            switch(i){
                case 0:
                    //Move left add movement to list of moves along with its score
                    if(this.GetComponent<FusionAI>().canMove(ref copyGrid, Vector2.left)){
                        mark = this.GetComponent<FusionAI>().GridParse(Vector2.left, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.left, ref copyGrid);
                    }else{
                        mark = int.MinValue;
                    }
                    break;
                case 1:
                    //Move right
                    if(this.GetComponent<FusionAI>().canMove(ref copyGrid, Vector2.right)){
                        mark = this.GetComponent<FusionAI>().GridParse(Vector2.right, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.right, ref copyGrid);
                    }else{
                        mark = int.MinValue;
                    }
                    break;
                case 2:
                    //Move down
                    if(this.GetComponent<FusionAI>().canMove(ref copyGrid, Vector2.down)){
                        mark = this.GetComponent<FusionAI>().GridParse(Vector2.down, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.down, ref copyGrid);
                    }else{
                        mark = int.MinValue;
                    }
                    break;
                case 3:
                    //Move up
                    if(this.GetComponent<FusionAI>().canMove(ref copyGrid, Vector2.up)){
                        mark = this.GetComponent<FusionAI>().GridParse(Vector2.up, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.up, ref copyGrid);
                    }else{
                        mark = int.MinValue;
                    }
                    break;
                default:
                    mark = 0;
                    break;
            }

            int currentScore = ExpectiMax(ref copyGrid, 8, false) + mark;
            if(currentScore > bestScore){
                bestScore = currentScore;
                switch(i){
                    case 0:
                        //Move left add movement to list of moves along with its score
                        bestMove = Vector2.left;
                        break;
                    case 1:
                        //Move right
                        bestMove = Vector2.right;
                        break;
                    case 2:
                        //Move down
                        bestMove = Vector2.down;
                        break;
                    case 3:
                        //Move up
                        bestMove = Vector2.up;
                        break;
                    default:
                        bestMove = Vector2.zero;
                        break;
                }
            }
        }
        Debug.Log(bestScore);
        return bestMove;
    }
    public int ExpectiMax(ref GameObject [,] grid, int depth , bool maxPlayer){
        if(depth == 0){
            return cornerHeuristic(grid);
        }
        if(maxPlayer){
            int heuristicValue = -1;
            for(int i = 0; i < 4; i++){
                int score = 0;
                GameObject [,] copyGrid = new GameObject[4,4];
                for(int x = 0; x < 4; x++){
                    for(int y = 0; y < 4; y++){
                        copyGrid[x,y] = grid[x,y];
                    }
                }
                switch(i){
                    case 0:
                        //Move left add movement to list of moves along with its score
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.left, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.left, ref copyGrid);
                        break;
                    case 1:
                        //Move right
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.right, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.right, ref copyGrid);
                        break;
                    case 2:
                        //Move down
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.down, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.down, ref copyGrid);
                        break;
                    case 3:
                        //Move up
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.up, ref copyGrid);
                        this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.up, ref copyGrid);
                        break;
                    default:
                        score = 0;
                        break;
                }
                int potentialMax = ExpectiMax(ref copyGrid, depth-1, false) + score;
                if(potentialMax > heuristicValue){
                    heuristicValue = potentialMax;
                }
            }
            return heuristicValue;
            
        }else{
            int sumValue = 0;
            int count = 0;
            for(int i = 0; i < this.GetComponent<FusionAI>().emptyGridPositions.Count; i++){
                GameObject [,] copyGrid = new GameObject[4,4];
                List<int[]> emptyPositions = new List<int[]>();
                for(int x = 0; x < 4; x++){
                    for(int y = 0; y < 4; y++){
                        copyGrid[x,y] = grid[x,y];
                        if(copyGrid[x,y] != null){
                            continue;
                        }else{
                            emptyPositions.Add(new int [] {x, y});
                        }
                    }
                }
                int [] newPiece = emptyPositions[i];
                copyGrid[newPiece[0],newPiece[1]] = square;
                copyGrid[newPiece[0],newPiece[1]].GetComponent<TileValue>().ChangeTileNum(2);

                sumValue += ExpectiMax(ref copyGrid, depth-1, true);
                count++;

            }
            if(count == 0){
                return ExpectiMax(ref grid, depth-1, true);
            }
            return sumValue/count;
            
        }
    }

    public int cornerHeuristic(GameObject [,] grid){
        int score = 0;
        int [,] bestPositionSet = new int [,] 
        {   {0,2,4,8},
            {16,32,64,128},
            {256,512,1024,2048},
            {4096,8192,16384,32768}};
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] != null){
                    score += bestPositionSet[i,j] * grid[i,j].GetComponent<TileValue>().GetTileNum();
                }
            }
        }
        return score;
    }

    /*public int sideHeuristic(GameObject [,] grid){
        int score = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] != null){
                    if(i == 0 || i == 3 || j == 0|| j == 3){
                        if((i == 0 || i == 3) && (j == 0 || j == 3)){
                            score += 10 * grid[i,j].GetComponent<TileValue>().GetTileNum();
                        }else{
                            score += 5 * grid[i,j].GetComponent<TileValue>().GetTileNum();
                        }
                    }else{
                        score += grid[i,j].GetComponent<TileValue>().GetTileNum();
                    }
                }
            }
        }
        return score;
    }*/
}
