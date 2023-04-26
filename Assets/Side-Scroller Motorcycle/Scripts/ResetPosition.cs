using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Transform objectToReset;
    public Vector3 resetPosition;

    public void ResetObjectPosition()
    {
        objectToReset.position = resetPosition;
    }
}
