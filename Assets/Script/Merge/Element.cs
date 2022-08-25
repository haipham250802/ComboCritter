using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
public class Element : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public int Hp;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;

    public Button PurchaseBtn;

    public Image ICON;
    private UI_Merge UIController;
    private UI_Team UITeam;
    private void Start()
    {
        UIController = FindObjectOfType<UI_Merge>();
        UITeam = FindObjectOfType<UI_Team>();
    }
    public void Init()
    {
        if (PurchaseBtn != null)
            PurchaseBtn.onClick.AddListener(Load_Data_UI_Team);
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
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
    void LoadDataButton()
    {
        UIController.ADD_SLOT(this, success =>
        {
            this.gameObject.SetActive(!success);
        });
    }
    void Load_Data_UI_Team()
    {
        Debug.Log("da ban");
        UITeam.ADD_SLOT_ELEMENT_TEAM(this, success =>
        {
            this.gameObject.SetActive(!success);
        });
    }
    public void setIcon(Sprite icon)
    {
        ICON.sprite = icon;
    }
}
