import java.util.*;

public class TTT{

    static class Position{
        int x;
        int y;
        int score;
        Position(int x, int y, int score){
            this.x = x;
            this.y = y;
            this.score = score;
        }
    }
    public static void main (String [] args){
        Random rand = new Random();
        int [][] board = new board [3][3];
        for(int i = 0; i < 3; i++){
            for(int ii = 0; ii < 3; ii++){
                board[i][ii] = null;
            }
        }
        int startx = rand.nextInt(4);
        int starty = rand.nextInt(4);
        Position start = new Position(startx, starty, 0);
        minimax(board, 3, start, true);
    }

    public static Position minimax(int[][]board, int depth, Position currPosition, bool maxPlayer){
        if (depth == 0) return currPosition;
        if (maxPlayer){
            Integer maxEval = Integer.MIN_VALUE;
            for(int i = -1; i < 2; i++){
                for(int ii = -1; ii < 2; ii++){
                    if(currPosition.x + i >= 0 && currPosition.y >= 0){
                        if(board[currPosition.x + i][currPosition.y + ii] == 1 && (currPosition.x + i != 0 && currPosition.y + ii != 0)){
                            currPosition.score++;
                        }
                    }
                }
            }
            maxEval = Math.max(maxEval.intValue(), currPosition.score);
        }
        return null;
    }

}