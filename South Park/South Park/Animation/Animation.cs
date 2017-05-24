using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    sealed class Animation
    {
       
        private Rectangle bounds;
      
       
        public Animation(Game game) 
        {
            this.Texture = null;
            this.Location = Vector2.Zero;
            this.Size = new System.Drawing.Size(80, 80);
            this.Frames = 1;
            this.Frequency = 10;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = 1f / this.Frequency;
            this.SpriteBounds = bounds = new Rectangle(0, 0, 0, 0);
            this.MirrorDisplay = false;
            this.Rotation = 0f;
            this.Scale = 1;
        }

        public Animation(Game game, Texture2D texture, System.Drawing.Size size, Vector2 location, int frequency, short frames)
        {
            this.Texture = texture;
            this.Location = location;
            this.Size = size;
            this.Frames = frames;
            this.Frequency = frequency;
            this.CurrentFrame = this.StartFrame = 0;
            this.TimeFrame = 1f / this.Frequency;
            this.SpriteBounds = bounds = new Rectangle(0, 0, 0, 0);
            this.MirrorDisplay = false;
            this.Rotation = 0f;
            this.Scale = 1;
        }



        /// <summary>
        /// Прямоугольник для задания позиции кадра в изображении
        /// </summary>
        public Rectangle SpriteBounds { get; set; }
        /// <summary>
        /// Размер прямоугольника кадра анимации
        /// </summary>
        public System.Drawing.Size Size { get; set; } 
        /// <summary>
        /// Время, которое прошло с начала отображения текущего кадра
        /// </summary>
        public float TotalElapsed { get; set; }
        /// <summary>
        /// Текстура
        /// </summary>
        public Texture2D Texture { get; set; }
        /// <summary>
        /// Глубина отрисовки
        /// </summary>
        public float LayerDepth { get; set; }
        /// <summary>
        /// Текущий кадр
        /// </summary>
        public int CurrentFrame { get; set; }
        /// <summary>
        /// Положение анимации
        /// </summary>
        public Vector2 Location { get; set; }
        /// <summary>
        /// Время отображения одного кадра 
        /// </summary>
        public float TimeFrame { get; set; }
        /// <summary>
        /// Начальный кадр
        /// </summary>
        public int StartFrame { get; set; }
        /// <summary>
        /// Направление
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// Частота кадров анимации
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// Колличество кадров анимации
        /// </summary>
        public short Frames { get; set; }

        public bool MirrorDisplay { get; set; }

        public float Rotation { get; set; }

        public short Scale { get; set; }

        public bool Rotate { get; set; }

        public Vector2 CenterRotate { get; set; }


        private void DrawMirrorMode(GameTime gameTime)
        {
            if (this.Rotate) SPHalper.SpriteBatch.Draw(this.Texture, this.Location, this.SpriteBounds, Color.White, this.Rotation, this.CenterRotate, this.Scale, SpriteEffects.FlipHorizontally, this.LayerDepth);
            else SPHalper.SpriteBatch.Draw(this.Texture, this.Location, this.SpriteBounds, Color.White, this.Rotation, Vector2.Zero, this.Scale, SpriteEffects.FlipHorizontally, this.LayerDepth);
        }

        private void DrawNormalMode(GameTime gameTime)
        {
            if (this.Rotate) SPHalper.SpriteBatch.Draw(this.Texture, this.Location, this.SpriteBounds, Color.White, this.Rotation, this.CenterRotate, this.Scale, SpriteEffects.None, this.LayerDepth);
            else SPHalper.SpriteBatch.Draw(this.Texture, this.Location, this.SpriteBounds, Color.White, this.Rotation, Vector2.Zero, this.Scale, SpriteEffects.None, this.LayerDepth);
               
        }




        public void Construct(Texture2D texture)
        {
            if (this.Texture.GetHashCode() != texture.GetHashCode()) this.Texture = texture;
        }

        public void Construct(Vector2 location)
        {
            if (this.Location != location) this.Location = location;
        }

        public void Construct(Texture2D texture, short frames)
        {
            if (this.Texture != texture) this.Texture = texture;
            if (this.Frames != frames) this.Frames = frames;
        }

        public void Construct(Texture2D texture, Vector2 location)
        {
            if (this.Texture != texture) this.Texture = texture;
            if (this.Location != location) this.Location = location;
        }

        public void Construct(Vector2 location, float layerDepth)
        {
            if (this.Location != location) this.Location = location;
            if (this.LayerDepth != layerDepth) this.LayerDepth = layerDepth;
        }

        public void Construct(Vector2 location, float layerDepth, float rotation)
        {
            if (this.Location != location) this.Location = location;
            if (this.LayerDepth != layerDepth) this.LayerDepth = layerDepth;
            if (this.Rotation != rotation) this.Rotation = rotation;
        }



        public void Construct(Texture2D texture, Vector2 location, float layerDepth)
        {
            if (this.Texture != texture) this.Texture = texture;
            if (this.Location != location) this.Location = location;
            if (this.LayerDepth != layerDepth) this.LayerDepth = layerDepth;
        }

        public void Construct(Texture2D texture, Vector2 location, short frames, float layerDepth)
        {
            if (this.Texture != texture) this.Texture = texture;
            this.Location = (this.Location != location) ? location : this.Location;
            this.LayerDepth = (this.LayerDepth != layerDepth) ? layerDepth : this.LayerDepth;
            this.Frames = (this.Frames != frames) ? frames : this.Frames;
        }


        public void Update(GameTime gameTime)
        {
            ChangeFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(GameTime gameTime)
        {
            if (this.MirrorDisplay) this.DrawMirrorMode(gameTime);
            else this.DrawNormalMode(gameTime);
        }

 





        public void ChangeFrame(float lastTimeFrame)
        {
            this.TotalElapsed += lastTimeFrame;

            if (this.TotalElapsed > this.TimeFrame)
            {
                if (this.CurrentFrame >= this.Frames - 1) this.CurrentFrame = this.StartFrame;
                else this.CurrentFrame++;

                bounds.X = this.Size.Width * this.CurrentFrame;
                bounds.Width = this.Size.Width;
                bounds.Height = this.Size.Height;

                this.SpriteBounds = bounds;
                this.TotalElapsed = 0;
            }
        } 
    }
} 