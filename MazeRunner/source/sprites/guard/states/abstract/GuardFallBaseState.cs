﻿using MazeRunner.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.Sprites.States;

public abstract class GuardFallBaseState : GuardBaseState
{
    public GuardFallBaseState(ISpriteState previousState) : base(previousState)
    {
    }

    public override Texture2D Texture
    {
        get
        {
            return Textures.Sprites.Guard.Fall;
        }
    }

    public override int FramesCount
    {
        get
        {
            return 4;
        }
    }
}