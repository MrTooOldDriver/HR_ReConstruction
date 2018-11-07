using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
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

        public double[] Common_Derivative_Output_Layer(double[] NoSigmoidResult, double[] SigmoidResult,
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

        public double[] Bia_Differentiation(double[] Common_Derivative)
        {
            double[] outputDoubles= new double[Common_Derivative.Length];
            for (int i = 0; i < Common_Derivative.Length; i++)
            {
                outputDoubles[i] = Common_Derivative[i];
            }

            return outputDoubles;

        }

        public double[][] Weight_Differentiation(double[] Common_Derivative,double[] Input_Lyaer)
        {
            //Common_Derivative.Length = Output Layer Node Number
            //Input_Lyaer.Length = Input Layer Node Number

            double[][] outputDoubles = new double[Common_Derivative.Length][];
            for (int i = 0; i < Common_Derivative.Length; i++)
            {
                outputDoubles[i] = new double[Input_Lyaer.Length];
            }
            for (int j = 0; j < Common_Derivative.Length; j++)
            {
                for (int k = 0; k < Input_Lyaer.Length; k++)
                {
                    outputDoubles[j][k] = Common_Derivative[j] * Input_Lyaer[k];
                }
            }

            return outputDoubles;
        }

        public double[] Input_Layer_Differentiation(double[] Common_Derivative,double[][] weight)
        {
            double[] outputDoubles = new double[Common_Derivative.Length];
            for (int i = 0; i < Common_Derivative.Length; i++)
            {
                for (int j = 0; j < weight.Length; j++)
                {
                    outputDoubles[i] += Common_Derivative[j] * weight[j][i]; //Important +=
                }
            }

            return outputDoubles;
        }

        public double[] Common_Derivative_Hidden_Layer(double[] Derivative_From_Last_Input_Layer)
        {
            double[] outputDoubles = new double[Derivative_From_Last_Input_Layer.Length];
            for (int i = 0; i < Derivative_From_Last_Input_Layer.Length; i++)
            {
                outputDoubles[i] = Derivative_Sigmoid(Derivative_From_Last_Input_Layer[i]);
            }

            return outputDoubles;
        }

    }
}
