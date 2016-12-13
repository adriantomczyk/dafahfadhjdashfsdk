using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smile;

namespace LungCancerBayesNetwork
{
    public class SimulatedAnnealing
    {
        private Network Network { get; set; }
        private List<CancerData> LearningData { get; set; }
        private List<CancerData> TestData { get; set; }

        // private variables
        private double StartTemperature { get; set; } // poczatkowa temperatura 
        private double EndTemperature { get; set; } // koncowa temperatra
        private double CurrentTemperature { get; set; } // aktualna temperatura
        private List<int> BestSolution { get; set; } // najlepsza znaleziona permutacja ze wszystkich
        private List<int> CurrentBestPermutation { get; set; } // aktualnie najlepsza (S*)
        private List<int> CurrentPermutation { get; set; } // aktualnie sprawdzana permutacja (S')
        private int ValueOfBestSolution { get; set; } // wartosc najlepszej permutacji, zapisujemy aby uniknac niepotrzebnych wyliczen
        private List<List<int>> MatrixOfCites { get; set; } // macierz miast
        private int CityCount { get; set; } // ilosc miast

        public SimulatedAnnealing(double startTemp, double endTemp, List<CancerData> learningData, List<CancerData> testData)
        {
            this.StartTemperature = startTemp;
            this.EndTemperature = endTemp;
            this.LearningData = learningData;
            this.TestData = testData;
            this.ValueOfBestSolution = -1;
            this.CurrentTemperature = -1;
            this.CityCount = 0;
        }

        public void RandomSwap(List<int> permutation)
        {
	        // losujemy dwie liczby, ktore sa od siebie rozne 
	        // i na ich podstawie dokonujemy zamiany miejsc
            Random rand = new Random();
	        int idx1 = 0;
	        int idx2 = 0;
	        while (idx1 == idx2)
	        {
		        idx1 = rand.Next(0, this.CityCount-1);
		        idx2 = rand.Next(0, this.CityCount-1);
	        }

	        int tmpValue = permutation[idx1];
	        permutation[idx1] = permutation[idx2];
	        permutation[idx2] = tmpValue;
        }

        public void CalculateNewTemperature(double constant) 
        { 
            this.CurrentTemperature *= constant; 
        }

        public double CalculateProbability(int bestPathLength, int currentlyCheckedPathLength, double currentTemperature)
        {
	        double delta = currentlyCheckedPathLength - bestPathLength;
            double probability = Math.Pow(Math.E, -(delta / currentTemperature));
	        return probability;
        }

        public List<int> GetInitialPermutation()
        {
            List<int> permutation = new List<int>();

	        for (int i = 0; i < this.CityCount; i++)
	        {
                permutation.Add(i);
	        }

            return permutation;
        }

        public int CalculatePathLength(List<int> permutation)
        {
	        int value = 0;
	        for (int i = 0; i < this.CityCount-1; i++)
	        {
		        // wyliczanie dlugosci sciezki z miasta do nastepnego miasta
		        value += this.MatrixOfCites[permutation[i]][permutation[i + 1]];
	        }
	        //powrot do poczatkowego miasta
	        value += this.MatrixOfCites[permutation[this.CityCount-1]][permutation[0]]; 
	        return value;
        }

        public void PrintSolution(List<int> solution)
        {
	        int bestVal = this.CalculatePathLength(solution);
	        for (int i = 0; i < this.CityCount-1; i++)
	        {
                Console.WriteLine("Z miasta nr " + solution[i]+1 + " do miasta nr " + solution[i + 1]+1);
	        }
	        Console.WriteLine("Z miasta nr " + solution[this.CityCount - 1]+1 + " do miasta nr " + solution[0]+1);
	        Console.WriteLine("obliczona droga wynosi " + bestVal);
        }

        public void ExecuteAlgorithm(double c)
        {
            Random rand = new Random();
	        int newPermutationPathLen = 0; // droga "sasiada"
	        int bestLen = 0; // aktualnie najkrotsza droga
            double x = 0; // 
            double probabilityValue = 0; // wartosc funkcji prawdopodobienstwa

            this.CurrentBestPermutation = this.GetInitialPermutation(); // permutacja poczatkowa uznawana jest za najlepsza
            this.BestSolution = this.CurrentBestPermutation.Clone(); // kopiujemy ja do tablicy przechowujacej rozwiazanie najlepsze z najlepszych
	        this.ValueOfBestSolution = this.CalculatePathLength(this.BestSolution); // zapisanie dlugosci sciezki najlepszego rozwiazania, zeby nie wyliczac tego bez potrzeby
	        this.CurrentTemperature = this.StartTemperature; // T0 = Ts

	        bestLen = this.ValueOfBestSolution;

	        while (this.CurrentTemperature > this.EndTemperature) // dopoki temperatura nie spadnie ponizej koncowej
	        {
                this.CurrentPermutation = this.CurrentBestPermutation.Clone(); // skopiowanie aktualnie najlepszej permutacji do tablicy przechowujacej permutacje ktora bedziemy teraz sprawdzac
		        this.RandomSwap(this.CurrentPermutation); // "tworzenie permutacji" poprzez swap
                newPermutationPathLen = this.CalculatePathLength(this.CurrentPermutation); // wyliczenie drogi nowej permutacji
		        if (newPermutationPathLen < bestLen)
		        {
			        bestLen = newPermutationPathLen;
                    this.CurrentBestPermutation = this.CurrentPermutation.Clone();
		        }
		        else
		        {
			        x = rand.NextDouble(); //wylosowanie liczby z zakresu (0,1)
			        probabilityValue = this.CalculateProbability(bestLen, newPermutationPathLen, this.CurrentTemperature); // wyliczenie prawdopodobienstwa
			        if (x < probabilityValue)
			        {
				        bestLen = newPermutationPathLen;
                        this.CurrentBestPermutation = this.CurrentPermutation.Clone(); // zapisanie permutacji jako aktualnie najlepszej
			        }
		        }
		        if (bestLen < this.ValueOfBestSolution) // jesli droga jest lepsza niz dotychczas znaleziona
		        {
			        this.ValueOfBestSolution = bestLen;
                    this.BestSolution = this.CurrentBestPermutation.Clone(); // zapisanie permutacji jako dotychczas najlepszej
		        }
		        this.CalculateNewTemperature(c); // wyliczenie nowej temperatury
	        }

	        this.PrintSolution(this.BestSolution);
        }
    }
}
