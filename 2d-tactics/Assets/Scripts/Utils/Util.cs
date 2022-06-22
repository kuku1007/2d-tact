using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Util
{
    public static int checkClickedObject(Camera camera, Vector2 mousePosition, string tagToFind)
    {
        Vector2 ray = camera.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
        if (hit.collider != null) {
            Debug.Log("hit");
            if(hit.collider.tag == tagToFind) {                         
                Debug.Log("---> Hit: " + hit.transform.GetInstanceID());
                return hit.transform.GetInstanceID();                   
            }
        }

        Debug.Log("not clicked on unit");
        return -1;
    }
}
