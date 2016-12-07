package bayes;

import java.util.Arrays;

public class CancerData {
	public Integer cancerClass;
	public Integer[] attributes;

	public CancerData(Integer cancerClass, String[] attributes) {
		this.cancerClass = cancerClass;
		Integer[] convertedAttributes = new Integer[56];
		for (Integer i = 0; i < attributes.length; ++i) {
			try{
				convertedAttributes[i] = Integer.parseInt(attributes[i]);
			} catch (NumberFormatException e){
				convertedAttributes[i] = -1;
			}
		}
		this.attributes = convertedAttributes;
	}
	public String toString(){
		String result = "";
		result="Class: "+ this.cancerClass.toString()+"\n Measures: "+Arrays.toString(attributes)+"\n measures.length: "+String.valueOf(attributes.length);
		return result;
	}
}
