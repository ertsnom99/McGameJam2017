using System.Collections;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public const string INTERACTIF_AREA = "InteractifArea";
    public const string WALKABLE_AREA = "WalkableArea";

    private float totalWalkableArea;

    private float[] individualWalkableAreas;

    private float interactifChoiceChance;

    public BoxCollider[] walkableAreas;
    public BoxCollider[] interactifAreas;

    private void Awake()
    {
        InitializeVariables();
        InitializeAreas();
    }

    private void InitializeVariables()
    {
        interactifChoiceChance = 20.0f;
    }

    private void InitializeAreas()
    {
        individualWalkableAreas = new float[walkableAreas.Length];

        int i = 0;

        foreach (BoxCollider walkableArea in walkableAreas)
        {
            float area = walkableArea.size.x * walkableArea.size.z;
            individualWalkableAreas[i] = area;
            totalWalkableArea += area;
            i++;
        }
    }

    public Vector3 GenerateSpawnPoint()
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
    
    public ArrayList GenerateDestination()
    {
        ArrayList positionInfo = new ArrayList();

        Vector3 position = Vector3.zero;
        string areaType = "";

        float randomType = Random.Range(0.0f, 100.0f);

        /*if (randomType > interactifChoiceChance)
        {
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

            areaType = WALKABLE_AREA;
        }
        else
        {*/
            int selectedInteractifArea = (int)Mathf.Round(Random.Range(0, 6));
            int i = 0;

            position = interactifAreas[selectedInteractifArea].gameObject.transform.position + new Vector3(Random.Range(0.0f, interactifAreas[selectedInteractifArea].size.x), 0, Random.Range(0.0f, -interactifAreas[selectedInteractifArea].size.z));
            areaType = INTERACTIF_AREA;
        //}

        positionInfo.Add(position);
        positionInfo.Add(areaType);

        return positionInfo;
    }
}
