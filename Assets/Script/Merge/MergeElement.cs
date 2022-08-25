using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class MergeElement : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public string Name;
    [FoldoutGroup("$Type")] public int Hp;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;
    [FoldoutGroup("$Type")]  public Image ICON;
    [FoldoutGroup("$Type")]  public UI_Merge UIController;

    private void Start()
    {
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
        Name = Type.ToString();
        Hp = stat.HP;
        Damage = stat.Damage;
        Rarity = stat.Rarity;
        ICON.sprite = stat.ICON;

        SetView();
    }
    public void Init()
    {
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
        Name = Type.ToString();
        Hp = stat.HP;
        Damage = stat.Damage;
        Rarity = stat.Rarity;
        ICON.sprite = stat.ICON;

        SetView();
    }
    public void SetView()
    {
        TxtHP.text = Hp.ToString();
        TxtDamage.text = Damage.ToString();
    }
    public void SetIcon(Sprite ICON)
    {
        this.ICON.sprite = ICON;
    }


}
