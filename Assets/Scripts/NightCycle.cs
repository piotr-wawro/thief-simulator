using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightCycle : MonoBehaviour
{
    public GameObject backgroundSound;
    public GameObject bustedCanvas;
    public float timeToEnd = 180;
    public float policeSiren = 30;
    public float policeLights = 10;

    public float time = 0;

    private bool isSirenOn = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= timeToEnd)
        {
            backgroundSound.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().Stop();
            Cursor.lockState = CursorLockMode.None;
            bustedCanvas.SetActive(true);
        }

        if (timeToEnd - time < policeSiren)
        {
            if (!isSirenOn)
            {
                gameObject.GetComponent<AudioSource>().Play();
                isSirenOn = true;
            }
            if (gameObject.GetComponent<AudioSource>().isPlaying)
            {
                //gameObject.GetComponent<AudioSource>().volume = Normalize(0, policeSiren, 0, 1, policeSiren - (timeToEnd - time));
                gameObject.GetComponent<AudioSource>().volume = Logarithmic(0.8f, timeToEnd - time);
            }

            if (timeToEnd - time < policeLights)
            {
                if (time % 2 < 1)
                {
                    var red = Normalize(0, 1, 0, 0.2f, Logarithmic(0.01f, timeToEnd - time));
                    RenderSettings.ambientSkyColor = new Color(red, 0.01568627f, 0.01568627f);
                }
                else
                {
                    var blue = Normalize(0, 1, 0, 0.2f, Logarithmic(0.01f, timeToEnd - time));
                    RenderSettings.ambientSkyColor = new Color(0.01568627f, 0.01568627f, blue);
                }
            }
        }
    }

    private float Normalize(float min, float max, float newMin, float newMax, float val)
    {
        return (val - min) * (newMax - newMin) / (max - min) + newMin;
    }

    private float Logarithmic(float a, float t)
    {
        return Mathf.Pow(0.8f, t);
    }
}
