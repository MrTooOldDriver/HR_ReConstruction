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

        public double Derivative_Sigmoid(double input)
        {
            double output = Sigmoid(input) * (1 - Sigmoid(input));
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
            double[] output = new double[inputBytes.Length];
            for (int i = 0; i < inputBytes.Length; i++)
            {
                output[i] = Convert.ToDouble(inputBytes[i]);
            }

            return output;
        }

        public double[] Inital_Number_Weights(int Length)
        {
            double[] outputDoubles = new double[Length];
            for (int i = 0; i < outputDoubles.Length; i++)
            {
                outputDoubles[i] = (RandomNumber_Positive_Negative());
            }
            return outputDoubles;
        }

        public double[] Inital_Number_Bias(int Length)
        {
            double[] outputDoubles = new double[Length];
            for (int i = 0; i < outputDoubles.Length; i++)
            {
                outputDoubles[i] = (RandomNumber_Positive_Large());
            }
            return outputDoubles;
        }

        public double[][] Forword_Calculation(double[] Node, double[][] Weight, double[] Bia)
        {
            double[] SigmoidResult = new double[Weight.Length];
            double[] NoSigmoidResult = new double[Weight.Length];
            double[][] finalDoubles = new double[2][];

            for (int i = 0; i < Weight.Length; i++)
            {
                for (int j = 0; j < Node.Length; j++)
                {
                    SigmoidResult[i] = SigmoidResult[i] + (Node[j] * Weight[i][j]);
                }
            }

            for (int k = 0; k < Bia.Length; k++)
            {
                NoSigmoidResult[k] = SigmoidResult[k] + Bia[k];
                SigmoidResult[k] = Sigmoid(SigmoidResult[k] + Bia[k]);
            }

            finalDoubles[0] = NoSigmoidResult;
            finalDoubles[1] = SigmoidResult;

            return finalDoubles;
        }

        public double[] Common_Derivative_Calculation(double[] NoSigmoidResult, double[] SigmoidResult,
            double[] ExpectResult)
        {
            double[] outputDoubles = new double[NoSigmoidResult.Length];
            for (int i = 0; i < NoSigmoidResult.Length; i++)
            {
                outputDoubles[i] = (2 * (SigmoidResult[i] - ExpectResult[i])) *
                                   (Derivative_Sigmoid(NoSigmoidResult[i]));
            }

            return outputDoubles;
        }

    }
}
