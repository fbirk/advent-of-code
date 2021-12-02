import java.io.File;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.Stack;

class day01 {

    public static ArrayList<Integer> fileReader(){
        ArrayList<Integer> values = new ArrayList<Integer>();
        try{
            var file = new File("data01.txt");
            var scanner = new Scanner(file);
            while(scanner.hasNextLine()){
                int data = Integer.parseInt(scanner.nextLine().trim());
                values.add(data);
            }
            scanner.close();
        } catch(Exception e){
            e.printStackTrace();
        }
        return values;
    }

    public static ArrayList<Integer> preProcessing(ArrayList<Integer> values) {
        var data = new ArrayList<Integer>();
        var a = new Stack<Integer>();
        var b = new Stack<Integer>();
        var c = new Stack<Integer>();

        for (int i = 0; i < values.size(); i++) {
            if (i == 0) {
                a.push(values.get(i));
            }
            else if (i == 1) {
                a.push(values.get(i));
                b.push(values.get(i));
            }
            else {
                a.push(values.get(i));
                b.push(values.get(i));
                c.push(values.get(i));
            }

            if (a.size() >= 3) {
                data.add(accumulateValues(a));
                a.clear();
            }
            if (b.size() >= 3) {
                data.add(accumulateValues(b));
                b.clear();
            }
            if (c.size() >= 3) {
                data.add(accumulateValues(c));
                c.clear();
            }
        }
        return data;
    }

    public static Integer accumulateValues(Stack<Integer> stack) {
        var sum = 0;
        for (Integer i : stack) {
            sum += i;
        }
        return sum;
    }

    public static void main(String[] args) {
        var values = fileReader();
        var data = preProcessing(values);
        int counter = 0;
        for(int i=1; i<data.size(); i++){
            if (data.get(i) > data.get(i-1)){
                counter++;
            }
        }

        System.out.println(counter);
    }
}