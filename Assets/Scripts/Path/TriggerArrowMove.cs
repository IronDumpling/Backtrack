using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerArrowMove : MonoBehaviour
{
    [SerializeField]  private Transform[] firstTrack;
    [SerializeField] private Transform[] secondTrack;
    [SerializeField] private Transform[] thirdTrack;
    
    private Vector3[] firstPointPos;
    private Vector3[] secondPointPos;
    private Vector3[] thirdPointPos;

    [SerializeField] private float firstTrackDuration;
    [SerializeField] private float secondTrackDuration;
    [SerializeField] private float thirdTrackDuration;
    [SerializeField] private float restTrackDuration;

    [SerializeField] private Transform moveObjTransform;
    private AudioSource _audioSource;
    //public AudioClip arrowMoveClip;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        firstPointPos = new Vector3[firstTrack.Length];
        secondPointPos = new Vector3[secondTrack.Length];
        thirdPointPos = new Vector3[thirdTrack.Length];
        
        
        moveObjTransform.position = firstTrack[0].position;
        for (var i = 0; i < firstTrack.Length ; i++)
        {
            firstPointPos[i] = firstTrack[i].localPosition;
        }
        for (var i = 0; i < secondTrack.Length ; i++)
        {
            secondPointPos[i] = secondTrack[i].localPosition;
        }
        for (var i = 0; i < thirdTrack.Length ; i++)
        {
            thirdPointPos[i] = thirdTrack[i].localPosition;
        }

        
        startArrow();
                
        
    }

    private void startArrow()
    {
        moveObjTransform.DOLocalPath(firstPointPos, firstTrackDuration)
            .onComplete += () =>
        {
            StartCoroutine(arrowRest());
        };

    }

    IEnumerator arrowRest()
    {
        
        yield return new WaitForSeconds(restTrackDuration);
        
        
        moveObjTransform.DOLocalPath(secondPointPos, secondTrackDuration)
            .onComplete += () =>
        {
            moveObjTransform.DOLocalPath(thirdPointPos, thirdTrackDuration).SetEase(Ease.InQuad)
                .onComplete += () =>
            {
                
                transform.parent.gameObject.SetActive(false);
            };
        };
        //yield return new WaitForSeconds(0.75f);
        //_audioSource.PlayOneShot(arrowMoveClip);

    }
}
