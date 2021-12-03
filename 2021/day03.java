import java.io.File;
import java.util.ArrayList;
import java.util.Scanner;

class day03 {

    public static Integer fileReader(){
        int[] values = {0,0,0,0,0,0,0,0,0,0,0,0};
        int inputSize = 0;
        try{
            var file = new File("data03.txt");
            var scanner = new Scanner(file);
            while(scanner.hasNextLine()) {
                inputSize++;

                var bitInput = scanner.nextLine().trim();
                var chars = bitInput.toCharArray();

                for (int i = 0; i < chars.length; i++) {
                    if (chars[i] == '1') {
                        values[i] += 1;
                    }
                }
            }

            scanner.close();
        } catch(Exception e){
            e.printStackTrace();
        }

        // count 1s and 0s
        String gamma = "";

        for (int i = 0; i < values.length; i++) {
            if (values[i] > inputSize/2) {
                gamma += "1";
            } else {
                gamma += "0";
            }
        }

        var gammaBinary = Integer.parseInt(gamma, 2);

        // epsilon is inverted value of gamma
        // XOR with bitmask 11111 to invert values
        var epsilonBin = 0b111111111111 ^ gammaBinary;

        return gammaBinary * epsilonBin;
    }

    public static Integer PartTwo() {
        var bitstringsO = new ArrayList<String>();
        var bitstringsCO = new ArrayList<String>();

        try {
            var file = new File("data03.txt");
            var scanner = new Scanner(file);
            while (scanner.hasNextLine()) {

                var bitInput = scanner.nextLine().trim();
                bitstringsO.add(bitInput);
                bitstringsCO.add(bitInput);
            }
            scanner.close();

        } catch (Exception e) {
            e.printStackTrace();
        }

        var oxygenRating = CalcOxygenRating(bitstringsO);
        var coRating = CalcCORating(bitstringsCO);

        System.out.println("\n" + oxygenRating);
        System.out.println(coRating);

        return oxygenRating * coRating;
    }

    private static int CountOnes(ArrayList<String> bitstrings, int position) {
        int counter = 0;
        for (int i = 0; i < bitstrings.size(); i++) {
            if (bitstrings.get(i).toCharArray()[position] == '1') {
                counter++;
            }
        }
        return counter;
    }

    private static Integer CalcCORating(ArrayList<String> bitstrings) {
        var size = bitstrings.get(0).length();

        for (int index = 0; index < size; index++) {
            var bitstringsNew = new ArrayList<String>();

            char comparer;
            var values = CountOnes(bitstrings, index);

            int half = (int) Math.ceil((double)bitstrings.size() / 2);
            if (values < half) {
                comparer = '1';
            } else {
                comparer = '0';
            }

            for (int i = 0; i < bitstrings.size(); i++) {
                var elem = bitstrings.get(i);
                if (elem.toCharArray()[index] == comparer) {
                    bitstringsNew.add(elem);
                }

            }

            bitstrings = bitstringsNew;
            if(bitstrings.size() == 1) {
                return Integer.parseInt(bitstrings.get(0), 2);
            }
        }

        return null;
    }

    private static Integer CalcOxygenRating(ArrayList<String> bitstrings) {
        var size = bitstrings.get(0).length();

        for (int index = 0; index < size; index++) {
            var bitstringsNew = new ArrayList<String>();

            char comparer;
            var values = CountOnes(bitstrings, index);

            int half = (int) Math.ceil((double)bitstrings.size() / 2);

            if (values >= half) {
                comparer = '1';
            } else {
                comparer = '0';
            }

            for (int i = 0; i < bitstrings.size(); i++) {
                var elem = bitstrings.get(i);
                if (elem.toCharArray()[index] == comparer) {
                    bitstringsNew.add(elem);
                }
            }

            bitstrings = bitstringsNew;


            if(bitstrings.size() == 1) {
                return Integer.parseInt(bitstrings.get(0), 2);
            }
        }

        return null;
    }

    public static void main(String[] args) {
        //var values = fileReader();
        var values = PartTwo();

        System.out.println(values);
    }
}