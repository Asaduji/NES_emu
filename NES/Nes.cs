using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NES_emu.NES
{
    public class Nes : GameWindow
    {
        public Nes(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title }) { }

        protected override void OnLoad()
        {
            // Set up OpenGL settings
            GL.ClearColor(Color4.Black);

            base.OnLoad();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            // Clear the screen
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Set the pixel color
            byte red = 255;
            byte green = 0;
            byte blue = 0;

            // Draw a pixel at (400, 300)
            GL.Begin(PrimitiveType.Points);
            GL.Color3(red, green, blue);
            GL.Vertex2(0, 0);
            GL.End();

            // Swap the back and front buffers
            SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            // Update the viewport when the window is resized
            GL.Viewport(0, 0, e.Width, e.Height);

            base.OnResize(e);
        }
    }
}
