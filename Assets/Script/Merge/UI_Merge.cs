using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Merge : MonoBehaviour
{
    public Button DEL_SLOT1;
    public Button DEL_SLOT2;
    public Button MERGE;

    public Element slot1, slot2;
    public MergeElement SlotMerge;

    public Transform SlotParent1, SlotParent2, SlotParentMerge, Eventory;
    public GameObject prefabsItemMerge;
    public GameObject prefabsItemEventory;

    GameObject Item1Render, Item2Render, ItemMergeRender;

    Dictionary<ECharacterType, int> keyValuePairs = new Dictionary<ECharacterType, int>();

    private void Start()
    {
        MERGE.onClick.AddListener(OnMergeBtn);
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);

        Spawn();
    }
    void Spawn()
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
    public void ADD_SLOT(Element CrtterItem, Action<bool> successed)
    {
        if (slot1 == null)
        {
            slot1 = CrtterItem;
            LoadDataElement(1);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
            MergeCritter();
        }
        else if (slot2 == null)
        {
            slot2 = CrtterItem;
            LoadDataElement(2);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
            MergeCritter();
        }
        else
        {
            successed?.Invoke(false);
        }
    }
    public void MergeCritter()
    {
        if (slot1 != null && slot2 != null)
        {
            ItemMergeRender = Instantiate(prefabsItemMerge);
            SlotMerge = ItemMergeRender.GetComponent<MergeElement>();

            if (SlotMerge != null)
            {
                SlotMerge.Type = Controller.Instance.mergeElementData.Child(slot1.Type, slot2.Type);
                SlotMerge.Init();
                ItemMergeRender.transform.SetParent(SlotParentMerge);
                ItemMergeRender.transform.localPosition = Vector3.zero;
                ItemMergeRender.transform.localScale = Vector3.one;
            }
        }
    }
    public void LoadDataElement(int index)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(prefabsItemMerge);
            Item1Render.GetComponent<MergeElement>().Type = slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init();

            Item1Render.transform.SetParent(SlotParent1);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;
        }
        else
        {
            Item2Render = Instantiate(prefabsItemMerge);

            Item2Render.GetComponent<MergeElement>().Type = slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init();
            Item2Render.transform.SetParent(SlotParent2);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;
        }
    }
    public void RemoveItem(int index)
    {
        if (index == 1)
        {
            Destroy(ItemMergeRender);
            if (Item1Render == null) return;

            Destroy(Item1Render);
            DEL_SLOT1.gameObject.SetActive(false);
            slot1.gameObject.transform.SetAsLastSibling();
            slot1.gameObject.SetActive(true);
            slot1 = null;
        }
        else if (index == 2)
        {
            Destroy(ItemMergeRender);
            if (Item2Render == null) return;

            Destroy(Item2Render);
            DEL_SLOT2.gameObject.SetActive(false);
            slot2.gameObject.transform.SetAsLastSibling();
            slot2.gameObject.SetActive(true);
            slot2 = null;
        }
    }
    private void OnMergeBtn()
    {
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);

        if (!keyValuePairs.ContainsKey(SlotMerge.Type))
        {
            keyValuePairs.Add(SlotMerge.Type, 1);
        }
        else
        {
            keyValuePairs[SlotMerge.Type] += 1;
        }
        GameObject MergeDone = Instantiate(prefabsItemEventory);
        MergeDone.transform.SetParent(Eventory);
        MergeDone.transform.localPosition = Vector3.zero;
        MergeDone.transform.localScale = Vector3.one;

        MergeDone.GetComponent<Element>().Type = SlotMerge.Type;
        MergeDone.GetComponent<Element>().Init();

        UI_Home.Instance.m_UIPopUp._ShowPopUpSuccess(SlotMerge);

        Destroy(SlotMerge.gameObject);
        SlotMerge = null;

        keyValuePairs[slot1.Type] -= 1;
        keyValuePairs[slot2.Type] -= 1;

        Destroy(Item1Render);
        Destroy(Item2Render);
        Destroy(slot1.gameObject);
        Destroy(slot2.gameObject);

        foreach (var kv in keyValuePairs)
        {
            ObscuredPrefs.SetInt(kv.Key.ToString(), kv.Value);
        }
    }
}
