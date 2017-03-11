using System.Collections;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    private float totalWalkableArea;

    private float[] individualWalkableAreas;

    private BoxCollider[] walkableAreas;

    private void Awake()
    {
        InitializeAreas();
    }

    private void InitializeAreas()
    {
        ArrayList tempWalkableAreas = new ArrayList();

        int i = 0;

        foreach (Transform child in transform)
        {
            tempWalkableAreas.Add(child.GetComponent<BoxCollider>());
            i++;
        }

        walkableAreas = (BoxCollider[])tempWalkableAreas.ToArray(typeof(BoxCollider));
        
        individualWalkableAreas = new float[walkableAreas.Length];

        i = 0;

        foreach (BoxCollider walkableArea in walkableAreas)
        {
            float area = walkableArea.size.x * walkableArea.size.z;
            individualWalkableAreas[i] = area;
            totalWalkableArea += area;
            i++;
        }
    }
    
    public Vector3 GeneratePosition()
    {
        Vector3 position = Vector3.zero;
        float selectedPositionInTotalArea = Random.Range(0.0f, totalWalkableArea);
        int i = 0;

        foreach (BoxCollider walkableArea in walkableAreas)
        {
            if (selectedPositionInTotalArea > walkableArea.size.x * walkableArea.size.z)
            {

                selectedPositionInTotalArea -= walkableArea.size.x * walkableArea.size.z;
                i++;
            }
            else
            {
                position = walkableAreas[i].gameObject.transform.position + new Vector3(Random.Range(0.0f, walkableAreas[i].size.x), 0, Random.Range(0.0f, -walkableAreas[i].size.z));
            }
        }

        return position;
    }
}
