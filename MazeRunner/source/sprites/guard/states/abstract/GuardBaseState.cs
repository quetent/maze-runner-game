﻿using MazeRunner.MazeBase;
using MazeRunner.MazeBase.Tiles;
using MazeRunner.Wrappers;
using Microsoft.Xna.Framework;
using System;

namespace MazeRunner.Sprites.States;

public abstract class GuardBaseState : SpriteBaseState
{
    protected GuardBaseState(ISpriteState previousState) : base(previousState)
    {
    }

    protected static bool IsHeroNearby(SpriteInfo heroInfo, SpriteInfo guardInfo)
    {
        const float detectionDistanceCoeff = 3;

        var distance = Vector2.Distance(heroInfo.Position, guardInfo.Position);

        return distance <= guardInfo.Sprite.FrameSize * detectionDistanceCoeff;
    }

    protected override GuardBaseState GetTrapCollidingState(TrapType trapType)
    {
        return trapType switch
        {
            TrapType.Drop => new GuardFallingState(this),
            TrapType.Bayonet => new GuardDyingState(this),
            _ => throw new NotImplementedException()
        };
    }

    protected static Vector2 GetSpriteNormalizedPosition(SpriteInfo spriteInfo)
    {
        var hitBox = spriteInfo.Sprite.GetHitBox(spriteInfo.Position);
        var position = new Vector2(hitBox.X + hitBox.Width / 2, hitBox.Y + hitBox.Height / 2);

        return position;
    }

    protected static Cell GetSpriteCell(SpriteInfo spriteInfo, Maze maze)
    {
        var position = GetSpriteNormalizedPosition(spriteInfo);
        var cell = maze.GetCellByPosition(position);

        return cell;
    }
}