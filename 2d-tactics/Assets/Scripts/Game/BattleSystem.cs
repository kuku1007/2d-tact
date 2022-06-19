using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private InputCH inputCH = default;

    // Start is called before the first frame update
    void Start()
    {
                this.inputCH.clickEvent += onClick;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(Vector2 position){

            return;
                
    }
}
