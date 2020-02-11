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

    Step 8. Evaluate the fitness of each chromosome in the new population. --done

    Step 9. Terminate if the number of generations meets some upper bound; otherwise go to Step  3. --done
    */

    public static double sumOfDistance = 0.0;
    public static double tmpSum = 0.0;
    public static void main(String [] args){
        Scanner sc = new Scanner(System.in);

        //Retrieve number of cities from user
        System.out.print("How many cities: ");
        int cities = sc.nextInt();
        sc.nextLine();

        //Get the distance array from user
        System.out.println("City distance array: ");
        double [][] distanceArray = new double[cities][cities];
        for(int i = 0; i < cities; i++){
            for(int j = 0; j < cities; j++){
                distanceArray[i][j] = sc.nextDouble();
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
        for(int i = 0; i < population.size(); i++){
            System.out.print(getDistance(population, distanceArray, i) + " : ");
        }

        //Step 2: Get fitness, and start looping through new generations
        double [] fitness = getFitness(population, distanceArray);
        System.out.println(Arrays.toString(fitness) + "\n");
        boolean fitnessConverge = true;
        int count = 0;
        while(fitnessConverge){
            //Step 3: Proportional selection
            List<int[]> parents = getNewParents(population.size(), population, fitness);

            //Step 4/6: Choose two random parents and apply crossover
            Random rand = new Random();
            List<int[]> prevMated = new ArrayList<int[]>();
            List<int[]> tmpPop = new ArrayList<int[]>();
            while(prevMated.size() < parents.size()){
                int p1 = rand.nextInt(parents.size());
                int p2 = rand.nextInt(parents.size());
                while(p2 == p1){
                    p2 = rand.nextInt(parents.size());
                }
                int [] tmp = {p1, p2};
                int [] tmp2 = {p2, p1};
                while(isInList(prevMated, tmp) || isInList(prevMated, tmp2)){
                    p1 = rand.nextInt(parents.size());
                    p2 = rand.nextInt(parents.size());
                    while(p2 == p1){
                        p2 = rand.nextInt(parents.size());
                    }
                    tmp[0] = p1;
                    tmp[1] = p2;
                    tmp2[0] = p2;
                    tmp2[1] = p1;
                }

                //Adding the new mated pair to be compared later
                prevMated.add(tmp);

                //Applying crossover
                int[] newOrder = crossOver(parents.get(p1), parents.get(p2));

                //Step 5: Mutate
                int [] newPop = mutate(newOrder, 0.5, cities);

                //Adding current child into the new population
                tmpPop.add(newPop);
            }
            //Step 7: Replace old population with new population
            population = tmpPop;
            for(int i = 0; i < population.size(); i++){
                System.out.print(getDistance(population, distanceArray, i) + " : ");
            }

            //Step 8: Calculate new fitness
            fitness = getFitness(population, distanceArray);
            System.out.println(Arrays.toString(fitness));

            //Step 9: Check to see if upper bounds has been met
            for(int i = 0; i < fitness.length; i++){
                if(fitness[0] != fitness[i]){
                    break;
                }else if(i == fitness.length-1){
                    fitnessConverge = false;
                }
            }
            count++; 
        }
        double max = -9999999;
        int index = 0;
        for(int i = 0; i < population.size(); i++){
            double tmp = getDistance(population, distanceArray, i);
            if(tmp > max){
                max = tmp;
                index = i;
            }
        }
        System.out.println(Arrays.toString(population.get(index)));
        System.out.println(getDistance(population, distanceArray, index));
    }   

    public static List<int[]> createPopulation(int [] order, List<int[]> population){
        for(int i = 0; i < order.length; i++){
            int[]tmp  = RandomizeArray(order);
            while(isInList(population, tmp)){
                tmp = RandomizeArray(order);
            }
            population.add(tmp.clone());   
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
        System.out.println(Arrays.toString(array) + "\n");
		return array;
    }

    public static double getDistance(List<int[]> population, double [][] distance, int i){
        double sum = 0;
        int [] tmp = population.get(i);
        for(int j = 0; j < tmp.length; j++){
            if(j == 0){
                continue;
            }else{
                sum += distance[tmp[j-1]][tmp[j]];
            }    
        }
        return sum + distance[tmp[0]][tmp[tmp.length-1]];
    }

    public static double [] getFitness(List<int[]> population, double [][] distance){
        //Get distances for orders in population
        double tmp = 0;
        double []tmpFitness = new double[population.size()];
        sumOfDistance = 0.0;
        for(int i = 0; i < population.size(); i++){
            tmp = getDistance(population, distance, i);
            sumOfDistance += tmp;
            tmpFitness[i] = tmp;
        }
        for(int i = 0; i < tmpFitness.length; i++){
            tmpFitness[i] = 1 - (tmpFitness[i] / sumOfDistance);
            tmpSum += tmpFitness[i];
        }
        return tmpFitness;
    }

    public static List<int[]> getNewParents(int numOfParents, List<int[]> population, double [] fitness){
        List<int[]> parents = new ArrayList<>();
        for(int i = 0; i < numOfParents; i++){
            parents.add(population.get(proportionalSelection(fitness)));
        }
        return parents;
    }

    public static int proportionalSelection(double [] fitness){
        Random rand = new Random();
        double tmp = rand.nextDouble();
        int count = 0;  
        int sum = 0;
        double [] cubeArray = new double[fitness.length];
        for(int i = 0; i <  fitness.length; i++){
            cubeArray[i] = Math.pow(fitness[i], 3);
            sum += cubeArray[i];
        }
        tmp *= sum;
        while(tmp >= 0){
            tmp = tmp - cubeArray[count];
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