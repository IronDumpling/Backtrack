using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxPlay : MonoBehaviour
{
    public GameObject vfx;
    public GameObject[] vfxWindAndSmoke;
    private int vfxCount;
    // Start is called before the first frame update
    void Start()
    {
        vfxCount= vfxWindAndSmoke.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            vfx.SetActive(true);
            ParticleSystem effect1ParticleSystem = vfx.GetComponent<ParticleSystem>();
            if (effect1ParticleSystem != null)
            {
                StartCoroutine(PlayEffect2AfterEffect1(effect1ParticleSystem.main.duration));
            }
        }
    }
    private System.Collections.IEnumerator PlayEffect2AfterEffect1(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject vfx2 in vfxWindAndSmoke)
        {
            vfx2.SetActive(true);
        }
        yield return null;


           
        
    }
}
