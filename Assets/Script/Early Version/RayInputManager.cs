using Unity.VisualScripting;
using UnityEngine;

public class RayInputManager : MonoBehaviour
{

    private void Update()
    {
        //if (true) // tag object sesuai & layernya juga sesuai
        //{

        //}

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        // Check if the ray hit something
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
            //Debug.DrawRay();
        }
    }
}

