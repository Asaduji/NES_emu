using SDL2;

namespace NES_emu.NES
{
    public class NesRenderer : IDisposable
    {
        //Renderer pointers
        private readonly IntPtr _window = IntPtr.Zero;
        private readonly IntPtr _renderer = IntPtr.Zero;
        private IntPtr _pixels = IntPtr.Zero;

        //Window
        private int _scale = 1;
        private readonly int _height = 256;
        private readonly int _width = 240;

        public NesRenderer(int scale = 1)
        {
            //Set scale first
            _scale = Math.Clamp(scale, 1, 5);

            // Initialize SDL
            _ = SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            // Create a window
            _window = SDL.SDL_CreateWindow("NES Emulator", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, _height * _scale, _width * _scale, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            // Create a renderer
            _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            // Allocate first pixel buffer
            _pixels = SDL.SDL_CreateTexture(_renderer, SDL.SDL_PIXELFORMAT_ARGB8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, _height * _scale, _width * _scale);
        }

        public void SetScale(int scale)
        {
            // Ignore if current scale is the same or no renderer
            if (scale == _scale || _renderer == IntPtr.Zero || _window == IntPtr.Zero)
            {
                return;
            }
            _scale = Math.Clamp(scale, 1, 5);

            SDL.SDL_DestroyTexture(_pixels);

            // Allocate new pixel buffer
            _pixels = SDL.SDL_CreateTexture(_renderer, SDL.SDL_PIXELFORMAT_ARGB8888, (int)SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING, _height * _scale, _width * _scale);

            SDL.SDL_SetWindowSize(_window, _width * _scale, _height * _scale);
        }

        public void RenderFrame()
        {
            // Update the texture with pixel data
            _ = SDL.SDL_UpdateTexture(_pixels, IntPtr.Zero, IntPtr.Zero, _height * 4 * _scale);

            // Clear the renderer
            _ = SDL.SDL_RenderClear(_renderer);

            // Render the texture
            _ = SDL.SDL_RenderCopy(_renderer, _pixels, IntPtr.Zero, IntPtr.Zero);

            // Update the screen
            SDL.SDL_RenderPresent(_renderer);
        }

        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            //Ignore if out of bounds
            if (x < 0 || x > _height || y < 0 || y > _width)
            {
                return;
            }

            _ = SDL.SDL_LockTexture(_pixels, IntPtr.Zero, out var bufferPtr, out var pitch);

            int scaledX = x * _scale;
            int scaledY = y * _scale;

            // Calculate the starting index of the block of pixels
            int startIndex = (scaledY * (_height * _scale) + scaledX) * 4;

            unsafe
            {
                byte* buffer = (byte*)bufferPtr;

                // Set the color of the block of 4 pixels
                for (int i = 0; i < _scale; i++)
                {
                    for (int j = 0; j < _scale; j++)
                    {
                        int index = startIndex + (i * (_height * _scale) + j) * 4;

                        buffer[index + 0] = b;  // B
                        buffer[index + 1] = g;  // G
                        buffer[index + 2] = r;  // R
                        buffer[index + 3] = 255;  // A
                    }
                }
            }

            SDL.SDL_UnlockTexture(_pixels);
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
