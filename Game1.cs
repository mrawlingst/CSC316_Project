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

        // Player
        Model player;
        Vector3 playerPos;
        int playerCurrentHealth;
        int playerMaxHealth;
        float playerHealthValue;
        Texture2D playerHealthBar;

        // Enemy
        Model enemy;
        Vector3 enemyPos;
        int enemyCurrentHealth;
        int enemyMaxHealth;
        float enemyHealthValue;
        Texture2D enemyHealthBar;

        // Camera
        Vector3 camPos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            camPos = new Vector3(0, 0, 5);

            // Player
            playerPos = Vector3.Zero;
            playerCurrentHealth = playerMaxHealth = 100;
            playerHealthValue = (float)playerCurrentHealth / (float)playerMaxHealth;

            // Enemy
            enemyPos = new Vector3(250, 0, 0);
            enemyCurrentHealth = enemyMaxHealth = 100;
            enemyHealthValue = (float)enemyCurrentHealth / (float)enemyMaxHealth;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = Content.Load<Model>("Player_Mesh");
            playerHealthBar = Content.Load<Texture2D>("HealthBar");

            enemy = Content.Load<Model>("Player_Mesh");
            enemyHealthBar = Content.Load<Texture2D>("HealthBar");
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

            updateHealth();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix world = Matrix.Identity;
            Matrix view = Matrix.CreateLookAt(camPos, Vector3.Zero, Vector3.Up);
            Matrix projection = Matrix.CreateOrthographic(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.1f, 1000.0f);

            // Player
            world = Matrix.CreateScale(3, 3, 1) * Matrix.CreateTranslation(playerPos);
            player.Draw(world, view, projection);

            // Enemy
            world = Matrix.CreateScale(15, 15, 1) * Matrix.CreateTranslation(enemyPos);
            enemy.Draw(world, view, projection);

            spriteBatch.Begin();

            spriteBatch.Draw(playerHealthBar,
                new Rectangle(10, 10, (int)(200 * playerHealthValue), 25),
                new Rectangle(0, 0, (int)(playerHealthBar.Width * playerHealthValue), playerHealthBar.Height),
                Color.White);

            spriteBatch.Draw(enemyHealthBar,
                new Rectangle(300, 10, (int)(200 * enemyHealthValue), 25),
                new Rectangle(0, 0, (int)(enemyHealthBar.Width * enemyHealthValue), enemyHealthBar.Height),
                Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void updateHealth()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                enemyCurrentHealth--;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                enemyCurrentHealth++;

            // Player
            playerCurrentHealth = MathHelper.Clamp(playerCurrentHealth, 0, playerMaxHealth);
            playerHealthValue = (float)playerCurrentHealth / (float)playerMaxHealth;

            // Enemy
            enemyCurrentHealth = MathHelper.Clamp(enemyCurrentHealth, 0, enemyMaxHealth);
            enemyHealthValue = (float)enemyCurrentHealth / (float)enemyMaxHealth;
        }
    }
}
