﻿using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Collections.Immutable;
using static MazeRunner.Content.Textures.MazeTiles.MazeItems.Chalk;

namespace MazeRunner.MazeBase.Tiles.States;

public abstract class ChalkBaseState : MazeItemBaseState
{
    protected static readonly IDictionary<Texture2D, float> TextureChancePairs;

    static ChalkBaseState()
    {
        TextureChancePairs = new Dictionary<Texture2D, float>
        {
            { Chalk_1, .25f },
            { Chalk_2, .25f },
            { Chalk_3, .25f },
            { Chalk_4, .25f },
        }
        .ToImmutableDictionary();
    }

    public override int FramesCount => 1;

    protected override double UpdateTimeDelayMs => double.MaxValue;
}