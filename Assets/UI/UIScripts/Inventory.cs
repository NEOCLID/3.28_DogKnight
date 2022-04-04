using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    // 1. enum �����Ӱ� ����
    enum Inventories
    {
        Blocker, ScrollRect,Scrollbar,ContentPanel,
        CloseButton
    }

    public int ItemNum = 30;

    private void Start()
    {
        Init();
    }

    // 2. Popup UI �ݴ� ��ư�� OnClick_Close ���ε�
    // 3. Item
    // �� ItemPropertyType �����ؼ� ������ ������� ItemGroup subitem ����� �� ��
    // 4. ������ ��, ItemGroup�� SetInfo�� ItemPropertyType �Ҵ��ؼ� ���� �Ѱ��� ��
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(Inventories));
        GameObject closeButton = GetUIComponent<GameObject>((int)Inventories.CloseButton);
        closeButton.BindEvent(OnClick_Close);
        GameObject contentPanel = GetUIComponent<GameObject>((int)Inventories.ContentPanel);

        for (int i = 0; i < ItemNum; i++)
        {
            string name = "Item" + i;
            GameObject item = UIManager.UI.MakeSubItem<Item>(contentPanel.transform).gameObject;
            Item itemscript = item.GetOrAddComponent<Item>();
            itemscript.SetInfo(ItemProperty.GetItemProperty(name).PropertyType);
        }
    }

    // 5. OnClick_Close: Popup �ݱ�
    public void OnClick_Close(PointerEventData data)
    {
        UIManager.Instance().ClosePopupUI(this);
    }
}