using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMergeElement", menuName = "CreateMergeElement")]
public class MergeElementData : ScriptableObject
{
    [SerializeField]
    public List<MergeElementStat> enemies = new List<MergeElementStat>();
    public List<string[]> test = new List<string[]>();
    public ECharacterType Child(ECharacterType parent, ECharacterType mother)
    {
        return enemies[(int)parent].TypeChild[(int)mother];
    }
#if UNITY_EDITOR
    [Button("Load Data")]
    private void LoadData()
    {
        enemies = new List<MergeElementStat>();

        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=636966808&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            int m = data[0].Length;


            Debug.Log("N = " + n + " M = " + m);
            for (int i = 1; i < n; i++)
            {
                MergeElementStat item = new MergeElementStat();
                item.Type = Utils.ToEnum<ECharacterType>(data[i][0]);
                Debug.Log("data: " + item.Type);

                item.TypeChild = new List<ECharacterType>();
                for (int j = 1; j < m; j++)
                {
                    ECharacterType tmp = ECharacterType.NONE;
                    if (string.IsNullOrEmpty(data[i][j]))
                    {
                        if (!string.IsNullOrEmpty(data[j][i]))
                        {
                            tmp = Utils.ToEnum<ECharacterType>(data[j][i]);
                        }
                    }
                    else
                    {
                        tmp = Utils.ToEnum<ECharacterType>(data[i][j]);
                    }
                    item.TypeChild.Add(tmp);
                }
                enemies.Add(item);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
}
#endif
[System.Serializable]
public class MergeElementStat
{
    [FoldoutGroup("$Type")]
    public ECharacterType Type;
    [FoldoutGroup("$Type")]
    public List<ECharacterType> TypeChild;
  

}