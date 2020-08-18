using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {
    [HideInInspector]
    public GameObject heldObject;

    //singleton pattern
    public static BuildController instance;
    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Update() {
        if (heldObject) {
            heldObject.transform.position = transform.position;//TODO: put object at mouse position
        }
    }


    //pick up an item from the shop
    public void onClickShopButton(GameObject obj) {
        print("clicked on " + obj.name);
        if (holdingSomething()) {
            Destroy(heldObject);
            heldObject = null;
        } else {
            Instantiate(obj, transform);
        }
    }

    public void onClick() {

    }

    private bool holdingSomething() {
        return heldObject != null;
    }

    //begin holding obj
    private void hold(GameObject obj) {

    }

    //place currently held object
    private void place() {

    }


    //can I place this object at this position on screen?
    private bool canPlace(Vector2 screenPos) {
        return true;
    }

    //is screenPos currently on top of the shop UI?
    private bool overShopUI(Vector2 screenPos) {
        return false;
    }
}
