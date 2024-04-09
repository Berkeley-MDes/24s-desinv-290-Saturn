using Rhino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SaturnProject.SaturnCapture
{

    internal class CaptureBase
    {
        bool doMaximize = false;
        public const int targetMinDimension = 512;
        private bool ViewMaximizationState;

        public void ViewSetup(Rhino.Display.RhinoView view, int minDim)
        {
            if (doMaximize)
            {
                // maximize view and test size
                ViewMaximizationState = view.Maximized;
                view.Maximized = true;
                if (Math.Min(view.Size.Width, view.Size.Height) < minDim)
                {
                    view.Maximized = ViewMaximizationState; // unmaximize view
                    throw new SaturnCaptureException(string.Format("Viewport for view {0} is too small! Minimum dimension is {1} pixels.", view.ActiveViewport.Name, minDim));
                }
            }
            else
            {
                if (Math.Min(view.Size.Width, view.Size.Height) < minDim)
                {
                    throw new SaturnCaptureException(string.Format("Viewport for view {0} is too small! Minimum dimension is {1} pixels.", view.ActiveViewport.Name, minDim));
                }
            }
        }

        public void ViewTeardown(Rhino.Display.RhinoView view)
        {
            if (doMaximize) view.Maximized = ViewMaximizationState; // unmaximize view
        }



        public static Rhino.Display.RhinoView GetView(string viewName)
        {
            RhinoDoc rhinoDoc = RhinoDoc.ActiveDoc;
            Rhino.Display.RhinoView namedView = null;

            // Attempt to find a view with the specified name.
            foreach (Rhino.Display.RhinoView view in rhinoDoc.Views)
            {

                if (view.MainViewport.Name.Equals(viewName, StringComparison.CurrentCultureIgnoreCase))
                {
                    namedView = view;
                    break;
                }
            }

            // If a view with the specified name was found, return it.
            if (namedView != null)
            {
                return namedView;
            }
            else
            {
                // If no view with the specified name was found, return the active view.
                Rhino.Display.RhinoView activeView = rhinoDoc.Views.ActiveView;
                if (activeView == null)
                {
                    RhinoApp.WriteLine(string.Format("Saturn - Critical error: we could not find a view named {0}, and could not find ActiveView.", viewName));
                    return null;
                }
                else
                {
                    RhinoApp.WriteLine(string.Format("Saturn - We could not find a view named {0}. Falling back to ActiveView.", viewName));
                    return activeView;
                }
            }

        }

        public static Bitmap ResizeToDimension(Bitmap bmp, int minDim)
        {
            int smallerDimension = Math.Min(bmp.Width, bmp.Height); // Determine the smaller dimension.
            float scale = (float)minDim / smallerDimension; // Calculate the scale factor.
            int newWidth = (int)(bmp.Width * scale); // Calculate the new dimensions while maintaining the aspect ratio.
            int newHeight = (int)(bmp.Height * scale);
            Bitmap resizedBitmap = new Bitmap(bmp, new Size(newWidth, newHeight)); // Create a new bitmap with the new dimensions.

            //RhinoApp.WriteLine(String.Format("Created a capture of size {0}x{1} resized to {2}x{3}", bmp.Width, bmp.Height, resizedBitmap.Width, resizedBitmap.Height));
            return resizedBitmap;
        }


    }


    public class SaturnCaptureException : Exception
    {
        public SaturnCaptureException() { }

        public SaturnCaptureException(string message)
            : base(message) { }

        public SaturnCaptureException(string message, Exception inner)
            : base(message, inner) { }
    }



}
