using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
public class Element : MonoBehaviour
{
    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int Rarity;
    [FoldoutGroup("$Type")] public Text TxtHP;
    [FoldoutGroup("$Type")] public Text TxtDamage;

    public Button PurchaseBtn;
    public Image ICON;
    private UI_Merge UIController;
    private UI_Team UITeam;

    public ElementData ThisElementData;
    private void Start()
    {
        UIController = FindObjectOfType<UI_Merge>();
        UITeam = FindObjectOfType<UI_Team>();

        for(int i = 0; i< DataPlayer.GetListAllid().Count;i++)
        {
            if(DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
            {
                Load_Data_UI_Team();
            }
        }
    }
    public void Init(ElementData elemendata = null)
    {
        if (PurchaseBtn != null)
            PurchaseBtn.onClick.AddListener(Load_Data_UI_Team);
        if (elemendata != null)
        {
            ThisElementData = elemendata;
            Type = elemendata.Type;
        }
        EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
        Damage = stat.Damage;
        Rarity = stat.Rarity;
        ICON.sprite = stat.ICON;

        SetView();
    }
    public void SetView()
    {
        if (ThisElementData != null)
        {
            TxtHP.text = ThisElementData.HP.ToString();
        }
        else
        {
            TxtHP.text = HP.ToString();
        }
        TxtDamage.text = Damage.ToString();
    }
    void LoadDataButton()
    {
        UIController.ADD_SLOT(this, success =>
        {
            this.gameObject.SetActive(!success);
        });
    }
    public void Load_Data_UI_Team()
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
