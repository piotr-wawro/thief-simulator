using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject backgroundSound;
    public GameObject pauseMenu;
    public GameObject events;
    private AudioSource audioSource;
    
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        backgroundSound.GetComponent<AudioSource>().UnPause();
        events.GetComponent<AudioSource>().UnPause();
        audioSource.Stop();
        Destroy(audioSource);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        backgroundSound.GetComponent<AudioSource>().Pause();
        events.GetComponent<AudioSource>().Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        var audioMixerGroup = Resources.Load<AudioMixer>("MainMixer").FindMatchingGroups("Music")[0];
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.clip = Resources.Load<AudioClip>("Sounds/Music/Fiberitron Loop");
        audioSource.Play();
    }

    public void CreateNewGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
