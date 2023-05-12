using System.Collections;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;


    public class TriggerMovableObj : TriggerBase
    {
        [Header("放入点，生成曲线，让玩家以曲线模式移动，当前玩家位置为起点位置，最后一个点为终点位置")] [SerializeField]
        private Transform[] points;
        [SerializeField] private Transform moveObjTransform;
        private Vector3[] pointPos;
        [SerializeField] private float duration;
        [SerializeField] private PathType type = PathType.CatmullRom;
        [SerializeField] private Ease easeType = DOTween.defaultEaseType;

        [SerializeField] private bool AppearAfterTrigger;
        [SerializeField] private bool isMoveOnStart = false;
        [SerializeField] private bool isLocalMove = false;
        [Header("只在isLocalMove的时候,false才有效")]
        [SerializeField] private bool isLookAtPath = true;

        [SerializeField] private bool isStopAtMid = false;
        [SerializeField] private float beforePauseTime = 1f;
        [SerializeField] private float pauseTime = 5f;

        Tween tw;
        private void Awake()
        {
            if(AppearAfterTrigger) moveObjTransform.gameObject.SetActive(false);
            if(isMoveOnStart) this.GetComponent<Collider>().enabled = false;
        }
        private void Start()
        {
            if (points.Length == 0) Debug.LogWarning(transform.name + " 未加入点，生成不了曲线");
            pointPos = new Vector3[points.Length];
            isOneTime = true;
            moveObjTransform.position = points[0].position;
            for (var i = 0; i < points.Length ; i++)
            {
                pointPos[i] = points[i].localPosition;
            }
            
            //pointPos = pointPos.SkipLast(1).ToArray();
            if (isMoveOnStart)
            {
                enterEvent();
                
            }
        }

        protected override void enterEvent()
        {
            base.enterEvent();
            moveObjTransform.gameObject.SetActive(true);
            
            if (isLocalMove)
            {
                if (isLookAtPath)
                {
                    tw = moveObjTransform.DOLocalPath(pointPos, duration, type).SetEase(easeType);

                }
                else
                {
                    tw = moveObjTransform.DOLocalPath(pointPos, duration, type).SetLookAt(0.01f).SetEase(easeType);

                }
                    
        
            }
            else
            {
                tw = moveObjTransform.DOPath(pointPos, duration, type).SetLookAt(0.01f).SetEase(easeType);

            }
            
            //moveObjTransform.DOPath(pointPos,duration

            if (isStopAtMid)
            {
                StartCoroutine(StopCount());
            }

            tw.onComplete += () =>
            {
                this.transform.parent.gameObject.SetActive(false);
            };
            
            

        }
        IEnumerator StopCount()
        {
            yield return new WaitForSeconds(beforePauseTime);
            tw.timeScale = 0f;
            yield return new WaitForSeconds(pauseTime);
            tw.timeScale = 1f;

        }


    }
