using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableText : Selectable
{
    //関数登録
    public UnityAction<Transform> OnSellectAction = null;
    //public  bool left = true;
    //public  bool up = true;
    //選択状態になった時に勝手にされる  

    private void Update()
    {

    }

    public override void OnSelect(BaseEventData eventData)
    {
        //Debug.Log($"{gameObject.name}が選択された");
        OnSellectAction.Invoke(transform);
    }

    //非選択状態になった時に勝手に選択される
    public override void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log($"{gameObject.name}が選択が外れた");
    }

    //public void CursorMove()
    //{

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        left = false;
    //        Debug.Log("→" + left + up);

    //    }
    //    else if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        left = true;
    //        Debug.Log("←" + left + up);

    //    }
    //    else if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        up = false;
    //        Debug.Log("↓" + left + up);

    //    }
    //    else if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        up = true;
    //        Debug.Log("↑" + left + up);

    //    }
    //}

}
