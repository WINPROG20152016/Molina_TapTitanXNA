using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_JuliusMolina
{

    public class Level
    {
        public static int windowWidth = 400;
        public static int windowHeight = 500;

        #region Properties
        ContentManager content;

        Texture2D background;
        public MouseState oldMouseState;
        public MouseState mouseState;
        bool mpressed, prev_mpressed = false;
        int mouseX, mouseY;

        Hero hero;
        Hero support1;
        Enemy enemy;

        SpriteFont damageStringFont;
        //int damageNumber = 0;

        Button playButton;
        Button attackButton;

        #endregion

        public Level(ContentManager content)
        {
            this.content = content;

            enemy = new Enemy(content, this, "snowman");
            hero = new Hero(content, this, "hero");
            support1 = new Hero(content, this, "support1");
        }

        public void LoadContent()
        {
            background = content.Load<Texture2D>("BackgroundSprite/bg");
            damageStringFont = content.Load<SpriteFont>("Font");

            playButton = new Button(content, "button", Vector2.Zero);
            attackButton = new Button(content, "abutton", new Vector2(100, 350));
            hero.LoadContent();
            enemy.LoadContent();
            support1.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;
            prev_mpressed = mpressed;
            mpressed = mouseState.LeftButton == ButtonState.Pressed;

            hero.Update(gameTime);
            enemy.Update(gameTime);
            support1.Update(gameTime);

            oldMouseState = mouseState;

            if (attackButton.Update(gameTime, mouseX, mouseY,
                            mpressed, prev_mpressed))
            {
                enemy.LifePoints--;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            enemy.Draw(gameTime, spriteBatch);
            hero.Draw(gameTime, spriteBatch);
            support1.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(damageStringFont, "HP = " + enemy.LifePoints, Vector2.Zero, Color.Red);

            //playButton.Draw(gameTime, spriteBatch);
            attackButton.Draw(gameTime, spriteBatch);
        }
    }
}
