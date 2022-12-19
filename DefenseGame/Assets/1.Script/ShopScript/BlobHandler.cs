using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlobHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region 변수
    [SerializeField]
    int cost;

    [SerializeField]
    ExplainController explain;
    [Header("설명서")]
    [SerializeField]
    ExplainContent param;
    [SerializeField]
    PlayerOperate operate;
    #endregion

    #region 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        BuilderController.instance.playerOP = operate;
        BuilderController.instance.cost = cost;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BuilderController.instance.canBuild = false;
        explain.gameObject.SetActive(true);
        explain.SetText(param);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BuilderController.instance.canBuild = true;
        explain.gameObject.SetActive(false);
    }
    #endregion
}
