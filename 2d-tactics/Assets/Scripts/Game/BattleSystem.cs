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

    // Start is called before the first frame update
    void Start()
    {
        this.currentState = new InitializeST(this);
        this.InputReader.clickEvent += propagateOnClick;
        this.attackBtn.onClick.AddListener(() => UiClicked("attack"));
        this.defendBtn.onClick.AddListener(() => UiClicked("defend"));
        this.moveBtn.onClick.AddListener(() => UiClicked("move"));

    }

    public void SetState(IState newState) {
        this.currentState = newState;
    }

    public void UiClicked(string btn) {
        Debug.Log(btn);
        this.currentState.onUIClick(btn);
    }

    private void propagateOnClick(Vector2 position) {
        this.currentState.onClick(position);
    }
}
