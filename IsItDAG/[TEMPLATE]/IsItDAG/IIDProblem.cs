using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using GraphGenerator;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "IsItDAG"; } }

        public override void TryMyCode()
        {
            bool output, expected;
            
            //Case1
            string[] vertices1 = { "A1", "A2", "A3"};
            KeyValuePair<string, string>[] edges1 = new KeyValuePair<string, string>[2];
            edges1[0] = new KeyValuePair<string, string>("A1", "A2");
            edges1[1] = new KeyValuePair<string, string>("A2", "A3");
            expected = true;
            output = IsItDAG.IsDAG(vertices1, edges1);
            PrintCase(vertices1, edges1, output, expected);

            //Case2
            string[] vertices2 = { "A", "B", "C", "D", "E", "F", "G" };
            KeyValuePair<string, string>[] edges2 = new KeyValuePair<string, string>[8];
            edges2[0] = new KeyValuePair<string, string>("A", "B");
            edges2[1] = new KeyValuePair<string, string>("B", "C");
            edges2[2] = new KeyValuePair<string, string>("B", "D");
            edges2[3] = new KeyValuePair<string, string>("B", "E");
            edges2[4] = new KeyValuePair<string, string>("C", "E");
            edges2[5] = new KeyValuePair<string, string>("D", "E");
            edges2[6] = new KeyValuePair<string, string>("E", "F");
            edges2[7] = new KeyValuePair<string, string>("G", "D");
            expected = true;
            output = IsItDAG.IsDAG(vertices2, edges2);
            PrintCase(vertices1, edges1, output, expected);

            //Case3
            string[] vertices3 = { "A", "B", "C", "D", "E", "F"};
            KeyValuePair<string, string>[] edges3 = new KeyValuePair<string, string>[6];
            edges3[0] = new KeyValuePair<string, string>("A", "B");
            edges3[1] = new KeyValuePair<string, string>("B", "C");
            edges3[2] = new KeyValuePair<string, string>("E", "D");
            edges3[3] = new KeyValuePair<string, string>("E", "F");
            edges3[4] = new KeyValuePair<string, string>("C", "E");
            edges3[5] = new KeyValuePair<string, string>("D", "B");
            expected = false;
            output = IsItDAG.IsDAG(vertices3, edges3);
            PrintCase(vertices1, edges1, output, expected);

        }

        

        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            bool output, actualResult;

            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();
            testCases = int.Parse(line);
   
            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;
 
            int i = 1;
            while (testCases-- > 0)
            {
                line = sr.ReadLine();
                int v = int.Parse(line);
                line = sr.ReadLine();
                int e = int.Parse(line);
                line = sr.ReadLine();
                string[] vertices = line.Split(',');
                var edges = new KeyValuePair<string, string>[e];
                for (int j = 0; j < e; j++)
                {
                    line = sr.ReadLine();
                    string[] lineParts = line.Split(',');
                    edges[j] = new KeyValuePair<string, string>(lineParts[0], lineParts[1]);
                }
                actualResult = bool.Parse(sr.ReadLine());
                output = !actualResult;
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            Stopwatch sw = Stopwatch.StartNew();
                            output = IsItDAG.IsDAG(vertices, edges);
                            sw.Stop();
                            //PrintCase(vertices,edges, output, actualResult);
                            Console.WriteLine("|V| = {0}, |E| = {1}, time in ms = {2}", vertices.Length, edges.Length, sw.ElapsedMilliseconds);
                            //Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                        }
                        catch
                        {
                            caseException = true;
                            output = !actualResult;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }

                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
					tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (output == actualResult)    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    Console.WriteLine(" your answer = " + output + ", correct answer = " + actualResult);
                    wrongCases++;
                }

                i++;
            }
            file.Close();
            sr.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0)); 
        }

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintCase(string[] vertices1, KeyValuePair<string, string>[] edges1, bool output, bool expected)
        {
            Console.Write("Vertices: ");
            for (int i = 0; i < vertices1.Length; i++)
            {
                Console.Write("{0}  ", vertices1[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Edges: ");
            for (int i = 0; i < edges1.Length; i++)
            {
                Console.WriteLine("{0}, {1}", edges1[i].Key, edges1[i].Value);
            }
            Console.WriteLine("Output = " + output);
            Console.WriteLine("Expected = " + expected);
            Console.WriteLine();
        }
        
        #endregion
   
    }
}
