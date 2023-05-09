﻿using Microsoft.Xna.Framework;

namespace MazeRunner.MazeBase.Tiles.States;

public class DropTrapActivatingState : DropTrapBaseState
{
    public override IMazeTileState ProcessState(GameTime gameTime)
    {
        var elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;

        ElapsedGameTimeMs += elapsedTime;

        if (ElapsedGameTimeMs > UpdateTimeDelayMs)
        {
            CurrentAnimationFramePointX += FrameWidth;

            if (CurrentAnimationFramePointX == (FramesCount - 1) * FrameWidth)
            {
                return new DropTrapActivatedState();
            }

            ElapsedGameTimeMs -= elapsedTime;
        }

        return this;
    }
}