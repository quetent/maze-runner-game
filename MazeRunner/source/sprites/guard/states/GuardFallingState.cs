﻿using Microsoft.Xna.Framework;

namespace MazeRunner.Sprites.States;

public class GuardFallingState : GuardFallBaseState
{
    public override double UpdateTimeDelayMs => 75;

    public GuardFallingState(ISpriteState previousState) : base(previousState)
    {
    }

    public override ISpriteState ProcessState(GameTime gameTime)
    {
        ElapsedGameTimeMs += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (ElapsedGameTimeMs > UpdateTimeDelayMs)
        {
            var animationPoint = CurrentAnimationFramePoint;

            if (animationPoint.X == (FramesCount - 1) * FrameSize)
            {
                return new GuardFalledState(this);
            }

            var framePosX = animationPoint.X + FrameSize;

            CurrentAnimationFramePoint = new Point(framePosX, 0);
            ElapsedGameTimeMs -= UpdateTimeDelayMs;
        }

        return this;
    }
}
