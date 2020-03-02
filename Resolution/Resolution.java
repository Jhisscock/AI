import java.util.*;

public class Resolution{
    public static boolean foundResolution = false;
    public static void main (String [] args){
        Scanner sc = new Scanner(System.in);
        System.out.println("Input one clause at a time hitting enter in between each, when finished type stop");
        List<String> startClauses = new ArrayList<String>();

        //take input and add startClauses to an array list
        while(true){
            String tmp = sc.nextLine();
            if(tmp.equals("stop")){
                break;
            }else{
                startClauses.add(tmp);
            }
        }

        //Create a list of clauses stored with their elements attached to a true or false value within an array
        ArrayList<Clause[]> clauses = new ArrayList<Clause[]>();
        for(int i = 0; i < startClauses.size(); i++){
            List<Clause> temp = new ArrayList<Clause>();
            for(int j = 0; j < startClauses.get(i).length(); j++){
                char tmp = startClauses.get(i).charAt(j);
                if(tmp == ' ' || tmp == 'v'){
                    continue;
                }else{
                    if(tmp == 170){
                       temp.add(new Clause(false, Character.toString(startClauses.get(i).charAt(j+2))));
                        j += 2;
                    }else{
                        temp.add(new Clause(true, Character.toString(startClauses.get(i).charAt(j))));
                    }
                }
            }
            clauses.add(temp.toArray(new Clause[0]));
        }

        System.out.println(PLResolution(clauses));
        
    }

    //PL_Resolution
    public static boolean PLResolution(ArrayList<Clause[]> clausesList){

        //Main Loop
        ArrayList<Clause[]> newClauses = new ArrayList<Clause[]>();
        while(true){

            //Find the resolvents of each pair of clauses within clausesList
            ArrayList<Clause[]> resolvents = new ArrayList<Clause[]>();
            for(int i = 0; i < clausesList.size(); i++){
                for(int j = 0; j < clausesList.size(); j++){
                    if(i < j){
                        resolvents.add(PL_Resolve(clausesList.get(i), clausesList.get(j)).toArray(new Clause[0]));
                        if(foundResolution){
                            return true;
                        }
                    }
                }
            }
            //add new resolvents to new clauses list
            newClauses.addAll(resolvents);

            //Check to see if newClauses is a subset of clausesList
            int allClauseEqualCount = 0;
            for(int i = 0; i < clausesList.size(); i++){
                for(int j = 0; j < newClauses.size(); j++){
                    int equalClauseCount = 0;
                    for(int k = 0; k < clausesList.get(i).length; k++){
                        for(int l = 0; l < newClauses.get(j).length; l++){
                            if(clausesList.get(i)[k].getElement().equals(newClauses.get(j)[l].getElement()) && clausesList.get(i)[k].getTf() == newClauses.get(j)[l].getTf()){
                                equalClauseCount++;
                            }
                        }
                    }
                    if(equalClauseCount == newClauses.get(j).length){
                        allClauseEqualCount++;
                    }
                    if(allClauseEqualCount == newClauses.size()){
                        return false;
                    }
                }
            }


            //Add all new resolvents to clausesList
            clausesList.addAll(newClauses);
        }
    }

    public static ArrayList<Clause> PL_Resolve(Clause [] c1, Clause [] c2){
        List<Integer> c1Index = new ArrayList<>();
        List<Integer> c2Index = new ArrayList<>();
        for(int i = 0; i < c1.length; i++){
            for(int j = 0; j < c2.length; j++){
                if(c1[i].getElement().equals(c2[j].getElement())){
                    c1Index.add(i);
                    if(c1[i].getTf() != c2[j].getTf()){
                        c2Index.add(j);
                    }
                }
            }
        }

        ArrayList<Clause> tmp = new ArrayList<Clause>();
        for(int i = 0; i < c1.length; i++){
            if(!c1Index.contains(i)){
                tmp.add(c1[i]);
            }
        }
        for(int i = 0; i < c2.length; i++){
            if(!c2Index.contains(i)){
                tmp.add(c2[i]);
            }
        }

        if(tmp.isEmpty()){
            foundResolution = true;
        }

        return tmp;
    }

    //Class to keep track of clause values
    public static class Clause{
        boolean tf;
        String element;
        public Clause(boolean tf, String element){
            this.tf = tf;
            this.element = element;
        }

        public boolean getTf(){
            return tf;
        }

        public String getElement(){
            return element;
        }
    }
    
}