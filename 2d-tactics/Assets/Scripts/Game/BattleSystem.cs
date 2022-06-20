using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine // TODO: ScriptableObject
{
    [SerializeField] private InputReader InputReader = default;
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        this.currentState = new ChooseCharacterST(this);
        this.InputReader.clickEvent += currentState.onClick;
    }

    public override void SetState(State newState) {
        this.InputReader.clickEvent -= currentState.onClick;
        this.currentState = newState;
        this.InputReader.clickEvent += currentState.onClick;
    }
}
