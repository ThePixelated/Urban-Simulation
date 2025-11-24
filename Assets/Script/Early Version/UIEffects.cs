using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    public static UIEffects Instance;

    [SerializeField] private float animDuration = 0.3f;
    [SerializeField] private List<AnimationList> m_AnimationList = new List<AnimationList>();

    private RectTransform m_RectTransform;
    private bool _pingpongState = false;

    private void Awake()
    {
        Instance = this;

        m_RectTransform = GetComponent<RectTransform>();
    }

    public void SmoothStepEffect(int indexAnimation)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothStep(indexAnimation));
    }

    private IEnumerator SmoothStep(int indexAnimation)
    {
        if (m_AnimationList[indexAnimation].PingPong)
            _pingpongState = !_pingpongState;

        Vector3 startPos = _pingpongState ? m_AnimationList[indexAnimation].targetPos : m_AnimationList[indexAnimation].startingPos;
        Vector3 startSize = _pingpongState ? m_AnimationList[indexAnimation].targetSize : m_AnimationList[indexAnimation].startingSize;
        Quaternion startRotation = _pingpongState ? m_AnimationList[indexAnimation].targetRotation : m_AnimationList[indexAnimation].startingRotation; // bikin conditional state ? target : start
        Vector3 startScale = _pingpongState ? m_AnimationList[indexAnimation].targetScale : m_AnimationList[indexAnimation].startingScale;

        Vector3 endPos = _pingpongState ? m_AnimationList[indexAnimation].startingPos : m_AnimationList[indexAnimation].targetPos;
        Vector3 endSize = _pingpongState ? m_AnimationList[indexAnimation].startingSize : m_AnimationList[indexAnimation].targetSize;
        Quaternion endRotation = _pingpongState ? m_AnimationList[indexAnimation].startingRotation : m_AnimationList[indexAnimation].targetRotation;    // bikin conditional state ? start : target
        Vector3 endScale = _pingpongState ? m_AnimationList[indexAnimation].startingScale : m_AnimationList[indexAnimation].targetScale;

        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animDuration;

            // SmoothStep biar lebih halus daripada linear
            t = Mathf.SmoothStep(0f, 1f, t);

            m_RectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            m_RectTransform.sizeDelta = Vector3.Lerp(startSize, endSize, t);
            //m_RectTransform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            m_RectTransform.localScale = Vector3.Lerp(startScale, endScale, t);

            yield return null;
        }

        // pastikan nilai akhir bener-bener sama
        m_RectTransform.anchoredPosition = endPos;
        m_RectTransform.sizeDelta = endSize;
        m_RectTransform.rotation = endRotation;
        m_RectTransform.localScale = endScale;

        if (m_AnimationList[indexAnimation].Loop)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothStep(indexAnimation));
        }
    }
}

[System.Serializable]
public class AnimationList
{
    public string Title = "[Title]_Effect";
    public bool Loop = false;
    public bool PingPong = false;
    [Header("Start Condition")]
    public Vector3 startingPos;
    public Vector2 startingSize;
    public Quaternion startingRotation;
    public Vector3 startingScale = new Vector3(1, 1, 1);

    [Header("Target Condition")]
    public Vector3 targetPos;
    public Vector2 targetSize;
    public Quaternion targetRotation;
    public Vector3 targetScale = new Vector3(1, 1, 1);
}
