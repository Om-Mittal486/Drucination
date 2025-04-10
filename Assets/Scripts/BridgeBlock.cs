using UnityEngine;

[System.Serializable]
public class BridgeBlock
{
    public Rigidbody2D rb;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Quaternion startRot;
}
