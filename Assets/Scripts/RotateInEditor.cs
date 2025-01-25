using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class RotateInEditor : MonoBehaviour
{
    public int i;
    private void OnValidate()
    {
        float r = Random.Range(0, 360);
        transform.rotation = Quaternion.AngleAxis(r, Vector3.up);
    }
}
