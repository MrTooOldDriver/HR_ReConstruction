using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HR_ReConstruction
{
    class Formula
    {
        public double Sigmoid(double input)
        {
            input = input*-1;
            double output = Math.Pow(Math.E, input);
            output = output + 1;
            output = 1 / output;
            return output;
        }

        private static Random rng = new Random();

        public double RandomNumber_Positive_Large()
        {
            double output = rng.Next(0, 100);
            return output;
        }

        public double RandomNumber_Positive_Negative()
        {
            double output = rng.NextDouble();
            double Go_negative = rng.Next(0, 2);
            if (Go_negative == 1)
            {
                output = output*-1;
            }
            return output;
            //Random Number Test Pass
        }

        public double[] ConvertDoubles(byte[] inputBytes)
        {
            double[] output = new double[inputBytes.Length-1];
            for (int i = 0; i < inputBytes.Length-1; i++)
            {
                output[i] = Convert.ToDouble(inputBytes[i]);
            }

            return output;
        }

        public double[] Inital_Number_Weights(int Length)
        {
            double[] outputDoubles = new double[Length];
            for (int i = 0; i < outputDoubles.Length-1; i++)
            {
                outputDoubles[i] = (RandomNumber_Positive_Negative());
            }
            return outputDoubles;
        }

        public double[] Forword_Calculation(double[] Node, double[][] Weight, double[] Bia)
        {
            double[] outputDoubles = new double[Weight.Length];
            for (int i = 0; i < Weight.Length; i++)
            {
                for (int j = 0; j < Node.Length; j++)
                {
                    outputDoubles[i] = outputDoubles[i] + (Node[j] * Weight[i][j]);
                }
            }

            for (int k = 0; k < Bia.Length; k++)
            {
                outputDoubles[k] = Sigmoid(outputDoubles[k] + Bia[k]);
            }

            return outputDoubles;
        }

    }
}
