using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public Slider alertSlider;
    public new Camera camera;
    public float armLenght = 2f;
    public float laserAmmountPercentage = 0.1f;

    private List<GameObject> lasers;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            alertSlider.value = 1f;
        }
    }

    private void Awake()
    {
        lasers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Laser"));
    }
    private void Start()
    {
        InitLasers();
    }

    void Update()
    {
        Vector3 position = camera.transform.position;
        Vector3 target = position + camera.transform.forward * armLenght;

        if (Input.GetMouseButtonDown(0) && Physics.Linecast(position, target, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Laser"))
            {
                Destroy(hit.collider.gameObject);        
            }
        }
    }

    private void InitLasers()
    {
        int lasersToDestroy = (int)(lasers.Count - lasers.Count * laserAmmountPercentage);

        int count = 0;
        while (count != lasersToDestroy)
        {
            foreach (var laser in lasers.ToArray())
            {
                if (count == lasersToDestroy)
                {
                    return;
                }
                if (Random.value > 0.5f)
                {
                    count++;
                    lasers.Remove(laser);
                    Destroy(laser);
                }
            }
        }
    }
}
