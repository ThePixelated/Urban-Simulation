using UnityEngine;

public class NPC_Move : NPC_Base
{
    NPC local_npcData;
    Vector3 _npcPos;
    Vector3 _endPosition;
    float _npcSpeed;

    public override void Enter(NPC npcData)
    {
        local_npcData = npcData;

        Debug.Log("Enter NPC MOVEMENT STATE");
        npcData.npcStatus = Status.MOVE;

        _npcSpeed = npcData.speed;
        _npcPos = npcData.transform.position;
        _endPosition = npcData.TargetTransform();

        npcData.prefabTarget.transform.position = _endPosition;

        Debug.Log("NPC Location: " + local_npcData.transform.position);
        Debug.Log("New Destination: " + _endPosition);
    }

    public override void DoUpdate(NPC npcData)
    {
        if (Vector3.Distance(_npcPos, _endPosition) >= 1f)
        {
            Debug.Log("Moving..." + Vector3.Distance(_npcPos, _endPosition));
            CalculateMovement();
        }
        else
        {
            Debug.Log("Destination reached...");
            npcData.DestroyTarget();
            npcData.SwitchState(npcData.NPC_Idle);
        }
    }

    public override void Exit(NPC npcData) 
    {
        Debug.Log("Exit NPC MOVEMENT STATE");
    }

    private void CalculateMovement()
    {
        if (Mathf.Abs(_endPosition.x - (_npcPos.x + _npcSpeed)) <= Mathf.Abs(_endPosition.x - (_npcPos.x - _npcSpeed)))
        {
            _npcPos.x = _npcPos.x + _npcSpeed * Time.deltaTime;
            Debug.Log("A (+) - Xcoor");
        }
        else 
        {
            _npcPos.x = _npcPos.x - _npcSpeed * Time.deltaTime;
            Debug.Log("B (-) - Xcoor");
        }

        if (Mathf.Abs(_endPosition.y - (_npcPos.y + _npcSpeed)) <= Mathf.Abs(_endPosition.y - (_npcPos.y - _npcSpeed)))
        {
            _npcPos.y = _npcPos.y + _npcSpeed * Time.deltaTime;
            Debug.Log("C (+) - Ycoor");
        }
        else
        {
            _npcPos.y = _npcPos.y - _npcSpeed * Time.deltaTime;
            Debug.Log("D (-) - Ycoor");
        }

        var marginSnap = 5f;
        if (Mathf.Abs(_npcPos.x - _endPosition.x) <= marginSnap)
        {
            _npcPos.x = _endPosition.x;
        }

        if (Mathf.Abs(_npcPos.y - _endPosition.y) <= marginSnap)
        {
            _npcPos.y = _endPosition.y;
        }

        local_npcData.transform.position = _npcPos;
        Debug.Log(local_npcData.transform.position);
    }
}
