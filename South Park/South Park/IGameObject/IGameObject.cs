// Интерфейс игровых объектов 

using Microsoft.Xna.Framework;

namespace South_Park
{
    interface IGameObject
    {
        // Позиция
        Vector2 Location { get; set; }
        // Состояние доступности
        bool Enabled { get; set; }
        // Глубина отрисовки
        float LayerDepth { get; set; }

        System.Drawing.Size Size();

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime);

        bool CheckCollision(Rectangle mask);
   
        Rectangle GetBounds(); 
    }
}