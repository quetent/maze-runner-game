﻿using Microsoft.Xna.Framework;

namespace MazeRunner.MazeBase.Tiles.States;

public class ExitClosedState : ExitBaseState
{
    public override IMazeTileState ProcessState(GameTime gameTime)
    {
        return this;
    }
}