using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CSC316_Project
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model player;
        Vector3 playerPos;
        Vector3 camPos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camPos = new Vector3(0, 0, 5);
            playerPos = Vector3.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = Content.Load<Model>("Player_Mesh");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W) && playerPos.Y < 230)
                playerPos += new Vector3(0, 1, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.S) && playerPos.Y > -230)
                playerPos -= new Vector3(0, 1, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.A) && playerPos.X > -390)
                playerPos -= new Vector3(1, 0, 0);

            if (Keyboard.GetState().IsKeyDown(Keys.D) && playerPos.X < 390)
                playerPos += new Vector3(1, 0, 0);

            Console.WriteLine(playerPos);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix world = Matrix.Identity;
            Matrix view = Matrix.CreateLookAt(camPos, Vector3.Zero, Vector3.Up);
            Matrix projection = Matrix.CreateOrthographic(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.1f, 1000.0f);

            world = Matrix.CreateScale(3, 3, 1) * Matrix.CreateTranslation(playerPos);
            player.Draw(world, view, projection);

            base.Draw(gameTime);
        }
    }
}
