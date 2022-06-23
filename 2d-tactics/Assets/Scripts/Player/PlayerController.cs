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
    [SerializeField] private int teamID;


    private Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();
    private GameObject selectedCharacter;

    void Awake()
    {
        gameCH.initEvent += onInit;
        gameCH.characterSelectedEvent += onCharacterSelected;
        gameCH.moveEvent += onMove;
    }

    private void onInit() {
        
        Vector2Int tileSpawnPosition = new Vector2Int(teamID,0);
        Vector3 spawnPosition = field.CellToWorld(new Vector3Int(tileSpawnPosition.x, tileSpawnPosition.y, 0));
        GameObject character = spawnCharacter(spawnPosition);
        characters.Add(character.GetInstanceID(), character);
        Debug.Log("spawned char: " + character.GetInstanceID());

        Vector2Int tileSpawnPosition2 = new Vector2Int(teamID,1);
        Vector3 spawnPosition2 = field.CellToWorld(new Vector3Int(tileSpawnPosition2.x, tileSpawnPosition2.y, 0));
        GameObject character2 = spawnCharacter(spawnPosition2);
        characters.Add(character2.GetInstanceID(), character2);
        Debug.Log("spawned char: " + character2.GetInstanceID());
    }

    private void onCharacterSelected(int charID) {
        if(characters.ContainsKey(charID)){
            this.selectedCharacter = characters[charID];
        } else {
            this.selectedCharacter = null;
        }
    }

    private void onMove(Vector2 destination) {
        if(selectedCharacter != null) {
            selectedCharacter.GetComponent<CharacterS>().move(destination);
        }
    }

    private void onAttack(Vector2 target) {
        if(selectedCharacter != null) {
            
        }
    }

    private GameObject spawnCharacter(Vector2 spawnPosition) {
        GameObject characterInstance = Instantiate(character, spawnPosition, Quaternion.identity);
        characterInstance.GetComponent<CharacterS>().field = this.field;
        characterInstance.GetComponent<CharacterS>().obstacles = this.obstacles;
        characterInstance.GetComponent<CharacterS>().mainCamera = this.mainCamera;
        characterInstance.GetComponent<CharacterS>().Init();

        return characterInstance;
    }
}
