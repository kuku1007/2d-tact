using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    protected UIInputSystem sm;

    public IState(UIInputSystem sm) {
        this.sm = sm;
    }

    public virtual void onClick(Vector2 position) {}
    public virtual void onHover(Vector2 position) {}
    public virtual void onUIClick(string name) {}
    public virtual void onContextChanged(TurnContext turnContext) {}

    
}
