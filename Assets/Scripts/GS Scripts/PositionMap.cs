using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMap : MonoBehaviour {
    public int i = 0;

    public Vector3Int GetPosition() {
        return Vector3Int.RoundToInt(transform.position);
    }

    public int GetI() {
        return i++;
    }
}