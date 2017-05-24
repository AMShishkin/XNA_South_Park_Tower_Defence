//
//-----------------------------------------------------------
//----------х Класс реализующий поведение камеры. х----------
//-----------------------------------------------------------
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class Camera2D
    {
        private static Matrix matrixTransform; // Матрица
        private static Vector2 cLocation; // Локация камеры
        private static float cRotation; // Поворот камеры
        private static float cZoom; // Зум камеры

        /// <summary>
        /// Возвращает преобразованную матрицу
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <returns></returns>
        public static Matrix MatrixTransformation(GraphicsDevice graphicsDevice)
        {
            matrixTransform = Matrix.CreateTranslation(new Vector3(-cLocation.X, -cLocation.Y, 0)) *
                                                       Matrix.CreateRotationZ(cRotation) *
                                                       Matrix.CreateScale(new Vector3(cZoom, cZoom, 1)) *
                                                       Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.001f,
                                                       graphicsDevice.Viewport.Height * 0.001f, 0));
            return matrixTransform;
        }
        /// <summary>
        /// Передвижение
        /// </summary>
        /// <param name="location">Позиция</param>
        public static void Move(Vector2 location)
        {
            cLocation += location;
        }
        /// <summary>
        /// Локация
        /// </summary>
        public static Vector2 Location
        {
            get { return cLocation; }
            set { cLocation = value; }
        }
        /// <summary>
        /// Вращение
        /// </summary>
        public static float Rotation
        {
            get { return cRotation; }
            set { cRotation = value; }
        }
        /// <summary>
        /// Зум
        /// </summary>
        public static float Zoom
        {
            get { return cZoom; }
            set { cZoom = value; if (cZoom < 0.1f) cZoom = 0.1f; } 
        }
    }
}