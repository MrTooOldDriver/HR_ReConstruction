using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void MainController(double[] Pix_info)
        {
            Network_Data.Layer FirstLayer = new Network_Data.Layer();

            FirstLayer.Node = Pix_info;
        }



    }
}
