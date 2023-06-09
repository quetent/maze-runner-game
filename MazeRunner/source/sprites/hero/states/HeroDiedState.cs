﻿using MazeRunner.Managers;
using MazeRunner.MazeBase;
using MazeRunner.MazeBase.Tiles;
using Microsoft.Xna.Framework;

namespace MazeRunner.Sprites.States;

public class HeroDiedState : HeroDeathBaseState
{
    public override double UpdateTimeDelayMs => double.MaxValue;

    public HeroDiedState(ISpriteState previousState, Hero hero, Maze maze) : base(previousState, hero, maze)
    {
        var framePosX = (FramesCount - 1) * FrameSize;

        CurrentAnimationFramePoint = new Point(framePosX, 0);
    }

    public override ISpriteState ProcessState(GameTime gameTime)
    {
        if (CollidesWithTraps(Hero, Maze, true, out var trapType))
        {
            if (trapType is TrapType.Drop)
            {
                SoundManager.Sprites.Common.PlayAbyssFallSound(0);

                return new HeroFallingState(this, Hero, Maze);
            }
        }

        return this;
    }
}