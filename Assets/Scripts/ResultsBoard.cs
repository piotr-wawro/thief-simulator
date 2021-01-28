using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsBoard : MonoBehaviour
{
    public GameObject player;
    public GameObject events;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textResult;

    private void Start()
    {
        var moneyPicker = player.GetComponent<MoneyPicker>();
        var onSceneLoad = events.GetComponent<OnSceneLoad>();
        var nightCycle = events.GetComponent<NightCycle>();

        textMoney.text = moneyPicker.count.ToString() + "/" + onSceneLoad.itemCount.ToString();
        textTime.text = nightCycle.time.ToString("N3") + "s";
        textResult.text = (100 * (float)moneyPicker.count / nightCycle.time).ToString("N3");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
