using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Header("Tiles Prefabs")]
    public GameObject FloorPrefab;
    public GameObject ObstaclePrefab;
    public GameObject StartPrefab;
    public GameObject EndPrefab;


    // Start is called before the first frame update
    void Start()
    {
        CreateBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateBoard(int size = 10){
        for(int i = 0; i < size; ++i){

            for(int j = 0; j < size; ++j){
                Instantiate(FloorPrefab, new Vector3(i, j, 0), Quaternion.identity);
            }
        }
    }

}
