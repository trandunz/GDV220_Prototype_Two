using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPopup : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshPro m_Text;
    [SerializeField] float m_PopUpSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLifetime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LerpColor()
    {
        Color startColor = m_Text.color;
        startColor.a = 0;
        while (m_Text.color.a < 1)
        {
            startColor.a += Time.deltaTime * 2;
            m_Text.color = startColor;
            yield return new WaitForEndOfFrame();
        }
        startColor.a = 1;
    }

    IEnumerator StartLifetime()
    {
        StartCoroutine((LerpColor()));
        m_Text.transform.localScale = Vector3.zero;
        Vector3 scale = m_Text.transform.localScale;
        while (m_Text.transform.localScale.x < 1)
        {
            scale.x += Time.deltaTime * m_PopUpSpeed;
            scale.y += Time.deltaTime * m_PopUpSpeed;
            m_Text.transform.localScale = scale;
            yield return new WaitForEndOfFrame();
        }
        m_Text.transform.localScale = Vector3.one;
        while (m_Text.transform.localScale.x > 0)
        {
            scale.x -= Time.deltaTime * m_PopUpSpeed;
            scale.y -= Time.deltaTime * m_PopUpSpeed;
            m_Text.transform.localScale = scale;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
