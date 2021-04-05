using AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ACAD_COLOR = AXDBLib.ACAD_COLOR;
using AcadArc = AutoCAD.AcadArc;
using AcadDimRadial = AutoCAD.AcadDimRadial;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace CallAutoCad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AcadCircle Circle = default(AcadCircle);
        private AcadApplication AcadApp = default(AcadApplication);
        private Acad3DSolid acad3DSolid = default(Acad3DSolid);


        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateCircle_Click(object sender, RoutedEventArgs e)
        {
            AcadApp = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application");
            double[] CenterOfCircle = new double[3];
            CenterOfCircle[0] = 0;
            CenterOfCircle[1] = 0;
            CenterOfCircle[2] = 0;

            if (pointX.Text != "" || pointY.Text != "" || pointZ.Text != "")
            {
                CenterOfCircle[0] = double.Parse(pointX.Text);
                CenterOfCircle[1] = double.Parse(pointY.Text);
                CenterOfCircle[2] = double.Parse(pointZ.Text);
            }

            double RadiusOfCircle = Convert.ToDouble(TextBox.Text.Trim());
            Circle = AcadApp.ActiveDocument.ModelSpace.AddCircle(CenterOfCircle, RadiusOfCircle);
        }
       
    }
}
