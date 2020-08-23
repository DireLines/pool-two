using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableZone : MonoBehaviour {
    public int ownerNumber;
    public List<CueBall> cueBalls;

    private void Start() {
        cueBalls = new List<CueBall>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<CueBall>()) {
            cueBalls.Add(collision.GetComponent<CueBall>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<CueBall>()) {
            cueBalls.Remove(collision.GetComponent<CueBall>());
        }
    }
}
