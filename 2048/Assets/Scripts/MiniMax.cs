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

    public Vector2 bestMove( int [,] grid){
        int n = 4;
        this.GetComponent<PieceManager>().aiComplete = false;
        float bestScore = 0;
        Vector2 bestMove = Vector2.zero;
        Vector2 prevBestMove = Vector2.zero;
        for(int i = 0; i < 4; i++){
            int [,] copyGrid = grid.Clone() as int[,];
            float mark = 0;
            switch(i){
                case 0:
                    //Move left add movement to list of moves along with its score
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.left, ref copyGrid);
                    this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.left, ref copyGrid);
                    break;
                case 1:
                    //Move right
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.right, ref copyGrid);
                    this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.right, ref copyGrid);
                    break;
                case 2:
                    //Move down
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.down, ref copyGrid);
                    this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.down, ref copyGrid);
                    break;
                case 3:
                    //Move up
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.up, ref copyGrid);
                    this.GetComponent<PieceMovementAi>().ShiftPieces(Vector2.up, ref copyGrid);
                    break;
                default:
                    mark = 0;
                    break;
                }
            float currentScore = ExpectiMax(ref copyGrid, n-1, false, i) + mark;
            Debug.Log(currentScore + " : " + i);
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
        return bestMove;
    }
    public float ExpectiMax(ref int [,] grid, int depth , bool maxPlayer, int d){
        if(depth == 0){
            string gridString = "";
                for(int ii = 3; ii >= 0; ii--){
                    for(int j = 0; j < 4; j++){
                        gridString += string.Format("{0} ", grid[j, ii]);
                    }
                    gridString += System.Environment.NewLine + System.Environment.NewLine;
                }
                Debug.Log(gridString);
                Debug.Log(d);
            return monotonicHeuristic(grid) + smoothnessHeuristic(grid) * 0.1f + Mathf.Log(emptySpaceHeuristic(grid)) * 2.7f + cornerHeuristic(grid);
        }
        if(maxPlayer){
            float heuristicValue = -1;
            float score = 0;
            for(int i = 0; i < 4; i++){
                int [,] copyGrid = grid.Clone() as int[,];
                switch(i){
                    case 0:
                        //Move left
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
                float potentialMax = ExpectiMax(ref copyGrid, depth-1, false, i) + score;
                if(potentialMax > heuristicValue){
                    heuristicValue = potentialMax;
                }
            }
            return heuristicValue;
            
        }else{
            float sumValue = 0;
            float count = 0;
            List<int[]> emptyPositions = new List<int[]>();
            for(int i = 0; i < this.GetComponent<FusionAI>().emptyGridPositions.Count; i++){
                int [,] copyGrid = grid.Clone() as int[,];
                for(int x = 0; x < 4; x++){
                    for(int y = 0; y < 4; y++){
                        if(copyGrid[x,y] == 0){
                            emptyPositions.Add(new int [] {x, y});
                        }
                    }
                }
                int [] newPiece = emptyPositions[i];
                copyGrid[newPiece[0],newPiece[1]] = 2;

                sumValue += ExpectiMax(ref copyGrid, depth-1, true, d);
                count++;

            }
            if(count == 0){
                return ExpectiMax(ref grid, depth-1, true, d);
            }
            return sumValue/count;
            
        }
    }

    public float monotonicHeuristic(int [,] grid){
        float udTotalGreater = 0;
        float udTotalLesser = 0;
        float lrTotalGreater = 0;
        float lrTotalLesser = 0;
        for(int i = 0; i < 4; i++){
            int current  = 0;
            int next = current + 1;
            while(next < 4){
                while(next < 4 && grid[i,next] == 0){
                    next++;
                }
                if(next >=4){
                    next--;
                }
                float currentValue = 0;
                if(grid[i,current] != 0){
                    currentValue = Mathf.Log(grid[i,current]);
                }else{
                    currentValue = 0;
                }
                float nextValue = 0;
                if(grid[i, next] != 0){
                    nextValue = Mathf.Log(grid[i,next]);
                }else{
                    nextValue = 0;
                }
                if(currentValue > nextValue){
                    udTotalGreater += nextValue - currentValue;
                }else if(nextValue < currentValue){
                    udTotalLesser += currentValue - nextValue;
                }
                current = next;
                next++;
            }
        }

        for(int i = 0; i < 4; i++){
            int current  = 0;
            int next = current + 1;
            while(next < 4){
                while(next < 4 && grid[next,i] == 0){
                    next++;
                }
                if(next >=4){
                    next--;
                }
                float currentValue = 0;
                if(grid[current, i] != 0){
                    currentValue = Mathf.Log(grid[current, i]);
                }else{
                    currentValue = 0;
                }
                float nextValue = 0;
                if(grid[next, i] != 0){
                    nextValue = Mathf.Log(grid[next, i]);
                }else{
                    nextValue = 0;
                }
                if(currentValue > nextValue){
                    lrTotalGreater += nextValue - currentValue;
                }else if(nextValue < currentValue){
                    lrTotalLesser += currentValue - nextValue;
                }
                current = next;
                next++;
            }
        }

        return Mathf.Max(udTotalGreater, udTotalLesser) + Mathf.Max(lrTotalGreater, lrTotalLesser);
    }

    public float smoothnessHeuristic(int [,] grid){
        float smoothness = 0;
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] != 0 ){
                    float value = Mathf.Log(grid[i,j]) / Mathf.Log(2);
                    for(int ii = i+1; ii < 4; ii++){
                        if(grid[ii,j] != 0){
                            float targetValue = Mathf.Log(grid[ii,j])/ Mathf.Log(2);
                            smoothness -= Mathf.Abs(value - targetValue);
                        }
                    }
                }
            }
        }

        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 3; j++){
                if(grid[i,j] != 0 ){
                    float value = Mathf.Log(grid[i,j]) / Mathf.Log(2);
                    for(int jj = j+1; jj < 4; jj++){
                        if(grid[i,jj] != 0){
                            float targetValue = Mathf.Log(grid[i,jj])/Mathf.Log(2);
                            smoothness -= Mathf.Abs(value - targetValue);
                        }
                    }
                }
            }
        }
        return smoothness;
    }

    public float emptySpaceHeuristic(int [,] grid){
        float score = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] == 0){
                    score++;
                }
            }
        }
        return score;
    }

    
    public float cornerHeuristic(int [,] grid){
        float score = 0;
        int [,] bestPositionSet = new int [,] 
        {   {20,5,1,0},
            {75,20,5,1},
            {150,75,20,5},
            {300,150,75,20}};
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                score += bestPositionSet[i,j] * grid[i,j];
            }
        }
        return score;
    }

    /*public int bestPlacementHeuristic(int [,] grid){
        int score = 0;
        List<piecesList> currentPieces = new List<piecesList>();
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] != 0){
                    piecesList tmp = new piecesList();
                    tmp.pieceValue = grid[i,j];
                    tmp.piecePosition = new int[]{i,j};
                    currentPieces.Add(tmp);
                }
            }
        }
        currentPieces.OrderByDescending(piecesList => piecesList.pieceValue);
        int countX = 0;
        int countY = 0;
        for(int i = 0; i < currentPieces.Count; i++){
            piecesList tmp = currentPieces[i];
            int [] position = tmp.piecePosition;
            int value = tmp.pieceValue;
            score += (Mathf.Abs(position[0] - countX) + Mathf.Abs(position[1] - countY)) * value;
            countX--;
            if(countY < 0){
                countX = 3;
                countX++;
            }
        }
        return score * 100;
    }

    

    struct piecesList{
        public int pieceValue;
        public int [] piecePosition;
    }

    public int sideHeuristic(int [,] grid){
        int score = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] != 0){
                    if(i == 0 || i == 3 || j == 0|| j == 3){
                        if((i == 0 || i == 3) && (j == 0 || j == 3)){
                            score += 10 * grid[i,j];
                        }else{
                            score += 5 * grid[i,j];
                        }
                    }else{
                        score += grid[i,j];
                    }
                }
            }
        }
        return score;
    }*/
}
