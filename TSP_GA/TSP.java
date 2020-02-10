import java.util.*;

public class TSP{
    /*
    Step 1. Create an initial population of P chromosomes (generation 0) --done

    Step 2. Evaluate the fitness of each chromosome. --done

    Step 3. Choose P parents from the current population via proportional selection (i.e. selection probability is proportional to the fitness function) --done

    Step 4. Randomly select two parents to create offspring using crossover operator. --done

    Step 5. Apply mutation operators for minor changes in the results. --done

    Step 6. Repeat Steps  4 and 5 until all parents are selected and mated. --done

    Step 7. Replace old population of chromosomes with new one. --done

    Step 8. Evaluate the fitness of each chromosome in the new population.

    Step 9. Terminate if the number of generations meets some upper bound; otherwise go to Step  3.
    */

    public static double sumOfDistance = 0.0;
    public static double tmpSum = 0;
    public static void main(String [] args){
        Scanner sc = new Scanner(System.in);

        //Retrieve number of cities from user
        System.out.print("How many cities: ");
        int cities = sc.nextInt();
        sc.nextLine();

        //Get the distance array from user
        System.out.print("City distance array: ");
        int [][] distanceArray = new int[cities][cities];
        for(int i = 0; i < cities; i++){
            for(int j = 0; j < cities; j++){
                distanceArray[i][j] = sc.nextInt();
            }
        }

        //Create a basic order
        int [] order = new int [cities];
        for(int i = 0; i < order.length; i++){
            order[i] = i;
        }

        //Step 1: Create starting population
        List<int[]> population = new ArrayList<int[]>();
        createPopulation(order, population);

        //Step 2: Get fitness, and start looping through new generations
        int count = 0;
        while(count < 100){
            double [] fitness = new double[population.size()];
            getFitness(fitness, population, distanceArray);
            
            //Step 3: Proportional selection
            List<int[]> parents = getNewParents((int)Math.ceil(cities/2), population);
            int t1 = proportionalSelection(fitness);
            int [] newParent1 = population.get(t1);
            parents.add(newParent1);

            int t2 = proportionalSelection(fitness);
            while(t2 == t1){
                t2 = proportionalSelection(fitness);
            }
            int [] newParent2 = population.get(t2);
            parents.add(newParent2);

            int t3 =proportionalSelection(fitness);
            while(t3 == t2 || t3 == t1){
                t3 =proportionalSelection(fitness);
            }
            int [] newParent3 = population.get(t3);
            parents.add(newParent3);

            int t4 = proportionalSelection(fitness);
            while(t4 == t3 || t4 == t2 || t4 == t1){
                t4 = proportionalSelection(fitness);
            }
            int [] newParent4 = population.get(t4);
            parents.add(newParent4);

            //Step 4: Choose two random parents and apply crossover
            Random rand = new Random();
            List<int[]> prevMated = new ArrayList<int[]>();
            population.clear();
            while(prevMated.size() < Math.ceil(cities/2)){
                int sum = 0;
                for(int i = 0; i < Math.ceil(cities/2); i++){
                    sum += i+1;
                }
                int p1 = rand.nextInt(sum);
                int p2 = rand.nextInt(sum);
                while(p2 == p1){
                    p2 = rand.nextInt(sum);
                }
                int [] tmp = {p1, p2};
                int [] tmp2 = {p2, p1};
                while(isInList(prevMated, tmp) || isInList(prevMated, tmp2)){
                    p1 = rand.nextInt(sum);
                    p2 = rand.nextInt(sum);
                    while(p2 == p1){
                        p2 = rand.nextInt(sum);
                    }
                    tmp[0] = p1;
                    tmp[1] = p2;
                    tmp2[0] = p2;
                    tmp2[1] = p1;
                }
                prevMated.add(tmp);
                int[] newOrder = crossOver(parents.get(p1), parents.get(p2));

                //Step 5: Mutate
                int [] newPop = mutate(newOrder, 0.2, cities);
                population.add(newPop);
            }
            count++;
            System.out.println(Arrays.toString(fitness));
        }
        double [] fitness = new double[population.size()];
        getFitness(fitness, population, distanceArray);
        double max = -9999999;
        int index = 0;
        for(int i = 0; i < population.size(); i++){
            if(fitness[i] > max){
                max = fitness[i];
                index = i;
            }
        }
        System.out.println(Arrays.toString(population.get(index)));
    }   

