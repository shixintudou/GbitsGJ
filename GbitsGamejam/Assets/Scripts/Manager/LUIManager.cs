using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LUIManager : MonoBehaviour
{
    GameObject currentTip;
    public void ShowTip(string str)
    {
        ShowTipUI_Internal(str, "TipUI");

    }
    public void ShowLongTip(string str)
    {
        ShowTipUI_Internal(str, "LongTipUI");
    }

    private void ShowTipUI_Internal(string str, string prefabName)
    {
     //   print("��ʾ��ʾ : " + str);
        if (currentTip == null)
        {
            currentTip = Instantiate(ResoucesManager.Instance.Resouces[prefabName]);
            if (currentTip != null)
            {
                currentTip.GetComponentInChildren<TextMeshProUGUI>().text = str;
            }
        }
        else
        {
            AutoDestroy autoDestroyComp = currentTip.GetComponent<AutoDestroy>();
            if (autoDestroyComp != null)
            {
                //ע����ʵ����animator��text������������
                Animator animator = autoDestroyComp.GetComponentInChildren<Animator>();
                if (animator != null)
                {
                    AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
                    // ����ǰ�������ص���ʼ״̬
                    animator.Play(currentState.fullPathHash, -1, 0f);
                }
                autoDestroyComp.ResetLifeSpan();
            }
            else
                print("warning:tip����δ����AutoDestroy�ű�");
            currentTip.GetComponentInChildren<TextMeshProUGUI>().text = str;
        }
    }
    public void CanvasFadeInAndOut()
    {
        Instantiate(ResoucesManager.Instance.Resouces["FadeCanvas"]);
    }
}

