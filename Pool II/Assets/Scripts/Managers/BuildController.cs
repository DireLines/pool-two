using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (holdingSomething()) {
            heldObject.transform.position = mouseWorldPos();
        }

        if (Input.GetMouseButtonDown(0)) {
            onMouseDown();
        }
    }


    //pick up an item from the shop
    public void onClickShopButton(GameObject obj) {
        print("onClickShopButton(" + obj.name + ")");
        if (holdingSomething()) {
            dropHeldObject();
        } else {
            hold(obj);
        }
    }

    void onMouseDown() {
        print("onMouseDown" + mouseWorldPos());
        if (holdingSomething()) {
            if (!overUI() && canPlace(mouseWorldPos())) {
                placeHeldObject();
            }
        } else {
            //TODO: click and drag existing balls if you placed them this turn
        }
    }

    private bool holdingSomething() {
        return heldObject != null;
    }

    //begin holding obj
    private void hold(GameObject obj) {
        heldObject = Instantiate(obj, mouseWorldPos(), Quaternion.identity);
    }

    private void dropHeldObject() {
        //TODO: give money back if you are deleting a ball from the set of current balls
        Destroy(heldObject);
        heldObject = null;
    }

    //place currently held object
    private void placeHeldObject() {
        //TODO try to purchase from shop
        Instantiate(heldObject, mouseWorldPos(), Quaternion.identity);
        dropHeldObject();
    }


    //can I place this object at this position on screen?
    //TODO detect if object would collide with other balls
    private bool canPlace(Vector2 screenPos) {
        return true;
    }

    //is screenPos currently on top of the shop UI?
    private bool overUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private Vector3 mouseWorldPos() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mouse.x, mouse.y, 0);
    }


}
