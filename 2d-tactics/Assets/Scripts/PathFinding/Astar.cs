using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

public class Astar
{

    // public static Stack<Vector2Int> pathFindJobs(Vector2Int start, Vector2Int goal, List<Vector2Int> obstaclesPositions) {

    //     // Set up the job data
    //     FindPath jobData = new FindPath();
    //     jobData.start = start;
    //     jobData.goal = goal;
    //     jobData.obstaclesPositions = obstaclesPositions;
    //     jobData.result = new Stack<Vector2Int>();

    //     // Schedule the job
    //     JobHandle handle = jobData.Schedule();

    //     // Wait for the job to complete
    //     handle.Complete();

    //     return jobData.result;
    // }
    private static bool validate(Vector2Int goal, HashSet<Vector2Int> obstaclesPositions) {
        if(obstaclesPositions.Contains(goal))
            return false;

        return true;

    }
    public static Stack<Vector2Int> findPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> obstaclesPositions) {
        Dictionary<Vector2Int, Node> openNodes = new Dictionary<Vector2Int, Node>();
        Dictionary<Vector2Int, Node> closedNodes = new Dictionary<Vector2Int, Node>();

        if(!validate(goal, obstaclesPositions)) {
            Debug.Log("invalid goal");
            return goalNotFound(start);
        }

        Node currentNode = new Node(start);
        currentNode.gCost = 0;
        currentNode.fCost = 0;
        currentNode.hCost = 0;
        openNodes.Add(start, currentNode);
        int counter = 0;

        while(currentNode.tilePos != goal && openNodes.Count != 0)
        {
            counter++;
            // Debug.Log("next iteration: " + counter);
            if (counter == 1000) break;
            Node nextCandidate = null;
            foreach (Node node in openNodes.Values)
            {
                if (nextCandidate == null)
                {
                    nextCandidate = node;
                    continue;
                }

                if (nextCandidate.fCost > node.fCost)
                {
                    nextCandidate = node;
                }
                else if (nextCandidate.fCost == node.fCost)
                {
                    if (nextCandidate.gCost > node.gCost)
                    {
                        nextCandidate = node;
                    }
                }
            }
            currentNode = nextCandidate;

            openNodes.Remove(currentNode.tilePos);
            closedNodes.Add(currentNode.tilePos, currentNode);

            if (currentNode.tilePos == goal)
            {
                break;
            }
            // Debug.Log("current node: " + currentNode.tilePos);
            // Debug.Log("cost: " + currentNode.fCost);

            RectGrid(goal, obstaclesPositions, openNodes, closedNodes, currentNode);
        }


        if (!closedNodes.ContainsKey(goal)) {
            return goalNotFound(start);
        }

        Stack<Vector2Int> path = new Stack<Vector2Int>();
        Node nextNode = closedNodes[goal];

        while(nextNode.parentNode != null) {
            path.Push(nextNode.tilePos);
            nextNode = nextNode.parentNode;
        }
        return path;
    }

    private static void RectGrid(Vector2Int goal, HashSet<Vector2Int> obstaclesPositions, Dictionary<Vector2Int, Node> openNodes, Dictionary<Vector2Int, Node> closedNodes, Node currentNode)
    {
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                // For hex:
                if(currentNode.tilePos.y % 2 == 0) {
                    if(x == 1 && y != 0) {
                        continue;
                    }
                } else if(currentNode.tilePos.y % 2 == -1 || currentNode.tilePos.y % 2 == 1) {
                    if(x == -1 && y != 0) {
                        continue;
                    }
                }

                Vector2Int currentNeighbourPos = new Vector2Int(currentNode.tilePos.x + x, currentNode.tilePos.y - y);

                // Not for hex:
                // if (!diagonallyPassable(currentNeighbourPos, currentNode.tilePos, obstaclesPositions))
                // {
                //     continue;
                // }

                Node tmpNeighbour = new Node(currentNeighbourPos);

                if (closedNodes.ContainsKey(currentNeighbourPos) || obstaclesPositions.Contains(currentNeighbourPos))
                    continue;

                tmpNeighbour.parentNode = currentNode;
                // calculateAndAddCost(tmpNeighbour, goal); not for hex
                hexCalculateAndAddCost(tmpNeighbour, goal); // For hex


                if (openNodes.ContainsKey(currentNeighbourPos))
                {
                    Node currentNeighbourNode = openNodes[currentNeighbourPos];

                    if (tmpNeighbour.fCost < currentNeighbourNode.fCost)
                    {
                        openNodes[tmpNeighbour.tilePos] = tmpNeighbour;
                    }
                    else if (tmpNeighbour.fCost == currentNeighbourNode.fCost)
                    {
                        if (tmpNeighbour.gCost < currentNeighbourNode.gCost)
                        {
                            openNodes[tmpNeighbour.tilePos] = tmpNeighbour;
                        }
                    }
                }
                else
                {
                    openNodes[tmpNeighbour.tilePos] = tmpNeighbour;
                }
            }
        }
    }

    private static Stack<Vector2Int> goalNotFound(Vector2Int start) {
        Debug.Log("Goal not found");
        Stack<Vector2Int> path = new Stack<Vector2Int>();
        path.Push(start);
        return path; 
    }

    private static void calculateAndAddCost(Node currentNeighbour, Vector2Int goalPos) {
        Vector2Int currentPos = currentNeighbour.tilePos;
        Vector2Int posDiffToParent = currentPos - currentNeighbour.parentNode.tilePos;
        if(Math.Abs(posDiffToParent.x) == Math.Abs(posDiffToParent.y)) {
            currentNeighbour.gCost = currentNeighbour.parentNode.gCost + 10;
        } else {
            currentNeighbour.gCost = currentNeighbour.parentNode.gCost + 10;
        }

        Vector2Int posDiffToGoal = currentPos - goalPos;
        int xDiff = Math.Abs(posDiffToGoal.x);
        int yDiff = Math.Abs(posDiffToGoal.y);
        if(xDiff >= yDiff) {
            currentNeighbour.hCost = yDiff * 10 + ((xDiff - yDiff) * 10);
        } else {
            currentNeighbour.hCost = xDiff * 10 + ((yDiff - xDiff) * 10);
        }

        currentNeighbour.fCost = currentNeighbour.gCost + currentNeighbour.hCost;
    }
    private static void hexCalculateAndAddCost(Node currentNeighbour, Vector2Int goalPos) {
        Vector2Int currentPos = currentNeighbour.tilePos;
        currentNeighbour.gCost = currentNeighbour.parentNode.gCost + 10;

        currentNeighbour.hCost = (int)axialDistance(currentPos, goalPos) * 10;
        currentNeighbour.fCost = currentNeighbour.gCost + currentNeighbour.hCost;
    }

    private static bool diagonallyPassable(Vector2Int neighbour, Vector2Int current, HashSet<Vector2Int> obstaclesPositions) {
        Vector2Int diff = current - neighbour;

        Vector2Int first = new Vector2Int(current.x - diff.x, current.y);
        Vector2Int second = new Vector2Int(current.x, current.y - diff.y);

        if(obstaclesPositions.Contains(first) || obstaclesPositions.Contains(second)) {
            return false;
        }

        return true;
    }

    private static Vector2Int oddrToAxial(Vector2Int hex) {
        var q = hex.x - (-hex.y - (-hex.y&1)) / 2;
        var r = -hex.y;
        return new Vector2Int(q, r);
    }

    private static float axialDistance(Vector2Int hexA, Vector2Int hexB) {
        Vector2Int a = oddrToAxial(hexA);
        Vector2Int b = oddrToAxial(hexB);
        return (Math.Abs(a.x - b.x) + Math.Abs(a.x + a.y - b.x - b.y) + Math.Abs(a.y - b.y)) / 2;
    }
}

// public struct FindPath : IJob
// {
//     public Vector2Int start;
//     public Vector2Int goal;
//     public List<Vector2Int> obstaclesPositions;
//     public Stack<Vector2Int> result;

//     public void Execute()
//     {
//         result = Astar.findPath(start, goal, obstaclesPositions);
//     }
// }
