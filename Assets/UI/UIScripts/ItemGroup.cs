using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;
using System;

public class ItemGroup : UIBase
{
    // 1. enum 자유롭게 구성

    private string _itemGroupName;

    private void Start()
    {
        Init();
    }

    // 2. 텍스트 UI에 _itemGroupName으로 update
    // 3. 현재 할당된 타입에 해당하는 아이템들을(Item) subtiem으로 생성할 것
    // 4. 생성할 때, Item의 SetInfo에 _itemName 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        Text itemText = UIUtils.FindUIChild<Text>(gameObject, "ItemTypeText", true);
        itemText.text = _itemGroupName;
        Transform itemPanel = UIUtils.FindUIChild<Transform>(gameObject, "ItemPanel", true);
        foreach (ItemProperty i in ItemProperty.ItemProperties)
        {
            if (i.PropertyType == _itemGroupName)
            {
                for (int j = 0; j < i.ItemNumber; j++)
                {
                    Item item = UIManager.UI.MakeSubItem<Item>(itemPanel, i.ItemName);
                    item.SetInfo(i.ItemName);
                }
            }

        }
    }
    // 5. SetInfo: itemtype을 _itemGroupName에 할당
    public void SetInfo(string itemtype)
    {
        _itemGroupName = itemtype;
    }
}