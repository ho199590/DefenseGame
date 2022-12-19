using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ExplainContent
{
    public string title;
    public string cost;
    public string content;
}


public class ExplainController : MonoBehaviour
{
    #region ����
    [SerializeField]
    Text title, cost, content;

    #endregion

    #region �Լ�
    public void SetText(ExplainContent explan)
    {
        title.text = explan.title;
        cost.text = explan.cost;
        content.text = explan.content;
    }
    #endregion
}
