using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XReal.XTown.UI;

public class Item : UIBase
{
    // 1. enum �����Ӱ� ����
    enum Images
    {
        Image
    }

    enum Texts
    {
        Text
    }

    private string _itemName;
    private string _name;
    private int _color;

    private void Start()
    {
        Init();
    }

    // 2. Item Button�� OnClick_ItemUse Bind
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.Text).text = _name;
        GetImage((int)Images.Image).color = new Color(0, _color / 255f, 0);
        GetImage((int)Images.Image).gameObject.BindEvent(OnClick_ItemUse);

    }

    /// <summary>
    /// 3. OnClick_ItemUse
    /// 1) 
    /// .GetItemProperty�� _itemName �̿��ؼ� ItemProperty ����
    /// 2) ���� �ش� ������ ������ 0���� ũ�ٸ�
    /// 3) ���� -1 & ��ü �ı�
    /// 4) ItemAction();
    /// </summary>
    public void OnClick_ItemUse(PointerEventData data)
    {
        ItemProperty itemProperty = ItemProperty.GetItemProperty(_itemName); //�̰� �ϼ��� �ڵ������� �𸣰ھ�
        //�ƴ� ���ʿ� _itemName�� �갡 �μ��� ���� �� �ֱ�� ��? data�� �װ� �˷���?

        

        if (itemProperty.ItemNumber > 0)
        {
            itemProperty.ItemNumber--;
            Destroy(this);
            ItemAction(); //�̰� ���ֹ����� �� �³�?
            
        }
    }

    /// <summary>
    /// 4. ItemAction:
    /// 1) switch ������ itemProperty.PropertyType �μ��� �ް� //�̰� ��� switch�� ����?
    /// 2) ItemProperty.GetItemProperty�� _itemName �̿��ؼ� ItemProperty �����ؼ�
    /// 3) Damage���, GameManager.Instance().GetCharacter("Player")�� �÷��̾� �����ؼ� ������ �߰�
    /// //�ƴ� ���ʿ� ItemList���� ������ �ִµ�?
    /// 4) Heal�̶�� �����ϰ� �����ؼ� ü�� �߰� + SceneUI�� CharacterHP() ȣ��
    /// </summary>
    public void ItemAction()
    {

        switch (_itemName)
        {
            case "FlameItem":
                GameManager.Instance().GetCharacter("Player")._myDamage = 30;  //�̷��� ������ �ö� ������ �� �ƴϳ�?    
                
                break;


            case "FireSpearItem":

                break;

            case "Heal":
                GameManager.Instance().GetCharacter("Player")._myHp += 10;
                //SceneUI.CharacterHp(); //�̰� ���� ���� �����ε�.. ��..
                break;
            default:
                break;
        }
     }

    // 5. SetInfo: itemName�� _itemName�� �Ҵ�
    public void SetInfo(string itemName)
    {
        _itemName = itemName;
    }
}