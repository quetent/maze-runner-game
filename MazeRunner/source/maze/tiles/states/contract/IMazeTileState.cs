﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.MazeBase.Tiles.States;

public interface IMazeTileState
{
    public Texture2D Texture { get; }

    public int FrameSize { get; }

    public Rectangle CurrentAnimationFrame { get; }

    public IMazeTileState ProcessState(GameTime gameTime);
}