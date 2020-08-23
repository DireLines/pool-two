using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour {
    [HideInInspector]
    public GameObject heldObject;
    private int heldObjectPrice;

    public EconomyManager EM;

    private AudioSource oopsSFX;

    //singleton pattern
    public static BuildController instance;
    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;

        oopsSFX = GetComponent<AudioSource>();
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
    public void onClickShopButton(ShopItem item) {
        if (holdingSomething()) {
            dropHeldObject();
        } else {
            if (EM.TryPurchaseItem(item)) {
                heldObjectPrice = item.cost;
                hold(item.prefab);
            } else {
                oopsSFX.Play();
            }
        }
    }

    void onMouseDown() {
        if (holdingSomething()) {
            if (!overUI() && canPlace(mouseWorldPos())) {
                placeHeldObject();
            }
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
        EM.Refund(heldObjectPrice);
        Destroy(heldObject);
        heldObject = null;
    }

    private void placeHeldObject() {
        Instantiate(heldObject, mouseWorldPos(), Quaternion.identity);
        Destroy(heldObject);
        heldObject = null;
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
