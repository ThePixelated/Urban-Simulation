using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(0f, 4f)]
    [SerializeField] private float gameplaySpeed = 1f;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private List<GameObject> m_Objects = new List<GameObject>();

    private void Update()
    {
        //Debug.Log("A");
        foreach (var obj in m_Objects)
        {
            //Debug.Log("A1");
            //Debug.Log(obj.name);
            var npcData = obj.GetComponent<NPC>();
            npcData.SetUpdate(gameplaySpeed);
            //Debug.Log("A2");
        }

        //Debug.Log("B");
        if (Input.GetKeyDown(KeyCode.N))
        {
            //Debug.Log("B1");
            m_Objects.Add(Instantiate(npcPrefab));
        }
        //Debug.Log("C");
    }

    //private void OnDrawGizmos()
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
