using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Table : MonoBehaviour {

    public LinearMapping linearMapping;

    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] GameObject table;
    float currentLinearMapping = float.NaN;
    float diff;
    float startPosition;

    void Start() {
        if (linearMapping == null) {
            linearMapping = GetComponent<LinearMapping>();
        }

        startPosition = table.transform.position.y; // -1
        diff = start.position.y - end.position.y; // 2
        table.transform.position = new Vector3(table.transform.position.x, linearMapping.value * diff + table.transform.position.y, table.transform.position.z); 
    }

    void Update() {
        if (currentLinearMapping != linearMapping.value) {
            currentLinearMapping = linearMapping.value; // 0.5

            float newY = startPosition + currentLinearMapping * diff; 
            table.transform.position = new Vector3(table.transform.position.x, newY, table.transform.position.z);
        }
    }
}