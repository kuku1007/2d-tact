using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, Gen_PlayerInputActions.IPlayerActions
{
    public event UnityAction<Vector2> moveEvent;
    public event UnityAction<Vector2> clickEvent;
    public event UnityAction<Vector2> pointerPosEvent;
    
    private Gen_PlayerInputActions inputActions;

	private void OnEnable()
	{
		if (inputActions == null)
		{
			inputActions = new Gen_PlayerInputActions();
			inputActions.Player.SetCallbacks(this);
		}

		inputActions.Player.Enable();
	}

    public void OnClick(InputAction.CallbackContext context)
    {       
        if(EventSystem.current.IsPointerOverGameObject())
            return;


        if (clickEvent != null && context.phase == InputActionPhase.Performed)
			clickEvent.Invoke(inputActions.Player.PointerPos.ReadValue<Vector2>());
    }

    public void OnPointerPos(InputAction.CallbackContext context)
    {
        if (pointerPosEvent != null && context.phase == InputActionPhase.Performed)
			pointerPosEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (moveEvent != null && context.phase == InputActionPhase.Performed)
			moveEvent.Invoke(context.ReadValue<Vector2>());
        else if(moveEvent != null && context.phase == InputActionPhase.Canceled) {
            moveEvent.Invoke(new Vector2(0,0));
        }
    }
}
