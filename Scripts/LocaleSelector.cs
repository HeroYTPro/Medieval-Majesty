using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocaleSelector : MonoBehaviour
{
    public Dropdown languageDropdown; // ������ �� ���������� ������

    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);

        // �������� ������������ �������� ����������� ������
        int savedDropdownIndex = PlayerPrefs.GetInt("DropdownIndex", 0);
        languageDropdown.value = savedDropdownIndex;
    }
    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
        //PlayerPrefs.SetInt("DropdownIndex", languageDropdown.value);
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
    }
    public void OnDropdownValueChanged()
    {
        // ���������� ���������� �������� ����������� ������
        PlayerPrefs.SetInt("DropdownIndex", languageDropdown.value);
    }
}
