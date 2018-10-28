using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace HR_ReConstruction
{
    class Network_Data
    {
        public class Layer
        {
            public double[] Node;
            public double[][] Weight;
            public double[] Bia;
            public double[] NextLayerNode;
        }

        public class InputPixInfo
        {
            public double[] PixInfo;
            public double LableNumber;
        }
    }
}
