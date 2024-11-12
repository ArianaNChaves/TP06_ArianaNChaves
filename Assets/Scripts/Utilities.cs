using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static bool CompareLayerAndMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
    
    public static void RotateObjectOnMovement(float horizontalMovement, ref bool facingRight, ref Transform objTransform)
    {
        if ((!(horizontalMovement < 0) || !facingRight) && (!(horizontalMovement > 0) || facingRight)) return;
        facingRight = !facingRight;
        objTransform.Rotate(new Vector3(0, 180, 0));
    }
    public static void RotateObject(ref bool facingRight, ref Transform objTransform)
    {
        facingRight = !facingRight;
        objTransform.Rotate(new Vector3(0, 180, 0));
    }
}
