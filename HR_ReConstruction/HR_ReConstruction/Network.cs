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

        public double[] MainController(double[] Pix_info)
        {
            Formula formulaClass = new Formula();

            Network_Data.Layer FirstLayer = InitLayer(784,16);

            FirstLayer.Node = Pix_info;           

            FirstLayer.NextLayerNode = formulaClass.Forword_Calculation(FirstLayer.Node, FirstLayer.Weight, FirstLayer.Bia);




            return FirstLayer.NextLayerNode;

            
        }


        public Network_Data.Layer InitLayer(int InputNodeNumber,int OutputNodeNumber)
        {
            Network_Data.Layer layer = new Network_Data.Layer();
            Formula formulaClass = new Formula();

            layer.Node = new double[InputNodeNumber];
            layer.Bia = new double[OutputNodeNumber];
            layer.NextLayerNode = new double[OutputNodeNumber];

            layer.Weight = new double[OutputNodeNumber][];

            for (int i = 0; i < layer.Weight.Length; i++)
            {
                layer.Weight[i] = new double[InputNodeNumber];
                layer.Weight[i] = formulaClass.Inital_Number_Weights(InputNodeNumber);
            }

            layer.Bia = formulaClass.Inital_Number_Bias(OutputNodeNumber);

            return layer;
        }



    }
}
