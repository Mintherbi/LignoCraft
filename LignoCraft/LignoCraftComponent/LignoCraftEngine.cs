using System;
using System.Collections.Generic;

using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace LignoCraft.LignoCraftComponent
{
    public class LignoCraftEngine : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public LignoCraftEngine()
          : base("LignoCraftEngine", "LE",
            "Main Engine for Manipulating GCode and 3D Printer",
            "BinaryNature", "LignoCraft")
        {
        }

        public override void CreateAttributes()
        {
            List<object> buttontest = new List<object>();
            buttontest.Add(new CustomUI.ButtonUIAttributes(this, "A", AFunctionToRunOnClick, "Opt description"));
            buttontest.Add(new CustomUI.ButtonUIAttributes(this, "B", BFunctionToRunOnClick, "Opt description"));
        }

        public void AFunctionToRunOnClick()
        {
            System.Windows.Forms.MessageBox.Show("A Button was clicked");
        }
        public void BFunctionToRunOnClick()
        {
            System.Windows.Forms.MessageBox.Show("B Button was clicked");
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Input1", "I1", "First Input", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Output1", "O1", "First Output", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("dbb3c6d0-de65-4605-bfeb-ee5c09052662");
    }
}