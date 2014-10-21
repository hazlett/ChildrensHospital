using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class Languages {

    private static Languages instance = new Languages();
    public static Languages Instance { get { return instance; } }
    private Dictionary<string, List<string>> translations;
    
    private Languages()
    {
        translations = new Dictionary<string, List<string>>();
        LanguageUsed.Instance.Load();
        LanguageSerializer.Instance.Load();
        foreach (TranslatorList translatorList in LanguageSerializer.Instance.translatorList)
        {
            try
            {
                translations.Add(translatorList.key, translatorList.translations);
            }
            catch (Exception) { }
        }
    }
    public string GetTranslation(string key)
    {
        try
        {
            return translations[key][LanguageUsed.Instance.CurrentLanguage];
        }
        catch (Exception)
        {
            return key;
        }
        
    }
}
