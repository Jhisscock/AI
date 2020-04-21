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

    public Vector2 bestMove(GameObject [,] grid){
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
            int mark;
            switch(i){
                case 0:
                    //Move left add movement to list of moves along with its score
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.left);
                    break;
                case 1:
                    //Move right
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.right);
                    break;
                case 2:
                    //Move down
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.down);
                    break;
                case 3:
                    //Move up
                    mark = this.GetComponent<FusionAI>().GridParse(Vector2.up);
                    break;
                default:
                    mark = 0;
                    break;
            }

            int currentScore = ExpectiMax(copyGrid, 4, false) + mark;
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
    public int ExpectiMax(GameObject [,] grid, int depth , bool maxPlayer){
        if(depth == 0){
            return cornerHeuristic(grid);
        }
        if(maxPlayer){
            int heuristicValue = -1;
            for(int i = 0; i < 4; i++){
                int score;
                GameObject [,] copyGrid = new GameObject[4,4];
                for(int x = 0; x < 4; x++){
                    for(int y = 0; y < 4; y++){
                        copyGrid[x,y] = grid[x,y];
                    }
                }
                switch(i){
                    case 0:
                        //Move left add movement to list of moves along with its score
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.left);
                        break;
                    case 1:
                        //Move right
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.right);
                        break;
                    case 2:
                        //Move down
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.down);
                        break;
                    case 3:
                        //Move up
                        score = this.GetComponent<FusionAI>().GridParse(Vector2.up);
                        break;
                    default:
                        score = 0;
                        break;
                }
                int potentialMax = ExpectiMax(copyGrid, depth-1, false) + score;
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
                int twoOrFour;
                if(Random.value < 0.9){
                    twoOrFour = 2;
                }else{
                    twoOrFour = 4;
                }
                int [] newPiece = emptyPositions[i];
                copyGrid[newPiece[0],newPiece[1]] = square;
                copyGrid[newPiece[0],newPiece[1]].transform.Find("Canvas/Text").transform.GetComponent<Text>().text = twoOrFour.ToString();

                sumValue += ExpectiMax(copyGrid, depth-1, true);
                count++;

            }
            if(count == 0){
                return ExpectiMax(grid, depth-1, true);
            }
            return sumValue/count;
            
        }
    }

    public int cornerHeuristic(GameObject [,] grid){
        int score = 0;
        for(int i = 0; i < 4; i++){
            for(int j = 0; j < 4; j++){
                if(grid[i,j] != null){
                    if(i == 0 || i == 3 || j == 0 || j == 3){
                        if((i == 0 || i == 3) && (j == 0 || j == 3)){
                            score += 10 * int.Parse(grid[i,j].transform.Find("Canvas/Text").transform.GetComponent<Text>().text);
                        }else{
                            score += 5 * int.Parse(grid[i,j].transform.Find("Canvas/Text").transform.GetComponent<Text>().text);
                        }
                    }else{
                        score += int.Parse(grid[i,j].transform.Find("Canvas/Text").transform.GetComponent<Text>().text);
                    }
                }
            }
        }
        return score;
    }
}
