using UnityEngine;

public abstract class NPC_Base
{
    public abstract void Enter(NPC npcData);
    public abstract void DoUpdate(NPC npcData);
    public abstract void Exit(NPC npcData);
}
