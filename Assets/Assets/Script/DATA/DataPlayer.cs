using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using CodeStage.AntiCheat.ObscuredTypes;

public static class DataPlayer
{

    private const string ALL_DATA = "ALL_DATA";
    private const string ALL_DATA_ALLID = "ALL_DATA_ALLID";
    private static DataInfoPlayer dataInfoPlayer;
    private static DataAllid dataAllid;

    static DataPlayer()
    {
        dataInfoPlayer = JsonConvert.DeserializeObject<DataInfoPlayer>(PlayerPrefs.GetString(ALL_DATA));

        if (dataInfoPlayer == null)
        {
            dataInfoPlayer = new DataInfoPlayer();
        }
        dataAllid = JsonConvert.DeserializeObject<DataAllid>(PlayerPrefs.GetString(ALL_DATA_ALLID));

        if (dataAllid == null)
        {
            dataAllid = new DataAllid(3);
        }
        SaveData();
        SaveDataAllied();
    }

    private static void SaveData()
    {
        string json = JsonConvert.SerializeObject(dataInfoPlayer);
        PlayerPrefs.SetString(ALL_DATA, json);
    }
    private static void SaveDataAllied()
    {
        string json = JsonConvert.SerializeObject(dataAllid);
        PlayerPrefs.SetString(ALL_DATA_ALLID, json);
    }
    public static void Log()
    {
        string json = JsonConvert.SerializeObject(dataInfoPlayer);
        Debug.Log(json);
    }
    public static void Add(ECharacterType Key)
    {
        Debug.Log(Key);
        dataInfoPlayer.Add(Key);
        SaveData();
    }
    public static void Add(ElementData elementData)
    {
        dataInfoPlayer.Add(elementData);
        SaveData();
    }
    public static void Remove(ECharacterType Key, int ID)
    {
        dataInfoPlayer.Remove(Key, ID);
        SaveData();
    }
    public static bool Checkey(ECharacterType Key)
    {
        return dataInfoPlayer.CheckKey(Key);
    }
    public static Dictionary<ECharacterType, List<ElementData>> GetDictionary()
    {
        return dataInfoPlayer.keyValuePairs;
    }
    public static List<ElementData> GetListAllid()
    {
        return dataAllid.keyValueAllid;
    }
    public static void AddAlliedIteam(ElementData elementData)
    {
        dataAllid.AddAllidItem(elementData);
        SaveDataAllied();
    }
    public static void RemoveItem(ElementData elementData)
    {
        dataAllid.RemoveItem(elementData);
        SaveDataAllied();
    }
}
public class DataAllid
{
    public List<ElementData> keyValueAllid = new List<ElementData>();
    public DataAllid(int count)
    {
        keyValueAllid = new List<ElementData>();
        for (int i = 0; i < count; i++)
        {
            ElementData elementData = new ElementData();
            elementData.Type = ECharacterType.NONE;
            keyValueAllid.Add(elementData);
        }
    }

    public void AddAllidItem(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == ECharacterType.NONE)
            {
                keyValueAllid[i].Type = m_elementData.Type;
                keyValueAllid[i].ID = m_elementData.ID;
                keyValueAllid[i].HP = m_elementData.HP;
                break;
            }
        }
    }

    public void RemoveItem(ElementData m_elementData)
    {
        for (int i = 0; i < keyValueAllid.Count; i++)
        {
            if (keyValueAllid[i].Type == m_elementData.Type && keyValueAllid[i].ID == m_elementData.ID)
            {
                keyValueAllid[i].Type = ECharacterType.NONE;
                keyValueAllid[i].ID = 0;
                keyValueAllid[i].HP = 0;
                break;
            }
        }
    }
}
public class DataInfoPlayer
{
    public Dictionary<ECharacterType, List<ElementData>> keyValuePairs = new Dictionary<ECharacterType, List<ElementData>>();
    public bool CheckKey(ECharacterType Key)
    {
        return keyValuePairs.ContainsKey(Key);
    }
    public void Add(ECharacterType Key)
    {
        Debug.Log(Key);
        if (keyValuePairs.ContainsKey(Key))
        {
            List<ElementData> L_elementData = keyValuePairs[Key];
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = Key;
            m_elemenData.ID = L_elementData[L_elementData.Count - 1].ID + 1;
            m_elemenData.HP = Controller.Instance.enemyData.EnemyStatIndex(Key).HP;
            L_elementData.Add(m_elemenData);

            keyValuePairs[Key] = L_elementData;
        }
        else
        {
            List<ElementData> L_elementData = new List<ElementData>();
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = Key;
            m_elemenData.ID = 0;
            m_elemenData.HP = Controller.Instance.enemyData.EnemyStatIndex(Key).HP;
            L_elementData.Add(m_elemenData);
            keyValuePairs.Add(Key, L_elementData);
        }
    }
    public void Add(ElementData elementData)
    {
        if (keyValuePairs.ContainsKey(elementData.Type))
        {
            List<ElementData> L_elementData = keyValuePairs[elementData.Type];
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = elementData.Type;
            m_elemenData.ID = elementData.ID;
            m_elemenData.HP = elementData.HP;
            L_elementData.Add(m_elemenData);

            keyValuePairs[elementData.Type] = L_elementData;
        }
        else
        {
            List<ElementData> L_elementData = new List<ElementData>();
            ElementData m_elemenData = new ElementData();
            m_elemenData.Type = elementData.Type;
            m_elemenData.ID = elementData.ID;
            m_elemenData.HP = elementData.HP;
            L_elementData.Add(m_elemenData);
            keyValuePairs.Add(m_elemenData.Type, L_elementData);
        }
    }
    public void Remove(ECharacterType Key, int ID)
    {
        List<ElementData> L_elementData = keyValuePairs[Key];
        if (L_elementData.Count == 1)
        {
            keyValuePairs.Remove(Key);
        }
        else
        {
            for (int i = 0; i < L_elementData.Count; i++)
            {
                if (L_elementData[i].ID == ID)
                {
                    L_elementData.RemoveAt(i);
                    break;
                }
            }
        }
    }
}

public class ElementData
{
    public ECharacterType Type;
    public int ID;
    public int HP;
}
