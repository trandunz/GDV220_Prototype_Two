using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBuffUI : MonoBehaviour
{
    TMPro.TextMeshProUGUI m_TempText;
    [SerializeField]Image m_TimerImage;

    private void Start()
    {
        m_TempText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void SetAvailableBuff(BubbleBuff.BUFFTYPE _buffType)
    {
        switch (_buffType)
        {
            case BubbleBuff.BUFFTYPE.RANDOM:
                {
                    m_TempText.text = "Random";
                    break;
                }
            case BubbleBuff.BUFFTYPE.FLARE:
                {
                    m_TempText.text = "Flare";
                    break;
                }
            case BubbleBuff.BUFFTYPE.GEMCHEST:
                {
                    m_TempText.text = "GemChest";
                    break;
                }
            case BubbleBuff.BUFFTYPE.MAGNET:
                {
                    m_TempText.text = "Marget";
                    break;
                }
            case BubbleBuff.BUFFTYPE.SHIELD:
                {
                    m_TempText.text = "Shield";
                    break;
                }
            default:
                {
                    m_TempText.text = "Dash";
                    break;
                }
        }
    }

    public void SetTimerImageFill(float _fillAmount)
    {
        m_TimerImage.fillAmount = _fillAmount;
    }
}
