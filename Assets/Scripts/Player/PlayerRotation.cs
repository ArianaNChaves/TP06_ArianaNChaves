using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private bool _facingRight = true;
    
    
    public void FlipPlayer(float horizontalMovement)
    {
        if((horizontalMovement < 0 && _facingRight) || (horizontalMovement > 0 && !_facingRight))
        {
            _facingRight = !_facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
