using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HR_ReConstruction
{
    /// <summary>
    /// ReadDatabase.xaml 的交互逻辑
    /// </summary>
    public partial class ReadDatabase : Window
    {
        public ReadDatabase()
        {
            InitializeComponent();
        }

        private DatabaseReader databaseReader = new DatabaseReader(); // Object Database reader

        private readonly DatabaseReader newDatabase = new DatabaseReader();

        public double[] CoodCheck(int NumberOfPix, int mag)
        {
            double x = 50;
            double y = 50;
            x = x + NumberOfPix % 28 * mag;
            y = y + NumberOfPix / 28 * mag;
            var re = new double[2];
            re[0] = x;
            re[1] = y;
            return re;
        }

        private void Drawit(byte[] vs)
        {
            // Draw out the image from the MINST database
            this.Can.Children.Clear();
            var mag = 10; // Scale Factor of image
            this.Can.Margin = new Thickness(0, 0, 0, 0); // Create new canvas object
            for (var count = 0; count < vs.Length - 1; count++)
            {
                // display each pixs
                var r = new Rectangle();
                var pixinforReverse = Convert.ToByte(255 - vs[count]); // RGB convert
                r.Fill = new SolidColorBrush(Color.FromRgb(pixinforReverse, pixinforReverse, pixinforReverse));
                r.Width = mag;
                r.Height = mag;
                var db = this.CoodCheck(count, mag); // coordinate Check and calculation of picture
                var x = db[0];
                var y = db[1];       
                r.SetValue(Canvas.LeftProperty, x);
                r.SetValue(Canvas.TopProperty, y);
                this.Can.Children.Add(r); // draw picture
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var vs = this.newDatabase.readBytes(); // Get data from database
            this.Drawit(vs); // draw it out
            var vs_Position = vs.Length;
            this.itemLable.Content = vs[vs_Position - 1];
        }
    }
}
