﻿using MazeRunner.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.Sprites.States;

public class HeroIdleState : HeroBaseState
{
    public override Texture2D Texture
    {
        get
        {
            return Textures.Sprites.Hero.Idle;
        }
    }

    public override int FramesCount
    {
        get
        {
            return 2;
        }
    }

    public override double UpdateTimeDelayMs
    {
        get
        {
            return 300;
        }
    }
}
