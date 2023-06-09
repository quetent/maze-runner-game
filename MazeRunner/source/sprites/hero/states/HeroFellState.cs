﻿using MazeRunner.MazeBase;
using Microsoft.Xna.Framework;

namespace MazeRunner.Sprites.States;

public class HeroFellState : HeroFallBaseState
{
    public override double UpdateTimeDelayMs => double.MaxValue;

    public HeroFellState(ISpriteState previousState, Hero hero, Maze maze) : base(previousState, hero, maze)
    {

        var framePosX = (FramesCount - 1) * FrameSize;

        CurrentAnimationFramePoint = new Point(framePosX, 0);
    }

    public override ISpriteState ProcessState(GameTime gameTime)
    {
        return this;
    }
}
