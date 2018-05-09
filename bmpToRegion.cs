using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AnimatedForm
{
    internal unsafe class BmpToRegion
    {
        static Bitmap bitmap;
        static GraphicsUnit unit = GraphicsUnit.Pixel;
        static RectangleF boundsF;
        static Rectangle bounds;
        static BitmapData bitmapData;
        static uint* pixelPtr;
        static int yMax;
        static int xMax;
        static int x0;
        static byte* basePos;

        private BmpToRegion()
        {
        }

        public static Region Convert(Image image)
        {
            bitmap = new Bitmap(image);

            boundsF = bitmap.GetBounds(ref unit);
            bounds = new Rectangle((int)boundsF.Left, (int)boundsF.Top,
                (int)boundsF.Width, (int)boundsF.Height);


            //get access to the raw bits of the image
            bitmapData = bitmap.LockBits(bounds, ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            pixelPtr = (uint*)bitmapData.Scan0.ToPointer();

            //avoid property accessors in the for
            yMax = (int)boundsF.Height;
            xMax = (int)boundsF.Width;

            //to store all the little rectangles in
            GraphicsPath path = new GraphicsPath();

            for (int y = 0; y < yMax; y++)
            {
                //store the pointer so we can offset the stride directly from it later
                //to get to the next line
                basePos = (byte*)pixelPtr;

                for (int x = 0; x < xMax; x++, pixelPtr++)
                {
                    //is this transparent? if yes, just go on with the loop
                    if ((*pixelPtr == (uint)0))
                        continue;

                    //store where the scan starts
                    x0 = x;

                    //not transparent - scan until we find the next transparent byte
                    while (x < xMax && !((*pixelPtr == (uint)0)))
                    {
                        ++x;
                        pixelPtr++;
                    }

                    //add the rectangle we have found to the path
                    path.AddRectangle(new Rectangle(x0, y, x - x0, 1));
                }
                //jump to the next line
                pixelPtr = (uint*)(basePos + bitmapData.Stride);
            }

            //now create the region from all the rectangles
            Region region = new Region(path);

            //clean up
            path.Dispose();
            bitmap.UnlockBits(bitmapData);
            bitmap.Dispose();
            bitmapData = null;
            pixelPtr = null;

            return region;
        }
    }
}
