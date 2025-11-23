using TMPro;
using UnityEngine;
using System.Collections;

public class StatisticUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalObjectTxt; // bisa dijadiin lewat SO, nyimpen semua statisticnya
    [SerializeField] private int totalObjects = 0;

    [Header("NPC SECTION")]
    [SerializeField] private RectTransform panel;
    [SerializeField] private TextMeshProUGUI npcValCountTxt;
    [SerializeField] private TextMeshProUGUI targetValCountTxt;
    [SerializeField] private TextMeshProUGUI iconTxt;
    [SerializeField] private int totalNPCs = 0;
    [SerializeField] private int totalTargetNPCs = 0;
    [SerializeField] private bool isExpanded = false;
    [SerializeField] private float animDuration = 0.3f;

    private float posYclosedPanel = -59.8921f;
    private float posYExpandedPanel = -158.5596f;
    private float heightClosedPanel = 57.7841f;
    private float heightExpandedPanel = 255.1193f;

    public int TotalObjects { get { return totalObjects; } set { totalObjects = value; } }

    private NPCManager m_NPCManager;

    private void Start()
    {
        m_NPCManager = NPCManager.Instance;
        m_NPCManager.m_StatisticUI = this;

        totalObjectTxt.text = ": 0";
        npcValCountTxt.text = ": 0";
        targetValCountTxt.text = ": 0";
    }

    public void UpdateNPCStatisticDisplay()
    {
        int totalObject, totalNPC, totalTargetNPC;
        m_NPCManager.NPCStatistic(out totalNPC, out totalTargetNPC);

        //totalObjects += totalObject;
        totalNPCs = totalNPC;
        totalTargetNPCs = totalTargetNPC;

        totalObjectTxt.text = ": " + totalObjects.ToString();
        npcValCountTxt.text = ": " + totalNPCs.ToString();
        targetValCountTxt.text = ": " + totalTargetNPCs.ToString();
    }

    public void ExpandUI()
    {
        isExpanded = !isExpanded;
        iconTxt.text = isExpanded ? "-" : "+";

        float targetPosY = isExpanded ? posYExpandedPanel : posYclosedPanel;
        float targetHeight = isExpanded ? heightExpandedPanel : heightClosedPanel;

        // stop coroutine lama biar ga bentrok
        StopAllCoroutines();
        StartCoroutine(AnimatePanel(targetPosY, targetHeight));
    }

    private IEnumerator AnimatePanel(float targetPosY, float targetHeight)
    {
        Vector2 startPos = panel.anchoredPosition;
        Vector2 startSize = panel.sizeDelta;

        Vector2 endPos = new Vector2(startPos.x, targetPosY);
        Vector2 endSize = new Vector2(startSize.x, targetHeight);

        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animDuration;

            // SmoothStep biar lebih halus daripada linear
            t = Mathf.SmoothStep(0f, 1f, t);

            panel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            panel.sizeDelta = Vector2.Lerp(startSize, endSize, t);

            yield return null;
        }

        // pastikan nilai akhir bener-bener sama
        panel.anchoredPosition = endPos;
        panel.sizeDelta = endSize;
    }
}
