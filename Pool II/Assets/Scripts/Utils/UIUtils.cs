using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIUtils {
    // functions stolen/adapted from Keywords
    public static void DisableBall(GameObject obj) {
        if (obj.GetComponents<Collider2D>() != null) {
            foreach (Collider2D col in obj.GetComponents<Collider2D>()) {
                col.enabled = false;
            }
        }
        if (obj.GetComponent<Rigidbody2D>()) {
            obj.GetComponent<Rigidbody2D>().simulated = false;
        }
        if (obj.GetComponents<BaseBall>() != null) {
            foreach (BaseBall bol in obj.GetComponents<BaseBall>()) {
                bol.enabled = false;
            }
        }
        //add other scripts here if needed
        foreach (Transform child in obj.transform) {
            DisableBall(child.gameObject);
        }
    }
    public static void EnableBall(GameObject obj) {
        if (obj.GetComponents<Collider2D>() != null) {
            foreach (Collider2D col in obj.GetComponents<Collider2D>()) {
                col.enabled = true;
            }
        }
        if (obj.GetComponent<Rigidbody2D>() != null) {
            obj.GetComponent<Rigidbody2D>().simulated = true;
        }
        if (obj.GetComponents<BaseBall>() != null) {
            foreach (BaseBall bol in obj.GetComponents<BaseBall>()) {
                bol.enabled = true;
            }
        }
        //add other scripts here if needed
        foreach (Transform child in obj.transform) {
            EnableBall(child.gameObject);
        }
    }

    //called on an object and recursively sets its and its children's sorting order to be based on the desired height
    public static void RepositionInSortingOrder(GameObject obj, int height) {
        int originalSortingOrder = 0;
        if (obj.GetComponent<SpriteRenderer>()) {
            originalSortingOrder = obj.GetComponent<SpriteRenderer>().sortingOrder;
            obj.GetComponent<SpriteRenderer>().sortingOrder = height;
        }
        foreach (Transform child in obj.transform) {
            int diff = 0;
            if (child.GetComponent<SpriteRenderer>()) {
                diff = child.gameObject.GetComponent<SpriteRenderer>().sortingOrder - originalSortingOrder;
            }
            RepositionInSortingOrder(child.gameObject, height + diff);
        }
    }
}
