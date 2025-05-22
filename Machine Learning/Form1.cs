using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Machine_Learning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static List<List<int>> allList= new List<List<int>>();
        public static int numberOfLoops = 1000;
        public static int eachInd = 10;
        public static int bestOf = 200;
        public static int generations = 100000;

        private void Form1_Load(object sender, EventArgs e)
        {
            setList();
            makeList();
        }

        private void setList() {
            for (int i = 0; i < numberOfLoops; i++) {
                allList.Add(new List<int> {});
                for (int b = 0; b < eachInd; b++) {
                    allList[i].Add(0);
                }
            }
        }

        private void makeList() {
            for (int i = 0; i < generations; i++) {
                loop();
                getResults();
                Debug.Write("       |  Generation: " + (i + 1) + "\n");
            }
        }

        private void loop() {
            List<List<int>> maxValuesList = new List<List<int>>();

            int[] numberArray = new int[numberOfLoops];
            int[] sortArray = new int[numberOfLoops];
            for (int i = 0; i < numberOfLoops; i++)
            {
                int number = 0;
                for (int b = 0; b < eachInd; b++)
                {
                    number += allList[i][b];
                }
                numberArray[i] = number;
                sortArray[i] = number;
            }

            Array.Sort<int>(sortArray);
            Array.Reverse(sortArray);

            for (int i = 0; i < bestOf; i++) {
                maxValuesList.Add(new List<int> {});

                for (int b = 0; b < eachInd; b++) {
                    maxValuesList[i].Add(allList[Array.IndexOf(numberArray, sortArray[i])][b]);
                }
            }

            //COPY
            for (int i = 0; i < Math.Ceiling(Double.Parse(numberOfLoops.ToString()) / Double.Parse(bestOf.ToString())); i++) {
                for (int b = 0; b < bestOf; b++) {
                    int getIndex = (i * bestOf) + b;
                    allList[getIndex] = maxValuesList[i];
                }
            }


            Random random = new Random();
            for (int i = 0; i < numberOfLoops; i++) {
                for (int b = 0; b < eachInd; b++) {
                    int number = random.Next(2) == 0 ? -1 : 1;

                    allList[i][b] += number;
                }
            }
        }

        private void getResults() {
            int[] numberArray = new int[numberOfLoops];

            double maxNumber = 0;

            for (int i = 0; i < numberOfLoops; i++)
            {
                int number = 0;
                for (int b = 0; b < eachInd; b++)
                {
                    number += allList[i][b];
                }
                numberArray[i] = number;
                maxNumber += number;
            }

            Debug.Write("Best: " + numberArray.Max() + "   Worst: " + numberArray.Min() + "   Average: " + maxNumber / numberOfLoops);
        }
    }
}
