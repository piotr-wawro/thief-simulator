using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    public new Camera camera;
    public GameObject finishCanvas;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var angle = GetComponent<HingeJoint>().angle;
        if (angle > 40)
        {
            finishCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
