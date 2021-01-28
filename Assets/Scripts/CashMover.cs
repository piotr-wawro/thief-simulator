using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashMover : MonoBehaviour
{

    public Transform targetPosition;
    public float movementStep = 0.05f;
    public bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, movementStep);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(move) {
            Destroy(gameObject);
        }
    }
}
