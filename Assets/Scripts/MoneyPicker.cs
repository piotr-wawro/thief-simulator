using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPicker : MonoBehaviour
{

    public new Camera camera;
    public float armLenght = 2f;
    public float movementStep;
    public Transform player;

    private RaycastHit selectedItem;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pick")) {
            SelectItem();

            if(selectedItem.collider != null) {
                selectedItem.collider.GetComponent<CashMover>().move = true;
                selectedItem.collider.GetComponent<CashMover>().targetPosition = player;
                count++;
            }
        }
    }

    private bool SelectItem() {
        Vector3 position = camera.transform.position;
        Vector3 target = position + camera.transform.forward * armLenght;
        RaycastHit hit;

        if(Physics.Linecast(position, target, out hit)) {
            if(hit.collider.tag == "cash") {
                selectedItem = hit;
                return true;
            }
        }

        return false;
    }
}
