﻿using MazeRunner.Content;
using MazeRunner.Helpers;
using MazeRunner.Managers;
using MazeRunner.MazeBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.Sprites.States;

public class GuardIdleState : GuardBaseState
{
    private readonly ISpriteState _previousState;

    private bool IsAttackOnCooldown => _previousState is GuardAttackState;

    public override Texture2D Texture => Textures.Sprites.Guard.Idle;

    public override int FramesCount => 4;

    public override double UpdateTimeDelayMs => 650;

    public GuardIdleState(ISpriteState previousState, Hero hero, Guard guard, Maze maze) : base(previousState, hero, guard, maze)
    {
        _previousState = previousState;

        if (_previousState is GuardChaseState or GuardWalkState)
        {
            SoundManager.Sprites.Guard.PauseRunSoundIfPlaying(Guard);
        }
    }

    public GuardIdleState(Hero hero, Guard guard, Maze maze) : this(null, hero, guard, maze)
    {
    }

    public override ISpriteState ProcessState(GameTime gameTime)
    {
        if (CollidesWithTraps(Guard, Maze, true, out var trapType))
        {
            SoundManager.Sprites.Guard.PlayTrapDeathSound(trapType, GetDistanceToHero());

            return GetTrapCollidingState(trapType);
        }

        if (IsHeroNearby(out var _))
        {
            return new GuardChaseState(this, Hero, Guard, Maze, IsAttackOnCooldown);
        }

        if (CollidesWithTraps(Guard, Maze, false, out var _))
        {
            return new GuardWalkState(this, Hero, Guard, Maze, GuardWalkState.TrapEscapePathLength);
        }

        var elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;

        ElapsedGameTimeMs += elapsedTime;

        if (ElapsedGameTimeMs > UpdateTimeDelayMs)
        {
            var animationPoint = CurrentAnimationFramePoint;
            var framePosX = (animationPoint.X + FrameSize) % (FrameSize * FramesCount);

            if (framePosX == FrameSize * 3)
            {
                SoundManager.Sprites.Guard.PlaySwordFellSound(GetDistanceToHero());
            }

            if (framePosX is 0 && animationPoint.X == (FramesCount - 1) * FrameSize && RandomHelper.RandomBoolean())
            {
                return new GuardWalkState(this, Hero, Guard, Maze);
            }

            CurrentAnimationFramePoint = new Point(framePosX, 0);

            ElapsedGameTimeMs -= UpdateTimeDelayMs;
        }

        return this;
    }
}
