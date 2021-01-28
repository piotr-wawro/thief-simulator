using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectIteraction : MonoBehaviour
{
    public new Camera camera;
    public float force = 10f;
    public float armLenght = 2f;

    private RaycastHit selectedItem;
    private GameObject point;

    // Start is called before the first frame update
    void Start() {
        point = new GameObject();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            GrabItem();
        }
        if(Input.GetMouseButtonUp(0)) {
            DropItem();
        }

        if(Input.GetKey(KeyCode.Mouse0)) {
            DragItem();
        }

        if(selectedItem.collider != null) {
            if((selectedItem.transform.position - camera.transform.position).magnitude > armLenght * 1.5) {
                DropItem();
            }
        }
    }

    private void DropItem() {
        if(selectedItem.collider != null) {
            selectedItem = new RaycastHit();
        }
    }

    private void GrabItem() {
        SelectItem();
    }

    private void DragItem() {
        if(selectedItem.collider != null) {
            Vector3 destination = camera.transform.position + camera.transform.forward * selectedItem.distance;
            Vector3 delta = destination - point.transform.position;
            selectedItem.rigidbody.AddForceAtPosition(delta.normalized * Mathf.Clamp(delta.magnitude, 0, 1f) * force, point.transform.position);
        }
    }

    private bool SelectItem() {
        Vector3 position = camera.transform.position;
        Vector3 target = position + camera.transform.forward * armLenght;
        RaycastHit hit;

        if(Physics.Linecast(position, target, out hit)) {
            if(hit.collider.tag == "HeavyItem") {
                selectedItem = hit;
                point.transform.position = selectedItem.point;
                point.transform.SetParent(selectedItem.transform);
                return true;
            }
        }

        return false;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
}
