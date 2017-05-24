using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    static class SPHalper
    {
        public static Game Game { get; set; }

        public static bool EditorState { get; set; }

        public static string GameState { get; set; }

        public static Effect Effect { get; set; }

        public static GameTime GameTime { get; set; }

        public static RenderTarget2D RenderTarget { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static ContentManager ContentManager { get; private set; }

        public static void Initialize(Game game, RenderTarget2D renderTarget, SpriteBatch spriteBatch, ContentManager contentManager)
        {
            Game = game;
            RenderTarget = renderTarget;
            SpriteBatch = spriteBatch;
            ContentManager = contentManager;
        }
    }
}