    public static List<int[]> createPopulation(int [] order, List<int[]> population){
        for(int i = 0; i < order.length; i++){
            RandomizeArray(order);
            population.add(order.clone());   
        }
        return population;
    }

    public static int[] RandomizeArray(int[] array){
		Random rand = new Random();
		for (int i=0; i<array.length; i++) {
		    int randomPosition = rand.nextInt(array.length);
		    int tmp = array[i];
		    array[i] = array[randomPosition];
		    array[randomPosition] = tmp;
		}
		return array;
    }

    public static int getDistance(List<int[]> population, int [][] distance, int i){
        int sum = 0;
        int [] tmp = population.get(i);
        for(int j = 0; j < tmp.length; j++){
            if(j == 0){
                continue;
            }else{
                sum += distance[tmp[j-1]][tmp[j]];
            }    
        }
        return sum;
    }

    public static double [] getFitness(double [] fitness, List<int[]> population, int [][] distance){
        //Get distances for orders in population
        double tmp = 0;
        sumOfDistance = 0;
        for(int i = 0; i < fitness.length; i++){
            tmp = getDistance(population, distance, i);
            sumOfDistance += tmp;
            fitness[i] = tmp;
        }
        for(int i = 0; i < fitness.length; i++){
            fitness[i] = fitness[i] / sumOfDistance;
            tmpSum += fitness[i];
        }
        return fitness;
    }

    public static List<int[]> getNewParents(int numOfParents, List<int[]> population, double [] fitness){
        List<int[]> parents = new ArrayList<int[]>();
        int count = 0;
        while(count < numOfParents){
            int tmp = proportionalSelection(fitness);
            if(!isInList(parents, population.get(tmp))){
                parents.add(population.get(tmp));
                count++;
            }
        }
        return parents;
    }

    public static int proportionalSelection(double [] fitness){
        Random rand = new Random();
        double tmp = rand.nextDouble();
        int count = 0;
        while(tmp >= 0){
            tmp = tmp - fitness[count];
            count++;
        }
        count--;
        return count;
    }

    public static int[] crossOver(int[] p1, int[]p2){
        Random rand = new Random();
        int start1 = rand.nextInt(Math.floorDiv(p1.length, 2) + 1);
        int end1 = rand.nextInt((p1.length + 1 - start1)) + start1;
        int[]tmpArray = new int[end1 - start1];
        int count = 0;
        for(int i = start1; i < end1; i++){
            tmpArray[count] = p1[i];
            count++;
        }
        for(int i = 0; i < tmpArray.length; i++){
            for(int j = 0; j < p2.length; j++){
                if(tmpArray[i] == p2[j]){
                    int tmp = p2[j];
                    p2[j] = p2[i];
                    p2[i] = tmp;
                }
            }
        }
        return p2;
    }

    public static int[] mutate(int[] order, double mutationRate, int cities){
        Random rand = new Random();
        for(int i = 0; i < cities; i++){
            if(rand.nextDouble() < mutationRate){
                int index1 = rand.nextInt(order.length);
                int index2 = rand.nextInt(order.length);
                int tmp = order[index1];
                order[index1] = order[index2];
                order[index2] = tmp;
            }
        }
        return order;
    }

    public static boolean isInList(
        final List<int[]> list, final int[] candidate) {
        return list.stream().anyMatch(a -> Arrays.equals(a, candidate));
    }
}