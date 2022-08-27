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
    public UI_Team m_UITeam;

    public List<ElementData> L_elementData = new List<ElementData>();

    private void Start()
    {
        MERGE.onClick.AddListener(OnMergeBtn);
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);
        // Spawn();

        m_UITeam.Spawn(Eventory);
    }
    /*  void Spawn()
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
      }*/
    public void ADD_SLOT(Element CrtterItem, Action<bool> successed) //, 
    {
        if (slot1 == null)
        {
            slot1 = CrtterItem;
            LoadDataElement(1, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
            MergeCritter();
        }
        else if (slot2 == null)
        {
            slot2 = CrtterItem;
            LoadDataElement(2, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
            MergeCritter();
        }
        else
        {
            //  successed?.Invoke(false);
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
                ItemMergeRender.transform.SetParent(SlotParentMerge);
                ItemMergeRender.transform.localPosition = Vector3.zero;
                ItemMergeRender.transform.localScale = Vector3.one;
                SlotMerge.Init();
            }
        }
    }
    public void LoadDataElement(int index, ElementData elementData)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(prefabsItemMerge);
            Item1Render.GetComponent<MergeElement>().Type = slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init(elementData);

            Item1Render.transform.SetParent(SlotParent1.transform);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;

            // DataPlayer.AddAlliedIteam(elementData);
            // DataPlayer.Remove(elementData.Type, elementData.ID);

            L_elementData.Add(elementData);
        }
        else
        {
            Item2Render = Instantiate(prefabsItemMerge);

            Item2Render.GetComponent<MergeElement>().Type = slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init(elementData);
            Item2Render.transform.SetParent(SlotParent2);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;

            //DataPlayer.AddAlliedIteam(elementData);
            // DataPlayer.Remove(elementData.Type, elementData.ID);
            L_elementData.Add(elementData);
        }
    }
    public void RemoveItem(int index)
    {
        ElementData elementdata = new ElementData();
        if (index == 1)
        {
            Destroy(ItemMergeRender);
            if (Item1Render == null) return;

            Destroy(Item1Render);

            DataPlayer.Add(elementdata);
            DEL_SLOT1.gameObject.SetActive(false);
            slot1.gameObject.transform.SetAsLastSibling();
            //   slot1.gameObject.SetActive(true);
            LoadEventory(Eventory.gameObject);
            slot1 = null;
        }
        else if (index == 2)
        {
            Destroy(ItemMergeRender);
            if (Item2Render == null) return;

            Destroy(Item2Render);

            DataPlayer.Add(elementdata);
            DEL_SLOT2.gameObject.SetActive(false);
            slot2.gameObject.transform.SetAsLastSibling();
            //   slot2.gameObject.SetActive(true);
            LoadEventory(Eventory.gameObject);

            slot2 = null;
        }
    }
    private void OnMergeBtn()
    {
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);
        /*
                if (!keyValuePairs.ContainsKey(SlotMerge.Type))
                {
                    keyValuePairs.Add(SlotMerge.Type, 1);
                }
                else
                {
                    keyValuePairs[SlotMerge.Type] += 1;
                }*/
        GameObject MergeDone = Instantiate(prefabsItemEventory);
        // MergeDone.transform.SetParent(Eventory);
        MergeDone.GetComponent<Element>().Init();
        MergeDone.transform.localPosition = Vector3.zero;
        MergeDone.transform.localScale = Vector3.one;
        MergeDone.GetComponent<Element>().Type = SlotMerge.Type;
        DataPlayer.Add(SlotMerge.Type);

        UI_Home.Instance.m_UIPopUp._ShowPopUpSuccess(SlotMerge);
        Destroy(SlotMerge.gameObject);
        SlotMerge = null;

        /*      keyValuePairs[slot1.Type] -= 1;
                keyValuePairs[slot2.Type] -= 1;*/

        Destroy(Item1Render);
        Destroy(Item2Render);
        Destroy(slot1.gameObject);
        Destroy(slot2.gameObject);
        for (int i = 0; i < L_elementData.Count; i++)
        {
            DataPlayer.Remove(L_elementData[i].Type, L_elementData[i].ID);
        }
        LoadEventory(Eventory.gameObject);
        /*        foreach (var kv in keyValuePairs)
                {
                    ObscuredPrefs.SetInt(kv.Key.ToString(), kv.Value);
                }*/
    }
    public void LoadEventory(GameObject eventory)
    {
        var arr = eventory.GetComponentsInChildren<Element>();
        for (int i = 0; i < arr.Length; i++)
        {
            Destroy(arr[i].gameObject);
        }
        m_UITeam.Spawn(Eventory.transform);
    }

}
