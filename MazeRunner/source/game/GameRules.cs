﻿namespace MazeRunner.GameBase;

public static class GameRules
{
    public const int GuardHeroMaxPathLength = 7;

    private const float EnemySpawnDistanceCoeff = 3;
    private const float EnemyDisposingDistanceCoeff = 5;

    private const float GuardHeroDetectionDistanceCoeff = 3;

    public const float GuardHeroDetectionDistance = GameConstants.AssetsFrameSize * GuardHeroDetectionDistanceCoeff;

    public const float EnemyDisposeDistance = GameConstants.AssetsFrameSize * EnemyDisposingDistanceCoeff;
    public const float EnemySpawnDistance = GameConstants.AssetsFrameSize * EnemySpawnDistanceCoeff;
}