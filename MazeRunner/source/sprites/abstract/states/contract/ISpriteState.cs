﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.Sprites.States;

public interface ISpriteState
{
    public Texture2D Texture { get; }

    public int FrameSize { get; }

    public SpriteEffects FrameEffect { get; set; }

    public Rectangle CurrentAnimationFrame { get; }

    public ISpriteState ProcessState(GameTime gameTime);
}
