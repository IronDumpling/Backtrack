using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotRotateUI : MonoBehaviour
{

    [SerializeField] private float _rotateSpeed = 0.5f;

    private GameObject _circle;
    private GameObject _dot;

    private RectTransform _rectTrans;
    private float _radius;
    private float _angle;
    bool isBlowUp = false;

    private void Awake()
    {
        _circle = transform.Find("Circle")?.gameObject;
        _dot = transform.Find("Dot")?.gameObject;
        _rectTrans = _dot.GetComponent<RectTransform>();
        _radius = gameObject.GetComponent<RectTransform>().rect.width / 2;
    }

    private void Update()
    {
        if (isBlowUp) Rotate();
        
    }

    public void OnBlowUp()
    {
        isBlowUp = true;
        _angle = 0f;
    }

    public void OnShrinkDown()
    {
        isBlowUp = false;
        _angle = 0f;
        SetLocalPosition();
    }

    void Rotate()
    {
        SetLocalPosition();
        _angle += Time.deltaTime * Mathf.PI * _rotateSpeed;
    }

    void SetLocalPosition()
    {
        float x = Mathf.Cos(-_angle) * _radius;
        float y = Mathf.Sin(-_angle) * _radius;
        _rectTrans.localPosition = new Vector3(x, y, 0f);
    }
}
