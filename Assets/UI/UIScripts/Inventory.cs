using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Inventory : UIPopup
{
    // 1. enum 자유롭게 구성
    enum Inventories
    {
        Blocker, ScrollRect,Scrollbar,ContentPanel,
        CloseButton
    }

    public int ItemNum = 20;

    private void Start()
    {
        Init();
    }

    // 2. Popup UI 닫는 버튼에 OnClick_Close 바인드
    // 3. Item
    // 의 ItemPropertyType 참고해서 각자의 방식으로 ItemGroup subitem 만들어 볼 것
    // 4. 생성할 때, ItemGroup의 SetInfo에 ItemPropertyType 할당해서 정보 넘겨줄 것
    public override void Init()
    {
        base.Init(); 
        
        Bind<GameObject>(typeof(Inventories));
        GameObject closeButton = GetUIComponent<GameObject>((int)Inventories.CloseButton);
        closeButton.BindEvent(OnClick_Close);

        GameObject contentPanel = GetObject((int)Inventories.ContentPanel); 
        foreach(string type in Enum.GetNames(typeof(ItemPropertyType)))
        {
            ItemGroup _itemGroup = UIManager.UI.MakeSubItem<ItemGroup>(contentPanel.transform,"ItemGroup");
            _itemGroup.SetInfo(type);

        }
    }


    // 5. OnClick_Close: Popup 닫기
    public void OnClick_Close(PointerEventData data)
    {
        UIManager.UI.ClosePopupUI(this);
    }
}