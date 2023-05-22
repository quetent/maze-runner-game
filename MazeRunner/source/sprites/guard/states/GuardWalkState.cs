﻿using MazeRunner.Helpers;
using MazeRunner.Managers;
using MazeRunner.MazeBase;
using MazeRunner.MazeBase.Tiles;
using MazeRunner.Wrappers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace MazeRunner.Sprites.States;

public class GuardWalkState : GuardMoveBaseState
{
    private const int WalkPathMinLength = 3;
    private const int WalkPathMaxLength = 6;

    private readonly SpriteInfo _heroInfo;

    private readonly SpriteInfo _guardInfo;
    private readonly MazeInfo _mazeInfo;

    private readonly LinkedList<Vector2> _walkPath;

    public GuardWalkState(ISpriteState previousState, SpriteInfo heroInfo, SpriteInfo guardInfo, MazeInfo mazeInfo) : base(previousState)
    {
        _heroInfo = heroInfo;

        _guardInfo = guardInfo;
        _mazeInfo = mazeInfo;

        var walkPathLength = RandomHelper.Next(WalkPathMinLength, WalkPathMaxLength);

        _walkPath = GetRandomWalkPath(walkPathLength);
    }

    public override ISpriteState ProcessState(GameTime gameTime)
    { 
        base.ProcessState(gameTime); 
        
        if (IsHeroNearby(_heroInfo, _guardInfo))
        {
            return new GuardChaseState(this, _heroInfo, _guardInfo, _mazeInfo);
        }

        var walkPosition = _walkPath.First();

        var guardPosition = _guardInfo.Position;
        var direction = GetMovementDirection(guardPosition, walkPosition);

        var guard = _guardInfo.Sprite;
        var movement = guard.GetMovement(direction, gameTime);

        ProcessFrameEffect(movement);

        var newPosition = guardPosition + movement;

        if (CollisionManager.CollidesWithWalls(guard, guardPosition, movement, _mazeInfo.Maze)) //
        {
            throw new Exception();
        }

        _guardInfo.Position = newPosition;

        if (IsWalkPositionReached(walkPosition, newPosition))
        {
            _walkPath.RemoveFirst();
        }

        if (_walkPath.Count is 0)
        {
            return new GuardIdleState(this, _heroInfo, _guardInfo, _mazeInfo);
        }

        return this;
    }

    private LinkedList<Vector2> GetRandomWalkPath(int pathLength)
    {
        var guardPosition = _guardInfo.Position;

        var startPosition = GetNormalizedPosition(guardPosition);
        var startCell = _mazeInfo.Maze.GetCellByPosition(startPosition);

        var currentCell = startCell;

        var visitedCells = new HashSet<Cell>() { currentCell };
        var path = new LinkedList<Vector2>(); 
        
        var maze = _mazeInfo.Maze;
        var exitCell = maze.ExitInfo.Cell;

        var movingPosition = maze.GetCellPosition(currentCell);

        path.AddLast(movingPosition);

        for (int i = 0; i < pathLength; i++)
        {
            var adjacentCells = GetAdjacentMovingCells(currentCell, exitCell, maze, visitedCells).ToArray();

            if (adjacentCells.Length is 0)
            {
                break;
            }

            currentCell = RandomHelper.Choice(adjacentCells);
            movingPosition = maze.GetCellPosition(currentCell);

            path.AddLast(movingPosition);
            visitedCells.Add(currentCell);
        }

        return path;
    }
}