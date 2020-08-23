﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour {
    [HideInInspector]
    public GameObject heldObject;
    private GameObject preview;
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
            preview.transform.position = mouseWorldPos();
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
            if (!overUI()) {
                if (canPlace(mouseWorldPos())) {
                    placeHeldObject();
                } else {
                    oopsSFX.Play();
                }
            }
        }
    }

    private bool holdingSomething() {
        return heldObject != null;
    }

    //begin holding obj
    private void hold(GameObject obj) {
        preview = Instantiate(obj, mouseWorldPos(), Quaternion.identity);
        UIUtils.DisableBall(preview);
        UIUtils.RepositionInSortingOrder(preview, 100);
        heldObject = Instantiate(obj, mouseWorldPos(), Quaternion.identity);
        heldObject.SetActive(false);
    }

    private void stopHolding() {
        Destroy(preview);
        Destroy(heldObject);
        heldObject = null;
    }

    private void dropHeldObject() {
        EM.Refund(heldObjectPrice);
        stopHolding();
    }

    private void placeHeldObject() {
        GameObject newObj = Instantiate(heldObject, mouseWorldPos(), Quaternion.identity);
        newObj.SetActive(true);
        stopHolding();
    }


    //can I place this object at this position on screen?
    //TODO detect if object would collide with other balls
    private bool canPlace(Vector2 screenPos) {
        return true;
    }

    //is screenPos currently on top of any UI?
    private bool overUI() {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private Vector3 mouseWorldPos() {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(mouse.x, mouse.y, 0);
    }
}
