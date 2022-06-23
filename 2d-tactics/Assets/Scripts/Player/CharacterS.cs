using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System.Collections.Generic;

public class CharacterS : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 pointerPosition;
    private Vector2 prevPointerPosition;
    private float lookAngle;
    private Vector3 lookDirection;
    
    public Animator animator;
    public Camera mainCamera;
    public Tilemap field;
    public Tilemap obstacles;
    public HashSet<Vector2Int> obstaclesPositions;
    private Vector2[] worldPosPath;

    public delegate void PlayerShoot(float shootAngle, Vector3 shootDirection);
    public static event PlayerShoot OnPlayerShoot;

    public void Init() {
        this.rb = GetComponent<Rigidbody2D>();
        this.pointerPosition = rb.transform.position;
        this.prevPointerPosition = this.pointerPosition;

        this.obstaclesPositions = new HashSet<Vector2Int>();

        // bake obstacles map
        foreach (var pos in obstacles.cellBounds.allPositionsWithin)
        {   
            if (obstacles.HasTile(pos))
            {
                obstaclesPositions.Add((Vector2Int)(pos));
            }
        }
    }

    void Start()
    {
    
    }
    
    void FixedUpdate()
    {
        if(prevPointerPosition != pointerPosition && worldPosPath !=null) {
            this.prevPointerPosition = this.pointerPosition;
            rb.DOPath(worldPosPath, 3);
        }
        
        // setAnimationParamsPointer(lookAngle);
        // setAnimationParamsForWASD();
    }

    public void move(Vector2 inputPointerPos) { // TODO: clean this !!!
        pointerPosition = mainCamera.ScreenToWorldPoint(inputPointerPos);
        lookDirection = (pointerPosition - rb.position).normalized;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        Vector2Int cellPos = (Vector2Int)field.WorldToCell(pointerPosition);
        Vector2Int currentPos = (Vector2Int)field.WorldToCell(rb.transform.position);
        Stack<Vector2Int> path = Astar.findPath(currentPos, cellPos, obstaclesPositions);
        
        Vector2[] tmpWorldPosPath = new Vector2[path.Count];
        int i = 0;
        foreach (var tilePos in path) {
            tmpWorldPosPath[i] = field.CellToWorld(new Vector3Int(tilePos.x, tilePos.y, 0));
            i++;
        }
        this.worldPosPath = tmpWorldPosPath;
    }

    private void setAnimationParamsPointer(float angle) {
        if(moveDirection.x !=0 || moveDirection.y != 0) {
            animator.SetBool("playerMoves", true);
        } else {
            animator.SetBool("playerMoves", false);
        }
        animator.SetFloat("angle", angle);
    }

    private void setAnimationParamsWASD() {
        if(moveDirection.y > 0) {
            animator.SetBool("up", true);
            animator.SetBool("down", false);
        } else if(moveDirection.y < 0) {
            animator.SetBool("up", false);
            animator.SetBool("down", true);
        } else if(moveDirection.y == 0) {
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }

        if(moveDirection.x > 0) {
            animator.SetBool("right", true);
            animator.SetBool("left", false);
        } else if(moveDirection.x < 0) {
            animator.SetBool("right", false);
            animator.SetBool("left", true);
        } else if(moveDirection.x == 0) {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
        }
    }
}
