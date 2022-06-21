using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameCH gameCH;
    [SerializeField] private GameObject character;
    [SerializeField] private Tilemap field; // TODO: via Channel? onInit?
    [SerializeField] private Tilemap obstacles;
    [SerializeField] private Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        gameCH.initEvent += onInit;
    }

    private void onInit() {
        Debug.Log("spawning player team");
        Vector2Int tileSpawnPosition = new Vector2Int(-4,0);
        Vector3 spawnPosition = field.CellToWorld(new Vector3Int(tileSpawnPosition.x, tileSpawnPosition.y, 0));
        GameObject characterInstance = Instantiate(character, spawnPosition, Quaternion.identity);
        characterInstance.GetComponent<CharacterS>().field = field;
        characterInstance.GetComponent<CharacterS>().obstacles = obstacles;
        characterInstance.GetComponent<CharacterS>().mainCamera = mainCamera;
        characterInstance.GetComponent<CharacterS>().Init();
    }
}
