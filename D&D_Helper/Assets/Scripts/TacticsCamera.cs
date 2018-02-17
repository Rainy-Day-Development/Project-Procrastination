using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour {

    public Vector3 MapPos;
    public GameObject Map;

    void Start()
    {
        Map = GameObject.FindGameObjectWithTag("Map");
        MapPos = new Vector3(Map.transform.position.x, Map.transform.position.y, Map.transform.position.z);
        Debug.Log("x = " + MapPos.x);
        transform.localPosition = new Vector3(MapPos.x, MapPos.y, MapPos.z);
        Debug.Log("x = " + this.transform.position.x);
    }

    public void RotateLeft() {
        transform.Rotate(Vector3.up, 90, Space.Self);
    }

    public void RotateRight() {
        transform.Rotate(Vector3.up, -90, Space.Self);
    }


}
