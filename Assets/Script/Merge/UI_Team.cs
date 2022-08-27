using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Team : MonoBehaviour
{
    public GameObject Eventory;
    public GameObject prefabsItemEventory;

    GameObject Item1Render, Item2Render, Item3Render;
    public GameObject ItemBased;

    public Element Slot1;
    public Element Slot2;
    public Element Slot3;

    public GameObject SlotParent01, SlotParent02, SlotParent03;

    public Button DEL_SLOT1, DEL_SLOT2, DEL_SLOT3;

    Dictionary<ECharacterType, int> keyValuePairs = new Dictionary<ECharacterType, int>();
    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
        {
            ECharacterType Key = Controller.Instance.GetTypeIndex(i);
            if (ObscuredPrefs.HasKey(Key.ToString()))
            {
                keyValuePairs.Add(Key, ObscuredPrefs.GetInt(Key.ToString()));
            }
        }
        foreach (var kv in keyValuePairs)
        {
            for (int i = 0; i < kv.Value; i++)
            {
                GameObject tempItem = Instantiate(prefabsItemEventory);

                tempItem.transform.SetParent(Eventory.transform);
                tempItem.transform.localScale = Vector3.one;
                tempItem.transform.localPosition = Vector3.zero;

                tempItem.GetComponent<Element>().Type = kv.Key;
                tempItem.GetComponent<Element>().Init();
            }
        }
    }
    public void ADD_SLOT_ELEMENT_TEAM(Element CrtterItem, Action<bool> successed)
    {
        if (Slot1 == null)
        {
            Slot1 = CrtterItem;
            LoadDataElement(1);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
        }
        else if (Slot2 == null)
        {
            Slot2 = CrtterItem;
            LoadDataElement(2);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
        }
        else if(Slot3 == null)
        {
            Slot3 = CrtterItem;
            LoadDataElement(3);
            successed?.Invoke(true);
            DEL_SLOT3.gameObject.SetActive(true);
        }
        else
        {
            successed?.Invoke(false);
        }
    }
    public void LoadDataElement(int index)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(ItemBased);
            Item1Render.GetComponent<MergeElement>().Type = Slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init();

            Item1Render.transform.SetParent(SlotParent01.transform);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;
        }
        if (index == 2)
        {
            Item2Render = Instantiate(ItemBased);

            Item2Render.GetComponent<MergeElement>().Type = Slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init();
            Item2Render.transform.SetParent(SlotParent02.transform);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;
        }
        if(index == 3)
        {
            Item3Render = Instantiate(ItemBased);

            Item3Render.GetComponent<MergeElement>().Type = Slot3.Type;
            Item3Render.GetComponent<MergeElement>().Init();
            Item3Render.transform.SetParent(SlotParent03.transform);
            Item3Render.transform.localPosition = Vector3.zero;
            Item3Render.transform.localScale = Vector3.one;
        }
    }
    public void RemoveItem(int index)
    {
        if (index == 1)
        {
            if (Item1Render == null) return;
            
        
            Destroy(Item1Render);
            DEL_SLOT1.gameObject.SetActive(false);
            Slot1.gameObject.transform.SetAsLastSibling();
            Slot1.gameObject.SetActive(true);
            Slot1 = null;
        }
        if (index == 2)
        {
            if (Item2Render == null) return;

            Destroy(Item2Render);
            DEL_SLOT2.gameObject.SetActive(false);
            Slot2.gameObject.transform.SetAsLastSibling();
            Slot2.gameObject.SetActive(true);
            Slot2 = null;
        }
        if(index == 3)
        {
            if (Item3Render == null) return;

            Destroy(Item3Render);
            DEL_SLOT3.gameObject.SetActive(false);
            Slot3.gameObject.transform.SetAsLastSibling();
            Slot3.gameObject.SetActive(true);
            Slot3 = null;
        }
    }
}
