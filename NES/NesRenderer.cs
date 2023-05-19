using SDL2;

namespace NES_emu.NES
{
    public class NesRenderer : IDisposable
    {
        //Renderer pointers
        private readonly IntPtr _window = IntPtr.Zero;
        private readonly IntPtr _renderer = IntPtr.Zero;
        private IntPtr _pixelsTexture = IntPtr.Zero;
        private IntPtr _pixelsBuffer = IntPtr.Zero;

        //Window
        private int _scale = 1;
        private readonly int _width = 256;
        private readonly int _height = 240;
        private int _scaledWidth = 256;
        private int _scaledHeight = 240;

        public NesRenderer(int scale = 1)
        {
            //Set scale first
            _scale = Math.Clamp(scale, 1, 5);
            SetScaledResolution();

            // Initialize SDL
            _ = SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            // Create a window
            _window = SDL.SDL_CreateWindow("NES Emulator", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, _width * _scale, _height * _scale, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            // Create a renderer
            _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            // Allocate first pixel buffer
            _pixelsTexture = SDL.SDL_CreateTexture(_renderer, SDL.SDL_PIXELFORMAT_ARGB8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, _width * _scale, _height * _scale);
        }

        public void SetScale(int scale)
        {
            // Ignore if current scale is the same or no renderer
            if (scale == _scale || _renderer == IntPtr.Zero || _window == IntPtr.Zero)
            {
                return;
            }
            _scale = Math.Clamp(scale, 1, 5);
            SetScaledResolution();

            SDL.SDL_DestroyTexture(_pixelsTexture);

            // Allocate new pixel buffer
            _pixelsTexture = SDL.SDL_CreateTexture(_renderer, SDL.SDL_PIXELFORMAT_ARGB8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, _width * _scale, _height * _scale);

            SDL.SDL_SetWindowSize(_window, _width * _scale, _height * _scale);
        }

        private void SetScaledResolution()
        {
            _scaledWidth = _width * _scale;
            _scaledHeight = _height * _scale;
        }

        public void RenderFrame()
        {
            // Update the texture with pixel data
            _ = SDL.SDL_UpdateTexture(_pixelsTexture, IntPtr.Zero, IntPtr.Zero, _width * 4 * _scale);

            // Clear the renderer
            _ = SDL.SDL_RenderClear(_renderer);

            // Render the texture
            _ = SDL.SDL_RenderCopy(_renderer, _pixelsTexture, IntPtr.Zero, IntPtr.Zero);

            // Update the screen
            SDL.SDL_RenderPresent(_renderer);
        }

        public void BeginDraw()
        {
            _ = SDL.SDL_LockTexture(_pixelsTexture, IntPtr.Zero, out _pixelsBuffer, out _);
        }

        public void EndDraw()
        {
            SDL.SDL_UnlockTexture(_pixelsTexture);
            _pixelsBuffer = IntPtr.Zero;
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            //Ignore if out of bounds
            if (x < 0 || x >= _width || y < 0 || y >= _height || _pixelsBuffer == IntPtr.Zero)
            {
                return;
            }

            var scaledX = x * _scale;
            var scaledY = y * _scale;

            // Calculate the starting index of the block of pixels
            var startIndex = scaledY * _scaledWidth + scaledX;
            var color = (0xFF << 24) | (r << 16) | (g << 8) | b;

            unsafe
            {
                var buffer = (int*)_pixelsBuffer;
              
                for (int i = 0; i < _scale; i++)
                {
                    for (int j = 0; j < _scale; j++)
                    {
                        buffer[startIndex + i * _scaledWidth + j] = color;
                    }
                }
            }
        }

        public void Dispose()
        {
            // Destroy the renderer and window
            SDL.SDL_DestroyRenderer(_renderer);
            SDL.SDL_DestroyWindow(_window);

            // Quit SDL
            SDL.SDL_Quit();

            GC.SuppressFinalize(this);
        }
    }
}
