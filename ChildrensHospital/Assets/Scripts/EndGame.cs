using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    private static EndGame instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            EventLogger.Instance.LogData("Game Exited");
        }
    }
}
