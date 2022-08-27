using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Enemy")]
public class EnemyData : ScriptableObject
{
    public Sprite Icon;
    [SerializeField]
    public List<EnemyStat> enemies =  new List<EnemyStat>();
    public EnemyStat EnemyStatIndex(ECharacterType type)
    {
        return enemies[(int) type];
    }
#if UNITY_EDITOR
    [Button("Load Data")]
    private void LoadData()
    {
        enemies = new List<EnemyStat>();

        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=0&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 1; i < n; i++)
            {
                EnemyStat item = new EnemyStat();
                ECharacterType tmp = Utils.ToEnum<ECharacterType>(data[i][0]);
                item.Type = tmp;

                item.Damage = int.Parse( data[i][1]);

                item.HP = int.Parse(data[i][2]);

                item.Rarity = int.Parse(data[i][3]);

                item.EnocunterATK = int.Parse(data[i][4]);

                item.EncounterHP  = int.Parse(data[i][5]);
                item.ICON  = Icon;

                if (string.IsNullOrEmpty(data[i][6])) item.CatchChance = 0;
                else item.CatchChance = int.Parse(data[i][6]);

                if (string.IsNullOrEmpty(data[i][7])) item.CombineCost = 0;     
                else item.CombineCost = int.Parse(data[i][7]);

                enemies.Add(item);  
            }

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));

    }
#endif
}
[System.Serializable]
public class EnemyStat
{
    [FoldoutGroup("$Type")]
    public Sprite ICON;
    [FoldoutGroup("$Type")]
    public ECharacterType Type;
    [FoldoutGroup("$Type")]
    public int Damage;
    [FoldoutGroup("$Type")]
    public int HP;
    [FoldoutGroup("$Type")]
    public int Rarity;
    [FoldoutGroup("$Type")]
    public int EnocunterATK;
    [FoldoutGroup("$Type")]
    public int EncounterHP;
    [FoldoutGroup("$Type")]
    public int CatchChance;
    [FoldoutGroup("$Type")]
    public int CombineCost;
}
public enum ECharacterType
{
    NONE = -1,
    BlazingTurtle = 0,
    Pouteel = 1,
    Usaseal,
    Sucker,
    Baanu,
    Crawlson,
    Stabbycarrot,
    Grog,
    Squito,
    Shelly,
    Mimic,
    Goater,
    Punchycarrot,
    Pepe,
    Octopussy,
    Zombear,
    Goldcarp,
    Peter,
    Bigga,
    Pinchyfrog,
    Cloudy,
    Snailmer,
    Cryman,
    Flowser,
    Panmol,
    Cabra,
    Anta,
    Camotus,
    Golcro,
    Empogan,
    Bananaman,
    Eggbir,
    Octoking,
    Reptar,
    Beedeer,
    Illibird,
    Sharpie,
    Aubergiman,
    Lizargo,
    Snaker,
    Chocowal,
    Captaincarrot,
    Broccotree,
    Bunner,
    Crybaman,
    Bonare,
    Eyemeleon,
    Phenux,
    Wormer,
    Bonerex,
    Goatix,
    Bigbun,
    Tombi,
    Cowcho,
    Fatmeow,
    Tortolla,
    Goldcat,
    Heuhuebo,
    Jiangshi,
    Lolibun,
    Skullfly,
    Deermi,
    Octobear,
    Axerret,
    Carebo,
    Shellbert,
    Bhino,
    Schytherret,
    Oculus,
    Crocadoodledoo,
    Cupocat,
    Snacat,
    Skipleg,
    Doman,
    Chillrex,
    Dragonpuff,
    LuckyKandy,
    Sharko,
    Cluckix,
    Gingycane,
    Tofbat,
    Buncorn,
    Rooty,
    Redcat,
    Roostar,
    Fomb,
    Parote,
    Pegomb,
    Gumcat,
    Gingyking,
    Mously,
    Krabon,
    Hoocat,
    Kakotain,
    Puffi,
    Nianbot,
    Sharkon,
    Smucker,
    Stabbystar,
    Shreekin,
    Lapen,
    Lavsna,
    Garg,
    Larry,
    Ascor,
    Fibar,
    Tito,
    Salarhino,
    Molty,
    Lionfu,
    Magamlor,
    Mechacaptaincarrot,
    Mechaoctoking,
    Mechaschytherret,
    Mechacowcho,
    Mechashreekin,
    Mechacluckix,
    Mechagingyking,
    Mechamagmalor,
    Mecharaijin,
}

