package bayes;

import java.io.IOException;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class BayesNetworkCancerDetection {
	public static void main(String[] args) {
		ArrayList<CancerData> data = null;
		Path path = FileSystems.getDefault().getPath("data", "lung-cancer.data.txt");
		try {
			data = (ArrayList<CancerData>) getDataFromFile(path);
		} catch (IOException e) {
			e.printStackTrace();
		}
		System.out.print(data.get(1).toString());
	}
	
	public static List<CancerData> getDataFromFile(Path path) throws IOException{
		List<CancerData> data = new ArrayList<CancerData>();
		List<String> file = new ArrayList<String>();

		file = Files.readAllLines(path);
		for(String s :file){
			if(!s.isEmpty()){
				String [] splitted = s.split(",");
				Integer type = new Integer(splitted[0]);
				splitted = Arrays.copyOfRange(splitted, 1, splitted.length);
				CancerData personData = new CancerData(type,splitted);
				data.add(personData);
			}
		}
		return data;
	}
}
