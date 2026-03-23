using System;

namespace Framework.Engine
{
    public abstract class GameObject
    {
        public string Name { get; set; } = " ";
        public bool IsActive { get; set; } = true;
        public bool IsDestroy = false;
        public Scene Scene { get; }
        protected string[] _currentPixels;
        protected bool _pixelReversed;

        public Player PlayerReferance;


        public virtual Point Position{ get; set; }
        private (int X, int Y) _direction = (1, 0);
        public Point Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
            }
        }



        protected GameObject(Scene scene, Point position)
        {
            Scene = scene;
            Position = position;
        }


        protected virtual void Destroy()
        {
            IsDestroy = true;
            Scene.RemoveGameObject(this);
        }


        public virtual void DrawEntity(ScreenBuffer buffer)
        {
            int width = _currentPixels[0].Length;
            int height = _currentPixels.Length;

            int n = _pixelReversed ? -1 : 1;

            for (int j = 0; j < height; j++)
            {
                int sourceY = (height - 1) - j;

                for (int i = 0; i < width; i++)
                {
                    char pixel = _currentPixels[sourceY][i];
                    if (pixel == ' ' || pixel == '\0') continue;

                    ConsoleColor color = GetColor(pixel);

                    int relX = i;
                    int relY = j;

                    int drawX = 0;
                    int drawY = 0;

                    switch ((Direction.X, Direction.Y))
                    {
                        case (1, 0):
                            drawX = relX;
                            drawY = relY * n;
                            break;

                        case (-1, 0):
                            drawX = (width - 1) - relX;
                            drawY = relY * n;
                            break;

                        case (0, 1):
                            drawX = relY * n;
                            drawY = relX;
                            break;

                        case (0, -1):
                            drawX = relY * n;
                            drawY = (width - 1) - relX;
                            break;
                    }

                    buffer.SetCell(Position + new Point(drawX, drawY), color);
                }
            }
        }
        public ConsoleColor GetColor(char c)
        {
            switch (c)
            {
                case 'C':
                    return ConsoleColor.Cyan;
                case 'D':
                    return ConsoleColor.DarkBlue;
                case 'B':
                    return ConsoleColor.Black;
                case 'G':
                    return ConsoleColor.DarkGray;
                case 'R':
                    return ConsoleColor.Red;
                case 'r':
                    return ConsoleColor.DarkRed;
                case 'Y':
                    return ConsoleColor.DarkYellow;
                case 'y':
                    return ConsoleColor.Yellow;
                case 'g':
                    return ConsoleColor.DarkGreen;
                case 'W':
                default:
                    return ConsoleColor.White;
            }
        }
        public abstract void Update(float deltaTime);
        public virtual void Draw(ScreenBuffer buffer)
        {
            if (_currentPixels != null)
            {
                DrawEntity(buffer);
            }
        }
    }
}
