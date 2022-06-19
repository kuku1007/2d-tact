using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    public void onClick(Vector2 position) {}
    public void onHover(Vector2 position) {}
}
