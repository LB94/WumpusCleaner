using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WumpusCounter
{
    class Counter
    {
        private List<string> data = new List<string>();
        private List<string[,]> resultWithCount = new List<string[,]>();
        private List<string> dataWithoutLast = new List<string>();
        private List<string> goodData = new List<string>();
        string[,] lineWithCount;
        private int? count;
        private int? countTwo;
        private string e;
        private string localPath = @"C:\Users\ludau\Documents\goodData";

        internal void readFile(string path)
        {
            int counter = 0;
            int countGoodLines = 0;
            string line; 
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                numberSameLine(line, path);
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

            countLiveAndDie();

            int cpt = 0;
            while (File.Exists(localPath + cpt +".txt"))
            {
                cpt++;
            }
            using (System.IO.StreamWriter write_file =
            new System.IO.StreamWriter(localPath + cpt +".txt"))
            {
                foreach (string row in goodData)
                {
                    write_file.WriteLine(row);
                }
            }
            foreach (string row in goodData)
            {
                Console.WriteLine(row);
                countGoodLines++;
            }
            Console.WriteLine("Il y a {0} lignes", countGoodLines);
            System.Console.ReadLine();
        }

        internal void numberSameLine(string line, string path)
        {
            int counter = 0;
            string row;
            //si la liste data a déjà la même ligne
            if (data.Contains(line))
                return;
            //sinon il l'ajoute
            else
            {
                data.Add(line);
                goodData.Add(line);
                //appel de la méthode pour ajouter la ligne en virant les deux derniers caractères
                addLineWithoutLast(line);
                System.IO.StreamReader file =
                    new System.IO.StreamReader(path);
                while ((row = file.ReadLine()) != null)
                {
                    //on reparcourt le fichier pour incrémenter le compteur si deux lignes sont identiques, ça permet de compter le nombre 
                    //d'apparition d'une même ligne
                    if (row == line)
                    {
                        counter++;
                    }      
                }
                if (counter != 0)
                {
                    //on ajoute dans un tableau la ligne avec le nombre de fois qu'elle apparait dans le fichier
                    lineWithCount = new string[1, 2] { { line, counter.ToString() } };
                    resultWithCount.Add(lineWithCount);
                }
            }
            file.Close();
        }

        internal void addLineWithoutLast(string line)
        {
            //vire les deux derniers caractères d'une ligne
            string lineWithoutLast = line.Remove(line.Length - 1, 1);
            lineWithoutLast = lineWithoutLast.TrimEnd(',');
            dataWithoutLast.Add(lineWithoutLast);
        }

        internal void countLiveAndDie ()
        {

            //on va parcourir deux fois la liste dataWithoutLast pour comparer les lignes
            //si une ligne est dans cette liste, c'est qu'elle y est une fois ou deux fois, soit vivant soit mort soit vivant ET mort
            for (int i = 0; i < dataWithoutLast.Count(); i++)
            {
                for (int j = i + 1; j < dataWithoutLast.Count(); j++)
                {
                    if (dataWithoutLast[i] == dataWithoutLast[j])
                    {
                        //récupère les index des deux éléments dans le tableau de données complet
                        string element = data[i];
                        string elementTwo = data[j];

                        foreach (string[,] elem in resultWithCount)
                        {
                            foreach (string value in elem)
                            {
                                if (value == element)
                                {
                                    count = Convert.ToInt32(elem[0, 1]);
                                    e = value;
                                }
                                if (value == elementTwo)
                                {
                                    countTwo = Convert.ToInt32(elem[0, 1]);
                                }
                                if (count != null && countTwo != null)
                                {
                                    if (count > countTwo)
                                    {
                                        goodData.Remove(value);
                                    }
                                    else if (countTwo > count)
                                    {
                                        goodData.Remove(e);
                                    }
                                    else { }
                                    count = null;
                                    countTwo = null;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
