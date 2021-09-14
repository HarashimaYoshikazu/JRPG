using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWindow : MonoBehaviour
{
    [SerializeField] Transform arrow = default;
    [SerializeField] List<SelectableText> selectableTexts = new List<SelectableText>();

    private void Start()
    {
        SetMoveArrowFunction();
    }
    void SetMoveArrowFunction()
    {
        foreach (SelectableText selectableText in selectableTexts)
        {
            selectableText.OnSellectAction = MoveArrow2;
        }
    }

    public void MoveArrow2(Transform parent)
    {
        Debug.Log("カーソル移動");
        arrow.SetParent(parent);
    }
    private void Update()
    {
        //SetMoveArrowFunction();
    }
}
