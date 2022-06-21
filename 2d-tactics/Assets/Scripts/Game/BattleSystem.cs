using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour // TODO: ScriptableObject
{
    [SerializeField] private InputReader InputReader = default;
    public GameCH GameCH;
    
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        this.currentState = new InitializeST(this);
        this.InputReader.clickEvent += propagateOnClick;
    }

    public void SetState(State newState) {
        this.currentState = newState;
    }

    private void propagateOnClick(Vector2 position) {
        this.currentState.onClick(position);
    }
}
