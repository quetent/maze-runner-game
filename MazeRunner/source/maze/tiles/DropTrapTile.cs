﻿#region Usings
using Microsoft.Xna.Framework.Graphics;
using System;
#endregion

namespace MazeRunner;

public class DropTrapTile : MazeTrap
{
    private static readonly Random _random = new();

    public override Texture2D Texture
    {
        get
        {
            return TilesTextures.DropTrap;
        }
    }

    public override TileType TileType
    {
        get
        {
            return TileType.DropTrap;
        }
    }

    protected override int FramesCount
    {
        get
        {
            return 8;
        }
    }

    protected override double ActivateChance
    {
        get
        {
            return 1e-1;
        }
    }

    protected override double DeactivateChance
    {
        get
        {
            return 1e-2;
        }
    }

    protected override Random Random
    {
        get
        {
            return _random;
        }
    }

    protected override int AnimationFrameDelayMs
    {
        get
        {
            return 35;
        }
    }

    protected override int CurrentAnimationFrameX { get; set; } = 0;

    protected override double ElapsedGameTime { get; set; } = 0;

    protected override TrapCondition Condition { get; set; } = TrapCondition.Inactive;
}