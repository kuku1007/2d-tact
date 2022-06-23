using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    protected BattleSystem sm;

    public IState(BattleSystem sm) {
        this.sm = sm;
    }

    public virtual void onClick(Vector2 position) {}
    public virtual void onHover(Vector2 position) {}
}
