using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;

using SaturnProject.SaturnCapture;
using System.Drawing;

namespace SaturnProject
{
    public class SaturnCommand : Command
    {
        public SaturnCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static SaturnCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "SaturnTestCommand";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: start here modifying the behaviour of your command.
            // ---
            RhinoApp.WriteLine("The {0} command is not implimented.", EnglishName);

            Capture capture = new Capture();

            Bitmap bmp = new Bitmap(100,100);
            capture.DoCapture("noname", ref bmp);
            bmp.Save("C:\\Users\\ksteinfe\\Desktop\\test.bmp");

            // ---
            return Result.Success;
        }
    }
}
