﻿using MazeRunner.Content;
using MazeRunner.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace MazeRunner.MazeBase.Tiles.States;

public abstract class BayonetTrapBaseState : MazeTrapBaseState
{
    public override Texture2D Texture => Textures.MazeTiles.MazeTraps.Bayonet;

    public override int FramesCount => 10;

    protected override double UpdateTimeDelayMs => 40;

    protected BayonetTrapBaseState(Hero hero, MazeTrap trap) : base(hero, trap)
    {
    }
}