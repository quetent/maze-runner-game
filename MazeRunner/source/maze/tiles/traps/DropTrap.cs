﻿using MazeRunner.Helpers;
using MazeRunner.MazeBase.Tiles.States;
using MazeRunner.Sprites;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace MazeRunner.MazeBase.Tiles;

public class DropTrap : MazeTrap
{
    private const float HitBoxOffset = 2;

    private const float HitBoxSize = 12;

    public override bool IsActivated => State is DropTrapActivatedState;

    public override TileType TileType => TileType.Trap;

    public override TrapType TrapType => TrapType.Drop;

    public DropTrap(Hero hero)
    {
        State = new DropTrapDeactivatedState(hero, this);
    }

    public override RectangleF GetHitBox(Vector2 position)
    {
        return HitBoxHelper.GetHitBox(position, HitBoxOffset, HitBoxOffset, HitBoxSize, HitBoxSize);
    }
}