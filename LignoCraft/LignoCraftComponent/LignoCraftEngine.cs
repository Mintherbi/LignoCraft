using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Text; 

using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace LignoCraft.LignoCraftComponent
{
    public class LignoCraftEngine : GH_Component
    {
        string Port;
        int baudRate;
        bool _continue = true;

        List<string> log = new List<string>();
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
            m_attributes = new CustomUI.ButtonUIAttributes(this, "Connect", Connect2Device, "Connect to Device");
        }

        public void Connect2Device()
        {
            SerialPort _serialPort = new SerialPort("COM3", 115200);
            _serialPort.Open();
            _serialPort.Write("?");
            Thread.Sleep(500);
            log.Add("Information");
            if (_serialPort.BytesToRead > 0)
            {
                log.Add(_serialPort.ReadLine());
            }
            _serialPort.Write("$$");
            Thread.Sleep(500);
            log.Add("Connecting...");
            if (_serialPort.BytesToRead > 0)
            {
                log.Add(_serialPort.ReadLine());
            }
            _serialPort.Write("$h");
            _serialPort.Write("G01 F2000");
            Thread.Sleep(500);
            log.Add("Homing...");
            if (_serialPort.BytesToRead > 0)
            {
                log.Add(_serialPort.ReadLine());
            }
            _serialPort.Close();
        }
        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("serialPort", "sP", "Name of the port", GH_ParamAccess.item, "COM3");
            pManager.AddIntegerParameter("baudRate", "bR", "baudRate", GH_ParamAccess.item, 115200);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("FeedBack", "FB", "Feedback from device", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region ///Set input parameter
            if(!DA.GetData(0, ref Port)) { return; }
            if(!DA.GetData(1, ref baudRate)) { return; }
            #endregion

            #region ///Set output parameter
            DA.SetDataList(0, log);
            #endregion

            this.ExpireSolution(true);
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