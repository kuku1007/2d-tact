using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "GameCH", menuName = "Game/Game Channel")]
public class GameCH : ScriptableObject
{
    public event UnityAction initEvent;
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction<int> characterSelectedEvent;
    public event UnityAction moveSelectedEvent;
    public event UnityAction attackSelectedEvent;
    public event UnityAction defendSelectedEvent;

    public void RaiseInit() {
		  initEvent?.Invoke();
    }

    public void RaiseMove(Vector2 destination) {
		  moveEvent?.Invoke(destination);
    }

    public void RaiseCharacterSelected(int objectID) {
        characterSelectedEvent?.Invoke(objectID);
    }

    public void RaiseMoveSelected() {
		  moveSelectedEvent?.Invoke();
    }

    public void RaiseAttackSelected() {
		  attackSelectedEvent?.Invoke();
    }

    public void RaiseDefendSelected() {
		  defendSelectedEvent?.Invoke();
    }
}
