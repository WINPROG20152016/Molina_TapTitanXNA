﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TapTitanXNA_JuliusMolina
{
    public enum frames
    {
        hero_idle = 2,
        hero_att = 3,
        support1 = 3
    }

    public class Hero
    {
        #region Properties
        Vector2 playerPosition;
        Texture2D player;
        ContentManager content;
        Level level;

        string name;
        bool supp1;
        int suppatk=0;
        int heroatk=0;

        Animation idleAnimation;
        Animation attackAnimation;
        AnimationPlayer spritePlayer;

        #endregion

        public Hero(ContentManager content, Level level, string name)
        {
            this.content = content;
            this.level = level;
            this.name = name;
        }

        public void LoadContent()
        {
            string imageIdle = "";
            string imageAttack = "";
            float positionAdjustX = 0.0f;
            float positionAdjustY = 0.0f;
            int idleFrames = 1;
            int attackFrames = 1;

            switch (name)
            {
                case "hero":
                    imageIdle = "HeroSprite/heroIdle_3";
                    imageAttack = "HeroSprite/attack1";
                    positionAdjustX = -170.0f;
                    positionAdjustY = 20.0f;
                    idleFrames = (int)frames.hero_idle;
                    attackFrames = (int)frames.hero_att;
                    heroatk = 5;
                    break;

                case "support1":
                    imageIdle = "SupportSprite/Support_Air1";
                    positionAdjustX = -99.0f;
                    positionAdjustY = -160.0f;
                    idleFrames = (int)frames.support1;
                    supp1 = false;
                    suppatk = 3;
                    break;
            }

            player = content.Load<Texture2D>(imageIdle);
            idleAnimation = new Animation(player, 0.1f, true, idleFrames);

            if (name == "hero")
            {
                attackAnimation = new Animation(content.Load<Texture2D>(imageAttack), 0.2f, false, attackFrames);
            }

            int positionX = (Level.windowWidth / 2) - (idleAnimation.FrameWidth / 4);
            int positionY = (Level.windowHeight / 2) - (idleAnimation.FrameHeight / 4);
            playerPosition = new Vector2((float)positionX + positionAdjustX, (float)positionY + positionAdjustY);

            spritePlayer.PlayAnimation(idleAnimation);
        }

        public void Update(GameTime gameTime)
        {
            if (name == "hero" && level.mouseState.LeftButton == ButtonState.Pressed &&
                level.oldMouseState.LeftButton == ButtonState.Released)
            {
                //playerPosition.X++;
                spritePlayer.PlayAnimation(attackAnimation);
            }
            else if (name == "hero" && spritePlayer.FrameIndex == (int)frames.hero_att - 1)
                spritePlayer.PlayAnimation(idleAnimation);
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
        public int heroATK
        {
            set { heroatk = value; }
            get { return heroatk; }
        }

        public int suppATK
        {
            set { suppatk = value; }
            get { return suppatk; }
        }
    }

}
