using UnityEngine;
using System.Collections;

public class TranslationDebug : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (LanguageUsed.Instance.CurrentLanguage < LanguageSerializer.Instance.languages.Count - 1)
            {
                LanguageUsed.Instance.CurrentLanguage++;
            }
            else
            {
                LanguageUsed.Instance.CurrentLanguage = 0;
            }
        }
	}
}
