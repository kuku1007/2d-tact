using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class test : MonoBehaviour
{
    public Tilemap obstacles;
    public Tilemap field;
    public InputReader inputReader = default;
    
    // Start is called before the first frame update
    void Start()
    {
        inputReader.clickEvent += showTileIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        foreach (var position in field.cellBounds.allPositionsWithin) {
            if (!field.HasTile(position)) {

                continue;
            }
            UnityEditor.Handles.color = Color.red;
            string s = "" + position.x.ToString() + "," + position.y.ToString()+ "";
            Vector3 worldPost = field.CellToWorld(position);
            UnityEditor.Handles.Label( new Vector3(worldPost.x,worldPost.y,0),  s);
            // Tile is not empty; do stuff
        }

        #endif
    }

    private void showTileIndex(Vector2 pos) {

        Debug.Log("in: " + pos);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        Debug.Log("world in: " + worldPos);
        Debug.Log("Field id: " + field.WorldToCell(worldPos));
        Debug.Log("Obstacle id: " + obstacles.WorldToCell(worldPos));
        float dist = axialDistance(new Vector2Int(0,0), (Vector2Int)field.WorldToCell(worldPos));
        Debug.Log("Distance: " + dist);
    }
    Vector2Int oddrToAxial(Vector2Int hex) {
        var q = hex.x - (-hex.y - (-hex.y&1)) / 2;
        var r = -hex.y;
        return new Vector2Int(q, r);
    }

    private float axialDistance(Vector2Int hexA, Vector2Int hexB) {
        Vector2Int a = oddrToAxial(hexA);
        Vector2Int b = oddrToAxial(hexB);
        return (Math.Abs(a.x - b.x) + Math.Abs(a.x + a.y - b.x - b.y) + Math.Abs(a.y - b.y)) / 2;
    }
    
}
