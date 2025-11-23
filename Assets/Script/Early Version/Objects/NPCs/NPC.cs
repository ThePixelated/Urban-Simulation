using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NPC : Objects
{
    [SerializeField] private string name;

    [Header("NPC's Config")]
    public GameObject prefabTarget;
    public float speed = 200f;
    public Status npcStatus = Status.IDLE;
    public NPC_Idle NPC_Idle = new NPC_Idle();
    public NPC_Move NPC_Movement = new NPC_Move();
    public NPC_Base _currentState;

    [Header("NPC's UI")]
    [SerializeField] private TextMeshProUGUI npcName; // ganti ke GameObject
    [SerializeField] private Transform textAnchor;

    private GameObject prefabTG;
    private TargetNPC m_TargetNPC;
    private List<string> listNames = new List<string>() { "Akmal", "Sucpito", "Bob", "Andre", "Umang", "Asep" };

    /// <summary>
    /// PARENT OBJECT & UI
    /// </summary>
    private GameObject UI_NPC_Target;
    private GameObject UI_NPC;
    private GameObject OBJ_NPC_Target;
    private GameObject OBJ_NPC;
    public override string Name { get { return name; } set { name = value; } }
    public override ObjectTypes ObjectType { get { return ObjectType; } set { ObjectType = value; } }
    public override void SetUpdate(float inGameSpeedMultiplier)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchState(NPC_Idle);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchState(NPC_Movement);
        }

        _currentState.DoUpdate(this);

        npcName.transform.position = textAnchor.transform.position;
        if (m_TargetNPC != null)
            m_TargetNPC.UpdateTextPos();
    }

    private void Awake()
    {
        UI_NPC_Target = GameObject.Find("UI - NPC Target");
        UI_NPC = GameObject.Find("UI - NPC's");
        OBJ_NPC_Target = GameObject.Find("Obj - NPC Target");
        OBJ_NPC = GameObject.Find("Obj - NPC's");
    }

    private void Start()
    {
        float _margin = 100f;
        name = listNames[Random.Range(0, listNames.Count)];
        gameObject.transform.position = new Vector3(Random.Range(_margin, (Screen.height - _margin)), Random.Range(_margin, Screen.height - _margin));

        var objectsName = name;
        var parent = OBJ_NPC;

        gameObject.transform.SetParent(parent.transform);
        gameObject.name = "NPC - " + objectsName;

        npcName.text = objectsName;
        npcName.gameObject.name = "NPC " + objectsName + " Txt";

        var parentLegend = UI_NPC;
        npcName.transform.SetParent(parentLegend.transform);

        _currentState = NPC_Idle;
        _currentState.Enter(this);
    }

    public void SwitchState(NPC_Base newState)
    {
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void InstantiateTarget(float _margin = 100f)
    {
        var parent = OBJ_NPC_Target;

        prefabTG = Instantiate(prefabTarget, parent.transform);
        prefabTG.transform.position = new Vector3(Random.Range(_margin, (Screen.height - _margin)), Random.Range(_margin, Screen.height - _margin));
        m_TargetNPC = prefabTG.GetComponent<TargetNPC>();
        m_TargetNPC.m_NPC = this;

        prefabTG.gameObject.name = "Target - " + name + "'s";
        
        var parentLegend = UI_NPC_Target;
        m_TargetNPC.SetParentObj(parentLegend.transform);
    }

    public void DestroyTarget()
    {
        m_TargetNPC.SetParentObj(prefabTG.transform);
        m_TargetNPC.DestroyObj();
    }

    public Vector3 TargetTransform()
    {
        return prefabTG.transform.position;
    }

    public void GizmoData(out Vector3 npc, out Vector3 targetNPC)
    {
        npc = gameObject.transform.position;
        if (prefabTG != null)
            targetNPC = prefabTG.transform.position;
        else
            targetNPC = gameObject.transform.position;
    }
}

public enum Status
{
    IDLE,
    MOVE
}