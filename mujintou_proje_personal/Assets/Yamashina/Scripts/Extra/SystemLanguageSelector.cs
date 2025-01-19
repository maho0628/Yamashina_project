using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
[System.Serializable]

public class SystemLanguageSelector : UnityEngine.Localization.Settings.IStartupLocaleSelector

{




    public Locale GetStartupLocale(ILocalesProvider provider)
    {
        return provider.GetLocale(Application.systemLanguage);
    }
}

