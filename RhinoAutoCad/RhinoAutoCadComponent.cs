using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Geometry;
using Rhino.DocObjects;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace RhinoAutoCad
{
    public class RhinoAutoCadComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public RhinoAutoCadComponent()
          : base("RhinoAutoCad", "Nickname",
              "Description",
              "RhinoAutoCadTest", "Subcategory")
        {
        }


        private AcadCircle Circle = default(AcadCircle);
        private AcadApplication AcadApp = default(AcadApplication);


        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Radius", "R", "Add Radius from Grasshopper", GH_ParamAccess.item);
            pManager.AddPlaneParameter("Base", "B", "Setup Base for Object", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCircleParameter("oCircle", "oC", "Output in Autocad", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Rhino.Geometry.Plane center = Rhino.Geometry.Plane.WorldXY;
            double Radius=10;

            DA.GetData(0, ref Radius);
            DA.GetData(1, ref center);
       

            Rhino.Geometry.Circle output = AddCircle(Radius, center);

            DA.SetData(0, output);
        }

        private Rhino.Geometry.Circle AddCircle(double Radius, Rhino.Geometry.Plane center)
        {
            Rhino.Geometry.Circle circle = new Rhino.Geometry.Circle(center, Radius);
            
            AcadApp = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application");
            double[] CenterOfCircle = new double[3];
            CenterOfCircle[0] = 0;
            CenterOfCircle[1] = 0;
            CenterOfCircle[2] = 0;

            CenterOfCircle[0] = center.OriginX;
            CenterOfCircle[1] = center.OriginY;

            double RadiusOfCircle = Radius;
            Circle = AcadApp.ActiveDocument.ModelSpace.AddCircle(CenterOfCircle, RadiusOfCircle);
            
            return circle;
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("efa686c7-ef37-45f6-abe5-5bc181b2e591"); }
        }
    }
}
