using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace NES_emu.NES
{
    public class NesRenderer : IDisposable
    {
        //Renderer
        private readonly NativeWindow _window;
        private int _shaderProgram;
        private int _vbo;
        private int _vao;
        private int _ebo;
        private int _textureId;
        private int[] _indices = Array.Empty<int>();
        private int _textureLocation;

        //Window
        private int _scale = 1;
        private readonly int _width = 256;
        private readonly int _height = 240;
        private readonly byte[] _bitmap;

        public bool Closed { get; private set; }
        public KeyboardState KeyboardState => _window.KeyboardState;

        public NesRenderer(int scale = 1)
        {
            //Set scale first
            _scale = Math.Clamp(scale, 1, 5);

            _bitmap = new byte[_width * _height * 4];

            var windowSettings = new NativeWindowSettings
            {
                Size = new Vector2i(_width * _scale, _height * _scale),
                Title = "NES Emulator",
                WindowBorder = WindowBorder.Fixed // Disable window resizing
            };

            _window = new NativeWindow(windowSettings);
            _window.Closing += WindowClosing;
            _window.MakeCurrent();

            InitializeAll();
        }

        public void SetScale(int scale = 0)
        {
            if (scale < 1 || scale > 5 || scale == _scale)
            {
                return;
            }

            _scale = scale;

            _window.Size = new Vector2i(_width * _scale, _height * _scale);
        }

        private void InitializeAll()
        {
            // Initialize OpenGL objects
            InitializeShaders();
            InitializeVertexBuffer();
            InitializeTexture();
        }

        private void InitializeShaders()
        {
            // Create shader program
            _shaderProgram = GL.CreateProgram();

            // Create and compile vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, @"
                #version 330 core

                layout (location = 0) in vec2 vertexPosition;
                layout (location = 1) in vec2 textureCoord;

                out vec2 texCoord;

                void main()
                {
                    gl_Position = vec4(vertexPosition, 0.0, 1.0);
                    texCoord = textureCoord;
                }
            ");
            GL.CompileShader(vertexShader);
            GL.AttachShader(_shaderProgram, vertexShader);

            // Create and compile fragment shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, @"
                #version 330 core

                in vec2 texCoord;
                out vec4 fragColor;
        
                uniform sampler2D textureSampler;

                void main()
                {
                    fragColor = texture(textureSampler, texCoord);
                }
            ");
            GL.CompileShader(fragmentShader);
            GL.AttachShader(_shaderProgram, fragmentShader);

            // Link the shader program
            GL.LinkProgram(_shaderProgram);

            // Cleanup
            GL.DetachShader(_shaderProgram, vertexShader);
            GL.DeleteShader(vertexShader);
            GL.DetachShader(_shaderProgram, fragmentShader);
            GL.DeleteShader(fragmentShader);
        }

        private void InitializeVertexBuffer()
        {
            // Define vertices and texture coordinates for a quad
            float[] vertices =
            {
                -1.0f, -1.0f,  // Bottom-left vertex
                 1.0f, -1.0f,  // Bottom-right vertex
                 1.0f,  1.0f,  // Top-right vertex
                -1.0f,  1.0f   // Top-left vertex
            };

            float[] texCoords =
            {
                0.0f, 0.0f,  // Bottom-left texture coordinate
                1.0f, 0.0f,  // Bottom-right texture coordinate
                1.0f, 1.0f,  // Top-right texture coordinate
                0.0f, 1.0f   // Top-left texture coordinate
            };

            _indices = new int[]{
                0, 1, 2,  // Triangle 1
                2, 3, 0   // Triangle 2
            };

            // Create the vertex buffer object (VBO)
            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * (vertices.Length + texCoords.Length)), IntPtr.Zero, BufferUsageHint.StaticDraw);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(sizeof(float) * vertices.Length), vertices);
            GL.BufferSubData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, (IntPtr)(sizeof(float) * texCoords.Length), texCoords);

            // Create the vertex array object (VAO)
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            // Specify the vertex attributes
            int vertexAttribLocation = GL.GetAttribLocation(_shaderProgram, "vertexPosition");
            GL.EnableVertexAttribArray(vertexAttribLocation);
            GL.VertexAttribPointer(vertexAttribLocation, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, 0);

            int texCoordAttribLocation = GL.GetAttribLocation(_shaderProgram, "textureCoord");
            GL.EnableVertexAttribArray(texCoordAttribLocation);
            GL.VertexAttribPointer(texCoordAttribLocation, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, sizeof(float) * vertices.Length);

            GL.BindVertexArray(0);

            _ebo = GL.GenBuffer();

            _textureLocation = GL.GetUniformLocation(_shaderProgram, "textureSampler");
        }

        private void InitializeTexture()
        {
            // Generate the texture
            _textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            // Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            // Upload the bitmap data to the texture
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _width, _height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, _bitmap);

            // Unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void UpdateTexture()
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            // Upload the updated bitmap data to the texture
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, _width, _height, PixelFormat.Rgba, PixelType.UnsignedByte, _bitmap);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private void DeleteAll()
        {
            GL.DeleteTexture(_textureId);
            GL.DeleteBuffer(_vbo);
            GL.DeleteBuffer(_ebo);
            GL.DeleteVertexArray(_vao);
            GL.DeleteProgram(_shaderProgram);
        }

        public void ProcessEvents()
        {
            _window.ProcessEvents(0d);
        }

        public void RenderFrame()
        {
            UpdateTexture();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Activate the shader program
            GL.UseProgram(_shaderProgram);

            // Bind the texture
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            // Set the texture uniform in the shader program
            
            GL.Uniform1(_textureLocation, 0);  // Use texture unit 0

            // Render the quad with the texture
            GL.BindVertexArray(_vao);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * _indices.Length, _indices, BufferUsageHint.StaticDraw);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(0);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            _window.Context.SwapBuffers();
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            //Ignore if out of bounds
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                return;
            }

            var index = ((_height - 1 - y) * _width + x) * 4;
            _bitmap[index] = r;
            _bitmap[index + 1] = g;
            _bitmap[index + 2] = b;
            _bitmap[index + 3] = 0xFF;
        }

        private void WindowClosing(object sender)
        {
            // Clean up OpenGL resources and dispose the renderer
            Dispose();
        }

        public void Dispose()
        {
            DeleteAll();

            _window.Dispose();

            Closed = true;

            GC.SuppressFinalize(this);
        }
    }
}
