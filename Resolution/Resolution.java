import java.util.*;

public class Resolution{
    public static boolean foundResolution = false;
    public static void main (String [] args){
        Scanner sc = new Scanner(System.in);
        System.out.println("Input one clause at a time hitting enter in between each, when finished type stop");
        List<String> clauses = new ArrayList<String>();

        //take input and add clauses to an array list
        while(true){
            String tmp = sc.nextLine();
            if(tmp.equals("stop")){
                break;
            }else{
                clauses.add(tmp);
            }
        }

        //Create a list of clauses stored with their elements attached to a true or false value within an array
        ArrayList<Clause[]> newClauses = new ArrayList<Clause[]>();
        for(int i = 0; i < clauses.size(); i++){
            List<Clause> temp = new ArrayList<Clause>();
            for(int j = 0; j < clauses.get(i).length(); j++){
                char tmp = clauses.get(i).charAt(j);
                if(tmp == ' ' || tmp == 'v'){
                    continue;
                }else{
                    if(tmp == 170){
                       temp.add(new Clause(false, Character.toString(clauses.get(i).charAt(j+2))));
                        j += 2;
                    }else{
                        temp.add(new Clause(true, Character.toString(clauses.get(i).charAt(j))));
                    }
                }
            }
            newClauses.add(temp.toArray(new Clause[0]));
        }

        //Resolve
        while(!foundResolution){
            ArrayList<Clause[]> resolvents = new ArrayList<Clause[]>();
            for(int i = 0; i < newClauses.size(); i++){
                for(int j = 0; j < newClauses.size(); j++){
                    if(i < j){
                        resolvents.add(PL_Resolve(newClauses.get(i), newClauses.get(j)).toArray(new Clause[0]));
                    }
                }
            }

            newClauses.addAll(resolvents);
        }
        
        for(int i = 0; i < newClauses.size(); i++){
            for(int j = 0; j < newClauses.get(i).length; j++){
                System.out.println(newClauses.get(i)[j].element);
            }
            System.out.println();
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
            if(!c1Index.contains(i) && !c1Index.contains(c1[i])){
                tmp.add(c1[i]);
            }
        }
        for(int i = 0; i < c2.length; i++){
            if(!c2Index.contains(i) && !c1Index.contains(c2[i])){
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