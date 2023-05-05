using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisslovingController : MonoBehaviour
{
    public MeshRenderer skinnedMesh;

    [SerializeField]
    private Material skinnedMaterials;

    public float dissolveRate = 0.0125f; 
    
    public float refreshRate = 0.025f;




    void Start()
    {
        skinnedMesh = GetComponent<MeshRenderer>();
        if (skinnedMesh != null)
        
            skinnedMaterials = skinnedMesh.material;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     StartCoroutine(DissolveCo());
        // }
    }

    public void DissolveGameObject()
    {
        StartCoroutine(DissolveCo());
    }

    IEnumerator DissolveCo()
    {
        
            float counter = 0;

            while (skinnedMaterials.GetFloat("_DissloveAmount") < 1)
            {
                
                counter += dissolveRate;
                skinnedMaterials.SetFloat("_DissloveAmount", counter);

                yield return new WaitForSeconds(refreshRate);

            }
        
    }













}
