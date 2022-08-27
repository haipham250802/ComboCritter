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

    public List<Element> L_element = new List<Element>();
    public List<GameObject> L_G_Render = new List<GameObject>();
    public GameObject SlotParent01, SlotParent02, SlotParent03, SLOT;

    public Button DEL_SLOT1, DEL_SLOT2, DEL_SLOT3;
    public Element m_element;

    //   Dictionary<ECharacterType, int> keyValuePairs = new Dictionary<ECharacterType, int>();
    private void Start()
    {
        // m_element = FindObjectOfType<Element>();
        Spawn(Eventory.transform);
        LoadAllied();

        L_G_Render.Add(Item1Render);
        L_G_Render.Add(Item2Render);
        L_G_Render.Add(Item3Render);

    }
    private void Update()
    {

    }
    public void LoadAllied()
    {
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            Debug.Log("a " + i);

            if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
            {
                m_element = new Element();
                GameObject tempItem = Instantiate(ItemBased);
                tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                tempItem.transform.localScale = Vector3.one;
                tempItem.transform.localPosition = Vector3.zero;
                ElementData element = new ElementData();
                element.Type = DataPlayer.GetListAllid()[i].Type;
                element.ID = DataPlayer.GetListAllid()[i].ID;
                element.HP = DataPlayer.GetListAllid()[i].HP;

                tempItem.GetComponent<MergeElement>().Init(element);
                LoadAllidElement(i, element);

              /*  if (Slot1 != null)
                {
                    m_element.Load_Data_UI_Team();
                }
                else if (Slot2 != null)
                {
                    LoadAllidElement(i, element);
                }
                else if (Slot3 != null)
                {
                    LoadAllidElement(i, element);
                }*/
                // LoadAllidElement(i, DataPlayer.GetListAllid()[i], m_element);
                /* m_element = new Element();
                 ADD_ALLID_ELEMENT_TEAM(i, m_element, DataPlayer.GetListAllid()[i]);*/
            }
        }
    }
    public void LoadAllied(Element m_Element,int index)
    {
        if (DataPlayer.GetListAllid()[index].Type != ECharacterType.NONE)
        {
            m_element = new Element();
            GameObject tempItem = Instantiate(ItemBased);
            tempItem.transform.SetParent(SLOT.transform.GetChild(index));
            tempItem.transform.localScale = Vector3.one;
            tempItem.transform.localPosition = Vector3.zero;
            ElementData element = new ElementData();
            element.Type = DataPlayer.GetListAllid()[index].Type;
            element.ID = DataPlayer.GetListAllid()[index].ID;
            element.HP = DataPlayer.GetListAllid()[index].HP;

            tempItem.GetComponent<MergeElement>().Init(element);
            if (Slot1 != null)
            {
                LoadAllidElement(index, element);
                Slot1 = m_Element;
            }
            else if (Slot2 != null)
            {
                LoadAllidElement(index, element);
                Slot2 = m_Element;
            }
            else if (Slot3 != null)
            {
                LoadAllidElement(index, element);
                Slot2 = m_Element;
            }
            // LoadAllidElement(i, DataPlayer.GetListAllid()[i], m_element);
            /* m_element = new Element();
             ADD_ALLID_ELEMENT_TEAM(i, m_element, DataPlayer.GetListAllid()[i]);*/
        }
    }
    public void Spawn(Transform _Eventory)
    {

        foreach (var kv in DataPlayer.GetDictionary())
        {
            for (int i = 0; i < kv.Value.Count; i++)
            {
                GameObject tempItem = Instantiate(prefabsItemEventory);
                m_element = new Element();
                tempItem.transform.SetParent(_Eventory.transform);
                tempItem.transform.localScale = Vector3.one;
                tempItem.transform.localPosition = Vector3.zero;
                ElementData element = new ElementData();
                element.Type = kv.Key;
                element.ID = kv.Value[i].ID;
                element.HP = kv.Value[i].HP;

                tempItem.GetComponent<Element>().Init(element);
            }
        }
    }
    /*  public void ADD_ALLID_ELEMENT_TEAM(int index, Element CrtterItem, ElementData elementData)
      {
          if (Slot1 == null)
          {
              Slot1 = CrtterItem;
              // LoadDataElement(1, CrtterItem.ThisElementData);
              LoadAllidElement(1, elementData, CrtterItem);

              DEL_SLOT1.gameObject.SetActive(true);
          }
          else if (Slot2 == null)
          {
              Slot2 = CrtterItem;
              // LoadDataElement(2, CrtterItem.ThisElementData);
              LoadAllidElement(2, elementData, CrtterItem);

              DEL_SLOT2.gameObject.SetActive(true);
          }
          else if (Slot3 == null)
          {
              Slot3 = CrtterItem;
              // LoadDataElement(3, CrtterItem.ThisElementData);
              LoadAllidElement(3, CrtterItem.ThisElementData, CrtterItem);

              DEL_SLOT3.gameObject.SetActive(true);
          }

      }*/
    public void ADD_SLOT_ELEMENT_TEAM(Element CrtterItem, Action<bool> successed)
    {
        if (Slot1 == null)
        {
            Slot1 = CrtterItem;
            LoadDataElement(1, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
        }
        else if (Slot2 == null)
        {
            Slot2 = CrtterItem;
            LoadDataElement(2, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
        }
        else if (Slot3 == null)
        {
            Slot3 = CrtterItem;
            LoadDataElement(3, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT3.gameObject.SetActive(true);
        }
        else
        {
            successed?.Invoke(false);
        }
    }
    public void LoadAllidElement(int index, ElementData elemendata)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(ItemBased);
            Item1Render.GetComponent<MergeElement>().Type = Slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init(elemendata);

            Item1Render.transform.SetParent(SlotParent01.transform);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;
            /*DataPlayer.AddAlliedIteam(elemendata);
            DataPlayer.Remove(elemendata.Type, elemendata.ID);*/

        }
        else if (index == 2)
        {
            Item2Render = Instantiate(ItemBased);

            Item2Render.GetComponent<MergeElement>().Type = Slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init(elemendata);
            Item2Render.transform.SetParent(SlotParent02.transform);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;

            /* DataPlayer.AddAlliedIteam(elemendata);
             DataPlayer.Remove(elemendata.Type, elemendata.ID);*/
        }
        else if (index == 3)
        {
            Item3Render = Instantiate(ItemBased);

            Item3Render.GetComponent<MergeElement>().Type = Slot3.Type;
            Item3Render.GetComponent<MergeElement>().Init(elemendata);
            Item3Render.transform.SetParent(SlotParent03.transform);
            Item3Render.transform.localPosition = Vector3.zero;
            Item3Render.transform.localScale = Vector3.one;

            /*  DataPlayer.AddAlliedIteam(elemendata);
              DataPlayer.Remove(elemendata.Type, elemendata.ID);*/
        }
    }
    public void LoadDataElement(int index, ElementData elemendata)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(ItemBased);
            Item1Render.GetComponent<MergeElement>().Type = Slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init(elemendata);

            Item1Render.transform.SetParent(SlotParent01.transform);
            Item1Render.transform.localPosition = Vector3.zero;
            Item1Render.transform.localScale = Vector3.one;
            DataPlayer.AddAlliedIteam(elemendata);
            DataPlayer.Remove(elemendata.Type, elemendata.ID);

        }
        else if (index == 2)
        {
            Item2Render = Instantiate(ItemBased);

            Item2Render.GetComponent<MergeElement>().Type = Slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init(elemendata);
            Item2Render.transform.SetParent(SlotParent02.transform);
            Item2Render.transform.localPosition = Vector3.zero;
            Item2Render.transform.localScale = Vector3.one;

            DataPlayer.AddAlliedIteam(elemendata);
            DataPlayer.Remove(elemendata.Type, elemendata.ID);
        }
        else if (index == 3)
        {
            Item3Render = Instantiate(ItemBased);

            Item3Render.GetComponent<MergeElement>().Type = Slot3.Type;
            Item3Render.GetComponent<MergeElement>().Init(elemendata);
            Item3Render.transform.SetParent(SlotParent03.transform);
            Item3Render.transform.localPosition = Vector3.zero;
            Item3Render.transform.localScale = Vector3.one;

            DataPlayer.AddAlliedIteam(elemendata);
            DataPlayer.Remove(elemendata.Type, elemendata.ID);
        }
    }
    public void RemoveItem(int index)
    {
        ElementData elementdata = new ElementData();
        if (index == 1)
        {
            if (Item1Render == null) return;

            Destroy(Item1Render);

            DataPlayer.Add(elementdata);
            DataPlayer.RemoveItem(elementdata);

            DEL_SLOT1.gameObject.SetActive(false);
            Slot1.gameObject.transform.SetAsLastSibling();
            UI_Home.Instance.m_UIMerge.LoadEventory(Eventory.gameObject);

            //  Slot1.gameObject.SetActive(true);
            Slot1 = null;
        }
        else if (index == 2)
        {
            if (Item2Render == null) return;

            Destroy(Item2Render);

            DataPlayer.Add(elementdata);
            DataPlayer.RemoveItem(elementdata);
            DEL_SLOT2.gameObject.SetActive(false);
            Slot2.gameObject.transform.SetAsLastSibling();
            UI_Home.Instance.m_UIMerge.LoadEventory(Eventory.gameObject);

            //         Slot2.gameObject.SetActive(true);
            Slot2 = null;
        }
        else if (index == 3)
        {
            if (Item3Render == null) return;

            Destroy(Item3Render);

            DataPlayer.Add(elementdata);
            DEL_SLOT3.gameObject.SetActive(false);
            Slot3.gameObject.transform.SetAsLastSibling();
            //     Slot3.gameObject.SetActive(true);

            UI_Home.Instance.m_UIMerge.LoadEventory(Eventory.gameObject);
            Slot3 = null;
        }
    }
}
