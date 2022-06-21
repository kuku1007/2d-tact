using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected BattleSystem sm;

    public State(BattleSystem sm) {
        this.sm = sm;
    }

    public virtual void onClick(Vector2 position) {}
    public virtual void onHover(Vector2 position) {}
}
