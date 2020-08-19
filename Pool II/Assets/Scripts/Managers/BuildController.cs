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
        print(mouseWorldPos());
        if (holdingSomething()) {
            heldObject.transform.position = mouseWorldPos();
        }
    }


    //pick up an item from the shop
    public void onClickShopButton(GameObject obj) {
        print("clicked on " + obj.name);
        if (!holdingSomething()) {
            hold(obj);
        }
    }

    public void onClick() {
        bool holding = holdingSomething();
        bool overUI = overShopUI();
        if (holding) {
            if (overUI) {
                Destroy(heldObject);
                heldObject = null;
            } else {
                placeHeldObject();
            }
        } else {
            if (canPlace(mouseWorldPos())) {

            }
        }
    }

    private bool holdingSomething() {
        return heldObject != null;
    }

    //begin holding obj
    private void hold(GameObject obj) {
        heldObject = Instantiate(obj, mouseWorldPos(), Quaternion.identity);
    }

    //place currently held object
    private void placeHeldObject() {
        Vector3 spawnPos = mouseWorldPos();
    }


    //can I place this object at this position on screen?
    private bool canPlace(Vector2 screenPos) {
        return true;
    }

    //is screenPos currently on top of the shop UI?
    private bool overShopUI() {
        Vector3 worldPoint = mouseWorldPos();
        return false;
    }

    private Vector3 mouseWorldPos() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mouse.x, mouse.y, 0);
    }
}
