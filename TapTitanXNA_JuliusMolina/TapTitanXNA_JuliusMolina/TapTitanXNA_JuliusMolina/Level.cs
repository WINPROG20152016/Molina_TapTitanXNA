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
        int level = 0;
        int currentEnemyHP;
        int atkdmg;

        Hero hero;
        Hero support1;
        Enemy enemy1;
        Enemy enemy2;

        int supporttts = 60;

        SpriteFont damageStringFont;
        //int damageNumber = 0;

        Button playButton;
        Button attackButton;

        #endregion

        public Level(ContentManager content)
        {
            this.content = content;

            enemy1 = new Enemy(content, this, "snowman");
            enemy2 = new Enemy(content, this, "shroom");
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
            enemy1.LoadContent();
            enemy2.LoadContent();
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
            enemy1.Update(gameTime);
            enemy2.Update(gameTime);
            support1.Update(gameTime);

            oldMouseState = mouseState;

            atkdmg=hero.heroATK;

            if (attackButton.Update(gameTime, mouseX, mouseY,
                            mpressed, prev_mpressed))
            {
                currentEnemyHP-=atkdmg;
            }

            if (supporttts < 1)
            {
                currentEnemyHP-=support1.suppATK;
                supporttts = 60;
            }
            else
            {
                supporttts--;
            }

            if (currentEnemyHP < 1)
            {
                level++;

                switch (level)
                {
                    case 1:
                        currentEnemyHP=enemy1.LifePoints;
                        break;
                    case 2:
                        currentEnemyHP=enemy2.LifePoints;
                        break;
                    default:
                        break;
                }
            }

            switch (level)
            {
                case 1:
                    enemy1.Update(gameTime);
                    break;
                case 2:
                    enemy2.Update(gameTime);
                    break;
                default:
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
      //      enemy1.Draw(gameTime, spriteBatch);
      //      enemy2.Draw(gameTime, spriteBatch);

            support1.Draw(gameTime, spriteBatch);

            switch (level)
            {
                case 1:
                    enemy1.Draw(gameTime, spriteBatch);
                    break;
                case 2:
                    enemy2.Draw(gameTime, spriteBatch);
                    break;
                default:
                    break;
            }

            hero.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(damageStringFont, "HP = " + currentEnemyHP, Vector2.Zero, Color.Red);

            //playButton.Draw(gameTime, spriteBatch);
            attackButton.Draw(gameTime, spriteBatch);
        }
    }
}
