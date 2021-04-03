using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabsOfToolsPO
{
    /// <summary>
    /// The IAction interface is common to all class of Strategy.
    /// It declares a method the ContextCalculate uses to execute a strategy.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// General strategy for math actions.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The result of two numbers.</returns>
        public double Execute(double a, double b);
    }
    /// <summary>
    /// StrategyAdd implement an algorithm the ContextCalculate uses.
    /// </summary>
    public class StrategyAdd : IAction
    {
        /// <summary>
        /// Adds two numbers and returns the result
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The sum of two numbers.</returns>
        public double Execute(double a, double b)
        {
            return a + b;
        }
    }
    /// <summary>
    /// StrategySubtract implement an algorithm the ContextCalculate uses.
    /// </summary>
    public class StrategySubtract : IAction
    {
        /// <summary>
        /// Subtracts an number from another and returns the result
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The subtracts of two numbers.</returns>
        public double Execute(double a, double b)
        {
            return a - b;
        }
    }
    /// <summary>
    /// StrategyMultiply implement an algorithm the ContextCalculate uses.
    /// </summary>
    public class StrategyMultiply : IAction
    {
        /// <summary>
        /// Multiplies two numbers and returns the result
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The multiplies of two numbers.</returns>
        public double Execute(double a, double b)
        {
            return a * b;
        }
    }
    /// <summary>
    /// StrategyDivision implement an algorithm the ContextCalculate uses.
    /// </summary>
    public class StrategyDivision : IAction
    {
        /// <summary>
        /// Divides a numbers by another and returns the result
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The division of two numbers.</returns>
        public double Execute(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            return a / b;
        }
    }
    /// <summary>
    /// StrategyPercent implement an algorithm the ContextCalculate uses.
    /// </summary>
    public class StrategyPercent : IAction
    {
        /// <summary>
        /// Percents a numbers by another and returns the result.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The percent of two numbers.</returns>
        public double Execute(double a, double b)
        {
            return a * b / 100;
        }
    }
    /// <summary>
    /// The ContextCalculate calls the execution method on the linked
    /// strategy object each time it needs to run the algorithm.
    /// The ContextCalculate doesn’t know what type of strategy
    /// it works with or how the algorithm is executed.
    /// </summary>
    public class ContextCalculate
    {
        /// <summary>
        /// A reference to one of the concrete Strategy.
        /// </summary>
        public IAction Action { get; set; }
        /// <summary>
        /// Assigning a reference to a Action
        /// Clients of the ContextCalculate must associate it with a suitable
        /// action that matches the way they expect
        /// the ContextCalculate to perform its primary job.
        /// </summary>
        /// <param name="action"></param>
        public ContextCalculate(IAction action)
        {
            this.Action = action;
        }
        /// <summary>
        /// Performing a calculation on two numbers
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>The result of two numbers.</returns>
        public double Making(double a, double b)
        {
            return Action.Execute(a, b);
        }

    }
    /// <summary>
    /// Contains all methods for reading from a file and performing mathematical calculations.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// The path to the file that contains the user's math values.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// List of all results.
        /// </summary>
        public List<string> ListOfResults { get; }
        /// <summary>
        /// List of individual lines from a file.
        /// </summary>
        public List<string> ListOfLinesFromFile { get; set; }
        /// <summary>
        /// Dictionary of all actions.
        /// </summary>
        public Dictionary<char, IAction> DictionaryOfActions { get; private set; } = new Dictionary<char, IAction>()
        {
            {'+', new StrategyAdd()},
            {'-', new StrategySubtract()},
            {'*', new StrategyMultiply()},
            {'/', new StrategyDivision()},
            {'%', new StrategyPercent()}
        };
        /// <summary>
        /// Constructor of current class.
        /// </summary>
        /// <param name="path"></param>
        public Calculator(string path)
        {

            this.Path = path;

            ListOfResults = new List<string>();

        }
        /// <summary>
        /// Small facade of the calculator.
        /// It runs the calculator calculations and outputs the result.
        /// </summary>
        public void RunCalculator()
        {
            ReadingFile();
            CalculatingLines();
            PrintResults();
        }
        /// <summary>
        /// Line-by-line reading from file and save to List Of Lines From File.
        /// </summary>
        public void ReadingFile()
        {
            try
            {
                if (File.Exists(Path))
                {
                    ListOfLinesFromFile = new List<string>();

                    using (StreamReader sr = new StreamReader(Path, System.Text.Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            ListOfLinesFromFile.Add(line);
                        }
                    }

                }
                else
                {
                    throw new Exception("File not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        /// <summary>
        /// Line-by-line calculation of mathematical expressions.
        /// </summary>
        public void CalculatingLines()
        {
            foreach (string currentLine in ListOfLinesFromFile)
            {
                ListOfResults.Add(WorkingWithLine(currentLine));
            }
        }
        /// <summary>
        /// Output of all results.
        /// </summary>
        public void PrintResults()
        {
            foreach (string result in ListOfResults)
            {
                Console.WriteLine(result);
            }
        }
        /// <summary>
        /// Get list of results like IEnumerator
        /// </summary>
        /// <returns>ListOfResults</returns>
        public IEnumerator GetListOfResults()
        {
            return ListOfResults.GetEnumerator();
        }
        /// <summary>
        /// Calculating all results.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public string WorkingWithLine(string line)
        {
            ContextCalculate contextCalculate = new ContextCalculate(new StrategyAdd());

            // Regular expression for getting numbers and operations from string.
            string[] ListOfNumbers = Regex.Matches(line, "[0-9]+").Cast<Match>().Select(m => m.Value).ToArray();
            string[] ListOfOperations = Regex.Matches(line, @"[+-\/*%]").Cast<Match>().Select(m => m.Value).ToArray();

            // Convert to Type of double and char the numbers and operations.
            double[] ArrayOfNumbersForCalculate = Array.ConvertAll(ListOfNumbers, double.Parse);
            char[] ArrayOfOperationsForCalculate = Array.ConvertAll(ListOfOperations, char.Parse);

            if (ArrayOfOperationsForCalculate.Length >= ArrayOfNumbersForCalculate.Length) { throw new Exception("you can't use more operations than numbers"); }

            double result = ArrayOfNumbersForCalculate[0];
            int IterationOfOperation = 0;

            for (var i = 1; i < ArrayOfNumbersForCalculate.Length; i++)
            {
                char CurrentSymbol = ArrayOfOperationsForCalculate[IterationOfOperation];

                if (DictionaryOfActions.ContainsKey(CurrentSymbol))
                {
                    contextCalculate.Action = DictionaryOfActions[CurrentSymbol];

                    result = contextCalculate.Making(result, ArrayOfNumbersForCalculate[i]);

                }
                IterationOfOperation++;
            }
            string answer = line + " = " + result.ToString();
            return answer;
        }

    }
}
