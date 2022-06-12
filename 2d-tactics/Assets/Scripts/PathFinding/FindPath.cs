using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

public class FindPath
{
    public Queue<PathThreadInfo> pathQueue = new Queue<PathThreadInfo>(); 
    
    public void RequestPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> obstaclesPositions, Action<PathData> callback) {

            ThreadStart findPathThread = delegate {

                Stack<Vector2Int> path = Astar.findPath(start, goal, obstaclesPositions);

                PathData data = new PathData(path);
                // lock(pathQueue) {
                    pathQueue.Enqueue(new PathThreadInfo(path, callback));
                // }
            };

            new Thread(findPathThread).Start();
    }

}

public struct PathData {
    private readonly Stack<Vector2Int> path;

    public PathData(Stack<Vector2Int> path)
    {
        this.path = path;
    }
}

public struct PathThreadInfo {
    public readonly Stack<Vector2Int> path;
    private readonly Action<PathData> callback;

    public PathThreadInfo(Stack<Vector2Int> path, Action<PathData> callback)
    {
        this.path = path;
        this.callback = callback;
    }
}