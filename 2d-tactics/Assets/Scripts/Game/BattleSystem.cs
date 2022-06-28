using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour // TODO: ScriptableObject
{
    [SerializeField] private InputReader InputReader = default;
    public GameCH GameCH;
    public Camera mainCamera;
    public Button attackBtn, defendBtn, moveBtn;
    private IState currentState;
    public string tagOfCharacter = "unit";

    void Start()
    {
        this.currentState = new InitializeST(this);
        this.InputReader.clickEvent += propagateOnClick;
        this.attackBtn.onClick.AddListener(() => propagateUIClicked("attack"));
        this.defendBtn.onClick.AddListener(() => propagateUIClicked("defend"));
        this.moveBtn.onClick.AddListener(() => propagateUIClicked("move"));
    }

    public void SetState(IState newState) {
        this.currentState = newState;
    }

    public void propagateUIClicked(string btn) {
        this.currentState.onUIClick(btn);
    }

    private void propagateOnClick(Vector2 position) {
        this.currentState.onClick(position);
    }
}
