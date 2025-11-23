using UnityEngine;

public class NPC_Idle : NPC_Base
{
    float probability = 5f;

    public override void Enter(NPC npcData)
    {
        Debug.Log("Enter NPC IDLE STATE");
        npcData.npcStatus = Status.IDLE;
    }

    public override void DoUpdate(NPC npcData)
    {
        if (Random.Range(0, 101) < probability)
        {
            Debug.Log("Decided to move!");
            npcData.InstantiateTarget();
            npcData.SwitchState(npcData.NPC_Movement);
        }
    }

    public override void Exit(NPC npcData)
    {
        Debug.Log("Exit NPC IDLE STATE");
    }
}
