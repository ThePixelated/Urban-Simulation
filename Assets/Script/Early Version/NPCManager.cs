using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    [Header("NPC Config")]
    [Range(0f, 4f)]
    [SerializeField] private float gameplaySpeed = 1f;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int countNPC = 0;
    [SerializeField] private int countTargetNPC = 0;
    public List<NPC> m_Objects = new List<NPC>();

    [Header("")]
    public StatisticUI m_StatisticUI;

    public int CountNPC { get { return countNPC; } set { countNPC = value; } }
    public int CountTargetNPC { get { return countTargetNPC; } set { countTargetNPC = value; } }

    private bool stableState = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Debug.Log("A");
        foreach (var obj in m_Objects)
        {
            //Debug.Log("A1");
            //Debug.Log(obj.name);
            var npcData = obj.GetComponent<NPC>();
            if (npcData != null)
                npcData.SetUpdate(gameplaySpeed);
            //Debug.Log("A2");
        }

        //Debug.Log("B");
        if (Input.GetKeyDown(KeyCode.N) || stableState)
        {
            //Debug.Log("B1");
            Debug.Log("New NPC has been added! - via hotkey N");
            var instatiateObj = Instantiate(npcPrefab).GetComponent<NPC>();
            m_Objects.Add(instatiateObj);
            countNPC++;
            m_StatisticUI.TotalObjects++;
            m_StatisticUI.UpdateNPCStatisticDisplay();
            stableState = false;
        }
        //Debug.Log("C");
    }

    public void AddNewNPC()
    {
        stableState = true;
        //Debug.Log("New NPC has been added! - - via Add button");
        //var instatiateObj = Instantiate(npcPrefab).GetComponent<NPC>();
        //m_Objects.Add(instatiateObj);
        //countNPC++;
        //m_StatisticUI.TotalObjects++;
        //m_StatisticUI.UpdateNPCStatisticDisplay();
        //stableState = true;
    }

    public void NPCStatistic(out int totalNPC, out int totalTargetNPC)
    {
        totalNPC = CountNPC;
        totalTargetNPC = CountTargetNPC;
    }

    //private void OnDrawGizmos() // karna local transform, jadi ngaco
    //{
    //    var m_CanvasTransform = GameObject.Find("Canvas");
    //    Gizmos.matrix = m_CanvasTransform.transform.localToWorldMatrix;

    //    Vector3 npc, targetNPC;

    //    foreach (var obj in m_Objects)
    //    {
    //        var npcData = obj.GetComponent<NPC>();
    //        npcData.GizmoData(out npc, out targetNPC);

    //        Gizmos.color = Color.red;
    //        if (targetNPC != null)
    //            Gizmos.DrawLine(npc, targetNPC);
    //    }
    //}
}
