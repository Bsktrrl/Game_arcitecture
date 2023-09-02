using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropManager : MonoBehaviour
{
    [SerializeField] GameObject raindrop_Prefab;
    [SerializeField] GameObject raindrop_Parent;

    [SerializeField] List<GameObject> raindrop_List;

    private void Start()
    {
        InstantitateRaindrops();
    }

    void InstantitateRaindrops()
    {

        raindrop_List.Add(Instantiate(raindrop_Prefab) as GameObject);
        raindrop_List[raindrop_List.Count - 1].transform.SetParent(raindrop_Parent.transform);
    }
}
