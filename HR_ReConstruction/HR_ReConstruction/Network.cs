using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HR_ReConstruction
{
    class Network
    {
        public class  InputData_DataFormatClass
        {
            public double pixinfo_InputNode { get; set; }
        }

        public List<InputData_DataFormatClass> InputData(double[] pixelInformation)
        {
            List<InputData_DataFormatClass> inputDataDataFormatClasses = new List<InputData_DataFormatClass>();
            
            for (int i = 0; i < pixelInformation.Length; i++)
            {
                InputData_DataFormatClass tem = new InputData_DataFormatClass();
                tem.pixinfo_InputNode = pixelInformation[i];
                inputDataDataFormatClasses.Add(tem);
            } 

            return inputDataDataFormatClasses;
        }

        Formula formulaClass = new Formula();

        public double[] MainController(double[] Pix_info)
        {
            //Grab Input
            Network_Data.InputPixInfo TrueInput = Pix_Lable_Separation(Pix_info);
            double LableNumber = TrueInput.LableNumber;

            //Initial Network Layers

            Network_Data.NeuralNetwork NeuralNetwork = initNeuralNetwork();
            // Rewrite function




            Network_Data.Layer FirstLayer = InitLayer(784,16);
            FirstLayer.Node = TrueInput.PixInfo;           
            FirstLayer.NextLayerNode = 
                formulaClass.Forword_Calculation(FirstLayer.Node, FirstLayer.Weight, FirstLayer.Bia);

            Network_Data.Layer SecondLayer = InitLayer(16, 16);
            SecondLayer.Node = FirstLayer.NextLayerNode[1];
            SecondLayer.NextLayerNode =
                formulaClass.Forword_Calculation(SecondLayer.Node, SecondLayer.Weight, SecondLayer.Bia);

            Network_Data.Layer OutputLayer = InitLayer(16, 10);
            OutputLayer.Node = SecondLayer.NextLayerNode[1];
            OutputLayer.NextLayerNode =
                formulaClass.Forword_Calculation(OutputLayer.Node, OutputLayer.Weight, OutputLayer.Bia);
            //Initial Network Layers Complete

            //Result Check 
            double[] ExpectOutput = GenerateExpectLayer(LableNumber);

            double[] CostFunction = Cost_Function(OutputLayer.NextLayerNode[1], ExpectOutput);
            //Result Check Finished 

            //Backpropagation
            OutputLayer.Common_Derivative=formulaClass.Common_Derivative_Output_Layer(OutputLayer.NextLayerNode[0], OutputLayer.NextLayerNode[1],
                ExpectOutput);

            OutputLayer.De_Bia = formulaClass.Bia_Differentiation(OutputLayer.Common_Derivative);
            OutputLayer.De_Weight =
                formulaClass.Weight_Differentiation(OutputLayer.Common_Derivative, OutputLayer.Node);
            OutputLayer.De_Node =
                formulaClass.Input_Layer_Differentiation(OutputLayer.Common_Derivative, OutputLayer.Weight);

            SecondLayer.Common_Derivative = formulaClass.Common_Derivative_Hidden_Layer(OutputLayer.De_Node);
            SecondLayer.De_Bia = formulaClass.Bia_Differentiation(SecondLayer.Common_Derivative);
            SecondLayer.De_Weight =
                formulaClass.Weight_Differentiation(SecondLayer.Common_Derivative, SecondLayer.Node);
            SecondLayer.De_Node =
                formulaClass.Input_Layer_Differentiation(SecondLayer.Common_Derivative, SecondLayer.Weight);

            FirstLayer.Common_Derivative = formulaClass.Common_Derivative_Hidden_Layer(SecondLayer.De_Node);
            FirstLayer.De_Bia = formulaClass.Bia_Differentiation(FirstLayer.Common_Derivative);
            FirstLayer.De_Weight =
                formulaClass.Weight_Differentiation(FirstLayer.Common_Derivative, FirstLayer.Node);
            FirstLayer.De_Node =
                formulaClass.Input_Layer_Differentiation(FirstLayer.Common_Derivative, FirstLayer.Weight);

            //adjust all layer method


            return CostFunction;
   
        }


        public Network_Data.Layer InitLayer(int InputNodeNumber,int OutputNodeNumber)
        {
            Network_Data.Layer layer = new Network_Data.Layer();
            Formula formulaClass = new Formula();

            layer.Node = new double[InputNodeNumber];
            layer.Bia = new double[OutputNodeNumber];
            //layer.NextLayerNode = new double[OutputNodeNumber][];
            layer.Weight = new double[OutputNodeNumber][];

            for (int i = 0; i < layer.Weight.Length; i++)
            {
                layer.Weight[i] = new double[InputNodeNumber];
                layer.Weight[i] = formulaClass.Inital_Number_Weights(InputNodeNumber);
            }


            layer.Bia = formulaClass.Inital_Number_Bias(OutputNodeNumber);

            return layer;
        }

        public Network_Data.InputPixInfo Pix_Lable_Separation(double[] FromDatabaseReader)
        {
            Network_Data.InputPixInfo outputInfo = new Network_Data.InputPixInfo();
            outputInfo.PixInfo = new double[FromDatabaseReader.Length-1];
            for (int i = 0; i < outputInfo.PixInfo.Length; i++)
            {
                outputInfo.PixInfo[i] = FromDatabaseReader[i];
            }

            outputInfo.LableNumber = FromDatabaseReader[FromDatabaseReader.Length-1];

            return outputInfo;
        }

        public double[] Cost_Function(double[] OutputLayer, double[] ExpectOutputLater)
        {
            double[] OutputCostFunction = new double[OutputLayer.Length];
            for (int i = 0; i < OutputLayer.Length; i++)
            {
                OutputCostFunction[i] = Math.Pow(OutputLayer[i] - ExpectOutputLater[i], 2);
            }

            return OutputCostFunction;
        }

        public double[] GenerateExpectLayer(double lable)
        {
            double[] outputDoubles = new double[10];
            for (int i = 0; i < outputDoubles.Length-1; i++)
            {
                if (i == lable)
                {
                    outputDoubles[i] = 1;
                }
                else
                {
                    outputDoubles[i] = 0;
                }
            }

            return outputDoubles;
        }

        public Network_Data.NeuralNetwork initNeuralNetwork()
        {
            Network_Data.NeuralNetwork inputNeuralNetwork = new Network_Data.NeuralNetwork();
            inputNeuralNetwork.FirstLayer = InitLayer(784, 16);
            inputNeuralNetwork.SecondLayer = InitLayer(16, 16);
            inputNeuralNetwork.OutputLayer = InitLayer(16, 10);
            return inputNeuralNetwork;
        }

    }
}
