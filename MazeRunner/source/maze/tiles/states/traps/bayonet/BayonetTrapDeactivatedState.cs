﻿using MazeRunner.Helpers;
using Microsoft.Xna.Framework;

namespace MazeRunner.MazeBase.Tiles.States;

public class BayonetTrapDeactivatedState : BayonetTrapBaseState
{
    public BayonetTrapDeactivatedState(MazeTrap trap)
    {
        Trap = trap;
    }

    public override IMazeTileState ProcessState(GameTime gameTime)
    {
        if (RandomHelper.RollChance(Trap.DeactivateChance))
        {
            return new BayonetTrapActivatingState(Trap);
        }

        return this;
    }
}