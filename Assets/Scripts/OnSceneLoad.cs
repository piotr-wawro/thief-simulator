using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoad : MonoBehaviour
{
    public int itemCount;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        GameObject[] cash = GameObject.FindGameObjectsWithTag("cash");
        List<GameObject> cashList = new List<GameObject>(cash);

        List<GameObject> toSave = new List<GameObject>();

        System.Random random = new System.Random();
        int rndIdx;

        for(int i = 0; i < itemCount; i++) {
            rndIdx = random.Next(0, cashList.Count);
            toSave.Add(cashList[rndIdx]);
            cashList.RemoveAt(rndIdx);
        }

        foreach(GameObject e in cashList) {
            Destroy(e);
        }

        Cursor.visible = false;
    }
}
