using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Busted : MonoBehaviour
{
    public GameObject player;
    public GameObject events;

    public TextMeshProUGUI textMoney;
    void Start()
    {
        var moneyPicker = player.GetComponent<MoneyPicker>();
        var onSceneLoad = events.GetComponent<OnSceneLoad>();
        textMoney.text = moneyPicker.count.ToString() + "/" + onSceneLoad.itemCount.ToString();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
