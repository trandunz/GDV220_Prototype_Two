using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBuffUI : MonoBehaviour
{
    public Image m_TimerImage;
    [SerializeField] Image m_BuffImage;
    [SerializeField] Sprite[] m_BuffSprites;
    BubbleBuff.BUFFTYPE m_CurrentBuff;
    SuckGemFromUI m_SuckGemScript;

    // Flash Unused Buff
    public bool m_UnusedBuff;
    float m_AlphaTimer;
    float m_ScaleTimer;
    public float m_ChangeTime;
    public Image m_BorderGlow;

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

    private void FlashBuff()
    {
        // Scale the buff icon
        m_ScaleTimer += 0.02f * Time.deltaTime;
        Vector3 scale = m_BuffImage.rectTransform.localScale;
        scale += new Vector3(m_ScaleTimer, m_ScaleTimer, m_ScaleTimer);

        if (m_ScaleTimer > 0.01f)
        {
            scale = new Vector3(1.0f, 1.0f, 1.0f);
            m_ScaleTimer = 0.0f;
        }
        m_BuffImage.rectTransform.localScale = scale;

        // Change alpha of the border glow
        m_AlphaTimer = m_ScaleTimer * 100.0f;

        if (m_AlphaTimer >= 1.0f)
        {
            m_AlphaTimer = 0.0f;
        }
        m_BorderGlow.GetComponent<Image>().color = Color.Lerp(Color.white, Color.clear, m_AlphaTimer);
    }

    private void Update()
    {
        if (m_CurrentBuff != BubbleBuff.BUFFTYPE.UNASSIGNED &&
            m_UnusedBuff == true)
        {
            FlashBuff();
        }
        else
        {
            Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
            m_BuffImage.rectTransform.localScale = scale;
            m_BorderGlow.GetComponent<Image>().color = Color.clear;
        }
        Debug.Log(m_UnusedBuff);
    }

    public void SetBuffUsed(bool notUsed)
    {
        m_UnusedBuff = notUsed;
    }
}
