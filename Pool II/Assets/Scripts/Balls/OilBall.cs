using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBall : BaseBall {
    [SerializeField]
    float oil_frequency;

    float time_since_oil = 0f;

    [SerializeField]
    GameObject oil_blob;

    protected override void OnMoving() {
        time_since_oil -= Time.deltaTime;

        if (time_since_oil <= 0f) {
            DropOil();
            time_since_oil = oil_frequency;
        }
    }

    protected override void OnSettle() {
        time_since_oil = 0f;
    }

    void DropOil() {
        Vector3 offset = Random.insideUnitCircle * 0.5f;

        GameObject new_blob = Instantiate(oil_blob, transform.position + offset, Quaternion.identity);

        new_blob.transform.localScale = new_blob.transform.localScale * Random.Range(0.5f, 2f);
    }
}
