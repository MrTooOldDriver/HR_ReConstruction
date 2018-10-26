using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_ReConstruction
{
    class Formula
    {
        public double Sigmoid(double input)
        {
            input = -input;
            double output = 1 / (1 + (Math.Pow(Math.E,input)));
            return output;
        }

        public double RandomNumber()
        {
            Random rng = new Random();
            double output = rng.Next(0, 100);
            return output;
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

    }
}
