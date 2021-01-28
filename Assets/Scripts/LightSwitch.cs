using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public float armLenght = 1.2f;
    public List<GameObject> lights;

    private new Camera camera;

    private void Start()
    {
      camera  = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 position = camera.transform.position;
        Vector3 target = position + camera.transform.forward * armLenght;
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && Physics.Linecast(position, target, out hit))
        {
           
            if (hit.collider.gameObject == gameObject)
            {
                GetComponent<AudioSource>().Play();
                foreach (var light in lights)
                {
                    light.GetComponent<Light>().enabled = !light.GetComponent<Light>().enabled;
                }
            }
        }
    }
}
