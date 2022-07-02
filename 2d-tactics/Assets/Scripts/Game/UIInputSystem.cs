using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class UIInputSystem : MonoBehaviour // TODO: ScriptableObject
{
    [SerializeField] private InputReader InputReader = default;
    public GameCH GameCH;
    public Camera mainCamera;
    public Button attackBtn, defendBtn, moveBtn;
    private IState currentState;
    public string currentTeam = "team1"; // TODO: hard coded
    public string enemyTeam = "team2"; // TODO: hard coded

    void Start()
    {
        this.currentState = new InitializeST(this, null); // TODO: load some game configuration, team names, team units etc
        this.InputReader.clickEvent += propagateOnClick;
        this.attackBtn.onClick.AddListener(() => propagateUIClicked("attack"));
        this.defendBtn.onClick.AddListener(() => propagateUIClicked("defend"));
        this.moveBtn.onClick.AddListener(() => propagateUIClicked("move"));
        this.GameCH.turnContextChangedEvent += propagateContextChanged;
    }

    public void SetState(IState newState) {
        this.currentState = newState;
    }

    private void propagateUIClicked(string btn) {
        this.currentState.onUIClick(btn);
    }

    private void propagateOnClick(Vector2 position) {
        this.currentState.onClick(position);
    }

    private void propagateContextChanged(TurnContext turnContext) {
        this.currentState.onContextChanged(turnContext);
    }
}
