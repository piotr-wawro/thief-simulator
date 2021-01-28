using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour {

    public new Camera camera;
    public float force = 7f;
    public float throwForce = 7f;
    public float armLenght = 2f;

    private RaycastHit selectedItem;

    // Start is called before the first frame update
    void Start() {

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
            HoldItem();
        }

        if(selectedItem.collider != null) {
            if((selectedItem.transform.position - camera.transform.position).magnitude > armLenght * 1.5) {
                DropItem();
            }
        }

        if(Input.GetMouseButtonDown(1)) {
            ThrowItem();
        }
    }

    private void DropItem() {
        if(selectedItem.collider != null) {
            selectedItem.rigidbody.useGravity = true;
            selectedItem = new RaycastHit();
        }
    }

    private void GrabItem() {
        SelectItem();
        if(selectedItem.collider != null) {
            selectedItem.rigidbody.useGravity = false;
        }
    }

    private void ThrowItem() {
        if(selectedItem.collider != null) {
            selectedItem.rigidbody.useGravity = true;
            selectedItem.rigidbody.velocity = camera.transform.forward * throwForce;
            selectedItem.rigidbody.angularVelocity = RandomVector(3, 7);
            selectedItem = new RaycastHit();
        }
    }

    private void HoldItem() {
        if(selectedItem.collider != null) {
            Vector3 destination = camera.transform.position + camera.transform.forward * armLenght / 1.75f;
            Vector3 delta = destination - selectedItem.transform.position;
            selectedItem.rigidbody.velocity = delta.normalized * delta.magnitude * force;
            selectedItem.rigidbody.angularVelocity -= selectedItem.rigidbody.angularVelocity * 1.5f * Time.deltaTime;
        }
    }

    private bool SelectItem() {
        Vector3 position = camera.transform.position;
        Vector3 target = position + camera.transform.forward * armLenght;
        RaycastHit hit;

        if(Physics.Linecast(position, target, out hit)) {
            if(hit.collider.tag == "Item") {
                selectedItem = hit;
                return true;
            }
        }

        return false;
    }

    private Vector3 RandomVector(float min, float max) {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
    }
}
