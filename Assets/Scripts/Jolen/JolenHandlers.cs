using UnityEngine;
public interface ISwipeInputHandler
{
    bool TryGetSwipe(out Vector2 start, out Vector2 end);
}

public interface IJolenSpawner
{
    void Spawn();
}

public interface ITurnManager
{
    bool IsPlayerTurn { get; }
    void EndTurn();
}
