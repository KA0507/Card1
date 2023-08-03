using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public const string KEY_BUTTON_CARD = "ButtonCard";
    public const string KEY_BUTTON_CARDTYPE = "ButtonCardType";
    public const string KEY_BUTTON_POOLTYPE = "ButtonPoolType";
    public const string KEY_COIN = "Coin";

    private void Awake()
    {
        /*for (int i = 0; i < 2; i++)
        {
            SetEnumData<PoolType>(KEY_BUTTON_POOLTYPE + i, PoolType.Digit_Add3);
            SetEnumData<CardType>(KEY_BUTTON_CARDTYPE + i, CardType.Digits);
        }
        SetEnumData<PoolType>(KEY_BUTTON_POOLTYPE + 2, PoolType.Digit_Div2);
        SetEnumData<CardType>(KEY_BUTTON_CARDTYPE + 2, CardType.Digits);

        SetEnumData<PoolType>(KEY_BUTTON_POOLTYPE + 3, PoolType.Digit_Sub3);
        SetEnumData<CardType>(KEY_BUTTON_CARDTYPE + 3, CardType.Digits);*/
    }
    // Thay đổi giá trị của key trong PlayerPrefs và varible
    public void SetEnumData<T>(string key, ref T varible, T value)
    {
        varible = value;
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    // Thay đổi giá trị của key trong PlayerPrefs
    public void SetEnumData<T>(string key, T value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
    }

    // Lấy enum theo key
    public T GetEnumData<T>(string key, T defaul)
    {
        return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaul)));
    }
}
