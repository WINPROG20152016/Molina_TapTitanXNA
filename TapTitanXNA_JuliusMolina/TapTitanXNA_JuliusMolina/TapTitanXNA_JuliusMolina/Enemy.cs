using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_JuliusMolina
{
    public class Enemy
    {
        #region Properties
        Vector2 playerPosition;
        Texture2D player;
        ContentManager content;
        Level level;

        Animation idleAnimation;
        Animation attackAnimation;
        Animation deadAnimation;
        AnimationPlayer spritePlayer;

        string name;
        int lifePoints;
        int idleFrames = 1;
        int attackFrames = 1;
        int deadFrames = 1;
        #endregion

        public Enemy(ContentManager content, Level level, string name)
        {
            this.content = content;
            this.level = level;
            this.name = name;
        }

        public void LoadContent()
        {
            string imageIdle = "";
            string imageAttack = "";
            string imageDead = "";
            float positionAdjustX = 0.0f;
            float positionAdjustY = 0.0f;
            
            switch (name)
            {
                case "snowman":
                    imageIdle = "EnemySprite/EnemyIdle";
                    imageAttack = "EnemySprite/EnemyAttacked";
                    imageDead = "EnemySprite/EnemyDead";
                    positionAdjustX = 250.0f;
                    positionAdjustY = -130.0f;
                    idleFrames = 4;
                    attackFrames = 3;
                    deadFrames = 4;
                    lifePoints = 100;
                    break;

                case "shroom":
                    imageIdle = "EnemySprite/Enemy2Idle";
                    imageAttack = "EnemySprite/Enemy2Attacked";
                    imageDead = "EnemySprite/Enemy2Dead";
                    positionAdjustX = 220.0f;
                    positionAdjustY = -130.0f;
                    idleFrames = 4;
                    attackFrames = 1;
                    deadFrames = 5;
                    lifePoints = 250;
                    break;

            }
            player = content.Load<Texture2D>(imageIdle);

            idleAnimation = new Animation(player, 0.1f, true, idleFrames);
            attackAnimation = new Animation(content.Load<Texture2D>(imageAttack), 0.1f, false, attackFrames);
            deadAnimation = new Animation(content.Load<Texture2D>(imageDead), 0.1f, false, deadFrames);

            int positionX = (Level.windowWidth / 2) - (player.Width / 4);
            int positionY = (Level.windowHeight / 2) - (player.Height / 4);
            playerPosition = new Vector2((float)positionX + positionAdjustX, (float)positionY + positionAdjustY);

            spritePlayer.PlayAnimation(idleAnimation);
        }

        public void Update(GameTime gameTime)
        {
            if ( lifePoints> 0)
            {
                if (level.mouseState.LeftButton == ButtonState.Pressed &&
                   level.oldMouseState.LeftButton == ButtonState.Released)
                {
                    //playerPosition.X++;
                   spritePlayer.PlayAnimation(attackAnimation);
                }
                else if (spritePlayer.FrameIndex == attackFrames-1)
                {
                    spritePlayer.PlayAnimation(idleAnimation);
                }

            }
            else
            {
                spritePlayer.PlayAnimation(deadAnimation);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //    spriteBatch.Draw(player,
            //        playerPosition,
            //        null,
            //        Color.White,
            //        0.0f,
            //        Vector2.Zero,
            //        0.5f,
            //        SpriteEffects.None,
            //        0.0f);

            spritePlayer.Draw(gameTime, spriteBatch, playerPosition, SpriteEffects.None);
        }

        public int LifePoints
        {
            set { lifePoints = value; }
            get { return lifePoints; }
        }
    }
}
