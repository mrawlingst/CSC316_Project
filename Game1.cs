using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        double enemyLastFire;

        // Projectile
        Model projectileModel;
        List<Vector3> playerProjectiles;
        List<Vector3> enemyProjectiles;

        // Keyboard States
        KeyboardState prevKeyboardState;

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
            enemyLastFire = 0f;

            // Projectile
            playerProjectiles = new List<Vector3>();
            enemyProjectiles = new List<Vector3>();

            prevKeyboardState = Keyboard.GetState();

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

            projectileModel = Content.Load<Model>("Player_Mesh");
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

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space))
            {
                playerProjectiles.Add(playerPos);
            }

            if (gameTime.TotalGameTime.TotalMilliseconds - enemyLastFire >= 1000f)
            {
                enemyProjectiles.Add(enemyPos);
                enemyLastFire = gameTime.TotalGameTime.TotalMilliseconds;
            }

            updateProjectiles();
            updateHealth();

            prevKeyboardState = Keyboard.GetState();

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
            if (enemyCurrentHealth > 0)
            {
                world = Matrix.CreateScale(15, 15, 1) * Matrix.CreateTranslation(enemyPos);
                enemy.Draw(world, view, projection);
            }

            // Projectiles
            foreach (var proj in playerProjectiles)
            {
                world = Matrix.CreateScale(3, 3, 1) * Matrix.CreateTranslation(proj);
                projectileModel.Draw(world, view, projection);
            }
            foreach (var proj in enemyProjectiles)
            {
                world = Matrix.CreateScale(3, 3, 1) * Matrix.CreateTranslation(proj);
                projectileModel.Draw(world, view, projection);
            }

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
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
                //enemyCurrentHealth--;
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
                //enemyCurrentHealth++;

            // Player
            playerCurrentHealth = MathHelper.Clamp(playerCurrentHealth, 0, playerMaxHealth);
            playerHealthValue = (float)playerCurrentHealth / (float)playerMaxHealth;

            // Enemy
            enemyCurrentHealth = MathHelper.Clamp(enemyCurrentHealth, 0, enemyMaxHealth);
            enemyHealthValue = (float)enemyCurrentHealth / (float)enemyMaxHealth;
        }

        void updateProjectiles()
        {
            var direction = new Vector3(5, 0, 0);

            // player
            for (int i = 0; i < playerProjectiles.Count; i++)
            {
                playerProjectiles[i] += direction;

                if (playerProjectiles[i].X > 250)
                {
                    playerProjectiles.RemoveAt(i);
                    i--;
                }
            }

            // enemy
            for (int i = 0; i < enemyProjectiles.Count; i++)
            {
                enemyProjectiles[i] += -direction;

                if (enemyProjectiles[i].X < 0 )
                {
                    enemyProjectiles.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
