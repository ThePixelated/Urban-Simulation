using TMPro;
using UnityEngine;

public class TargetNPC : MonoBehaviour
{
    public NPC m_NPC;
    [SerializeField] private TextMeshProUGUI npcTargetName;
    [SerializeField] private Transform textAnchor;

    private void Start()
    {
        npcTargetName.text = m_NPC.Name + "'s Target";
        npcTargetName.gameObject.name = "Target " + m_NPC.Name + "'s Txt";
    }

    public void UpdateTextPos()
    {
        npcTargetName.transform.position = textAnchor.transform.position;
    }

    public void SetParentObj(Transform parent)
    {
        npcTargetName.transform.SetParent(parent);
    }

    public void DestroyObj()
    {
        //Destroy(npcTargetName.gameObject);
        Destroy(gameObject);
    }
}
