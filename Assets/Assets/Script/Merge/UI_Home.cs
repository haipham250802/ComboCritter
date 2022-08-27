using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Home : MonoBehaviour
{
    private static UI_Home instance;

    public UI_Team m_UITeam;
    public UI_Merge m_UIMerge;
    public UIPopUp m_UIPopUp;

    string ALLID_1 = "ALLID_1";
    string ALLID_2 = "ALLID_2";
    string ALLID_3 = "ALLID_3";


    ECharacterType E_TypeAllid_1;
    ECharacterType E_TypeAllid_2;
    ECharacterType E_TypeAllid_3;
    public static UI_Home Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UI_Home();

            }
            return instance;
        }
    }
    public void LoadData()
    {
        if (ObscuredPrefs.HasKey(ALLID_1))
        {
            E_TypeAllid_1 = Utils.ToEnum<ECharacterType>(ObscuredPrefs.GetString(ALLID_1));
        }
        else
        {
            E_TypeAllid_1 = ECharacterType.NONE;
            ObscuredPrefs.SetString(ALLID_1, E_TypeAllid_1.ToString());
        }

        if(ObscuredPrefs.HasKey(ALLID_2))
        {
            E_TypeAllid_2 = Utils.ToEnum<ECharacterType>(ObscuredPrefs.GetString(ALLID_2));
        }
        else
        {
            E_TypeAllid_2 = ECharacterType.NONE;
            for (int i = 0; i < 3; i++)
            {
                ObscuredPrefs.SetString(ALLID_2, E_TypeAllid_2.ToString());
            }
        }

        if(ObscuredPrefs.HasKey(ALLID_3))
        {
            E_TypeAllid_3 = Utils.ToEnum<ECharacterType>(ObscuredPrefs.GetString(ALLID_3));
        }
        else
        {
            E_TypeAllid_3 = ECharacterType.NONE;
            ObscuredPrefs.SetString(ALLID_3, E_TypeAllid_3.ToString());
        }
    }
    private void Awake()
    {
        instance = this;
        LoadData();
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}


