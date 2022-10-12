using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBuffUI : MonoBehaviour
{
    [SerializeField]Image m_TimerImage;
    [SerializeField] Image m_BuffImage;
    [SerializeField] Sprite[] m_BuffSprites;
    BubbleBuff.BUFFTYPE m_CurrentBuff;
    SuckGemFromUI m_SuckGemScript;

    private void Start()
    {
        m_SuckGemScript = GetComponent<SuckGemFromUI>();
    }
    public void SetAvailableBuff(BubbleBuff.BUFFTYPE _buffType)
    {
        m_CurrentBuff = _buffType;
        switch (_buffType)
        {
            case BubbleBuff.BUFFTYPE.RANDOM:
                {

                    m_BuffImage.sprite = m_BuffSprites[0];
                    break;
                }
            case BubbleBuff.BUFFTYPE.FLARE:
                {
                    m_BuffImage.sprite = m_BuffSprites[1];
                    break;
                }
            case BubbleBuff.BUFFTYPE.GEMCHEST:
                {
                    m_BuffImage.sprite = m_BuffSprites[2];
                    break;
                }
            case BubbleBuff.BUFFTYPE.MAGNET:
                {
                    m_BuffImage.sprite = m_BuffSprites[3];
                    break;
                }
            case BubbleBuff.BUFFTYPE.SHIELD:
                {
                    m_BuffImage.sprite = m_BuffSprites[4];
                    break;
                }
            default:
                {
                    m_BuffImage.sprite = m_BuffSprites[5];
                    break;
                }
        }
    }

    public void SetTimerImageFill(float _fillAmount)
    {
        m_TimerImage.fillAmount = _fillAmount;

    }
    public IEnumerator SpawnGemChestGemsRoutine()
    {
        for(int i = 0; i < 10; i++)
        {
            var screenToWorldPosition = Camera.main.ScreenToWorldPoint(m_TimerImage.transform.position);
            m_SuckGemScript.MakeGem(screenToWorldPosition);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
