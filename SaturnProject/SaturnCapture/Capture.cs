using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;


namespace SaturnProject.SaturnCapture
{

    internal class Capture : CaptureBase
    {


        public bool DoCapture(string viewName, ref Bitmap returnBitmap)
        {
            Rhino.Display.RhinoView view = GetView(viewName);
            if (view != null)
            {
                //
                // Setup View
                //
                ViewSetup(view, targetMinDimension);


                var dmode_rdr = Rhino.Display.DisplayModeDescription.FindByName("Shaded");

                // TODO: pass something in here to set the display mode.
                Bitmap bmp = view.CaptureToBitmap();


                //
                // Teardown View
                //
                ViewTeardown(view);


                //
                // Resize
                //
                Bitmap resizedBitmap = ResizeToDimension(bmp, targetMinDimension);
                returnBitmap = resizedBitmap;

                return true;
            }
            else
            {
                throw new SaturnCaptureException(string.Format("Invalid view: {0}.", viewName));
            }
        }


    }
}
