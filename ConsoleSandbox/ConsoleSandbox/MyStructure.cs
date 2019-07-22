using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class MyStructure
    {
        public static void TestMatrix()
        {
            Stopwatch st = new Stopwatch();
            StreamWriter sw = new StreamWriter(@"E:\timer.txt");
            for (int i = 5; i <= 300; i++)
            {
                st.Start();
                Matrix.Test(i);
                st.Stop();
                sw.WriteLine(i + " " + st.Elapsed.ToString("ssfffff"));
                Console.WriteLine(i);
            }
            Console.WriteLine("end");
        }



        public static void MakeNN()
        {
            Matrix inputM = new Matrix(2, 1);
            Matrix transM = new Matrix(1, 2);
            Matrix outputM = new Matrix(1, 1);
            Matrix outputM_s = new Matrix(1, 1);

            Random rand = new Random();
            transM.Operate((double input) => { return rand.NextDouble() * 2 - 1; });
            StreamReader sr = new StreamReader(@"E:/test.txt");
            List<Example> allexamples = new List<Example>();
            while (!sr.EndOfStream)
            {
                string[] r = sr.ReadLine().Split(' ');
                double[] d = new double[2];
                d[0] = Convert.ToDouble(r[0]);
                d[1] = Convert.ToDouble(r[1]);
                allexamples.Add(new Example(2, d, r[2].Equals("1") ? true : false));
            }
            int exampleCount = allexamples.Count * 3 / 4;
            List<Example> tExamples = allexamples.GetRange(0, exampleCount);
            List<Example> vExamples = allexamples.GetRange(exampleCount, allexamples.Count - exampleCount);

            foreach(Example trainEx in tExamples)
            {
                inputM.setValue(0, 0, trainEx.inputs[0]);
                inputM.setValue(1, 0, trainEx.inputs[1]);
                outputM = transM.Multiply(inputM);
                outputM.Operate(Sigmoid);
                Console.WriteLine(outputM.data[0, 0]);
            }

        }

        private static double Sigmoid(double d)
        {
            if(d > 10)
            {
                return 1;
            }
            else if(d < -10)
            {
                return 0;
            }
            return 1 / (1 + Math.Exp(-d));
        }

        private class Example
        {
            public double[] inputs;
            public bool value;
            public Example(int icount,  double[] inputs, bool value)
            {
                this.value = value;
                this.inputs = new double[icount];
                for(int i = 0; i < icount; i++)
                {
                    this.inputs[i] = inputs[i];
                }
            }
        }


    }
}
