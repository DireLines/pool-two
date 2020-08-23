using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour {
    [HideInInspector]
    public GameObject heldObject;

    public List<GameObject> placedThisTurn;

    //singleton pattern
    public static BuildController instance;
    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        placedThisTurn = new List<GameObject>();
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
        if (holdingSomething()) {
            dropHeldObject();
        } else {
            hold(obj);
        }
    }

    void onMouseDown() {
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
        //TODO: make scriptless preview of ball instead of actual ball
        heldObject = Instantiate(obj, mouseWorldPos(), Quaternion.identity);
    }

    private void dropHeldObject() {
        //TODO: give money back if you are deleting a ball from the set of current balls
        Destroy(heldObject);
        heldObject = null;
    }

    //place currently held object
    private void placeHeldObject() {
        GameObject newObj = Instantiate(heldObject, mouseWorldPos(), Quaternion.identity);
        placedThisTurn.Add(newObj);
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
