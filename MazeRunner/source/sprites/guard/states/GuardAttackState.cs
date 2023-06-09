﻿using MazeRunner.Content;
using MazeRunner.Managers;
using MazeRunner.MazeBase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.Sprites.States;

public class GuardAttackState : GuardBaseState
{
    public const double AttackDelayMs = 850;

    private bool _isAttackOnCooldown;

    private bool _attackMissedSoundPlayed;

    public GuardAttackState(
        ISpriteState previousState, Hero hero, Guard guard, Maze maze, bool isOnCooldown, double cooldownTimeCounter)
        : base(previousState, hero, guard, maze)
    {
        _isAttackOnCooldown = isOnCooldown;

        if (_isAttackOnCooldown)
        {
            ElapsedGameTimeMs += cooldownTimeCounter;
        }

        SoundManager.Sprites.Guard.PauseRunSoundIfPlaying(Guard);
    }

    public override Texture2D Texture => Textures.Sprites.Guard.Attack;

    public override int FramesCount => 7;

    public override double UpdateTimeDelayMs => 100;

    public override ISpriteState ProcessState(GameTime gameTime)
    {
        if (CollidesWithTraps(Guard, Maze, true, out var trapType))
        {
            SoundManager.Sprites.Guard.PlayTrapDeathSound(trapType, GetDistanceToHero());

            return GetTrapCollidingState(trapType);
        }

        ElapsedGameTimeMs += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (_isAttackOnCooldown && ElapsedGameTimeMs > AttackDelayMs)
        {
            _isAttackOnCooldown = false;

            ElapsedGameTimeMs -= AttackDelayMs;
        }

        if (!_isAttackOnCooldown)
        {
            ElapsedGameTimeMs += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ElapsedGameTimeMs > UpdateTimeDelayMs)
            {
                var animationPoint = CurrentAnimationFramePoint;

                if (animationPoint.X == (FramesCount - 1) * FrameSize)
                {
                    return new GuardIdleState(this, Hero, Guard, Maze);
                }

                var framePosX = animationPoint.X + FrameSize;

                CurrentAnimationFramePoint = new Point(framePosX, 0);
                ElapsedGameTimeMs -= UpdateTimeDelayMs;
            }

            DamageHeroIfNeededAsync();

            FaceToHero();
        }

        return this;
    }

    private void DamageHeroIfNeededAsync()
    {
        if (!Hero.IsDead && !Hero.IsTakingDamage)
        {
            if (Vector2.Distance(Hero.Position, Guard.Position) < Guard.ElongatedAttackDistance)
            {
                SoundManager.Sprites.Guard.PlayAttackHitSound();

                Hero.TakeDamage(Guard.Damage);
            }
            else
            {
                if (!_attackMissedSoundPlayed)
                {
                    _attackMissedSoundPlayed = true;

                    SoundManager.Sprites.Guard.PlayAttackMissedSound();
                }
            }
        }
    }

    private void FaceToHero()
    {
        var turnDirectionX = (Hero.Position - Guard.Position).X;
        var materialMovement = new Vector2(turnDirectionX * float.Epsilon, 0);

        ProcessFrameEffect(materialMovement);
    }
}
