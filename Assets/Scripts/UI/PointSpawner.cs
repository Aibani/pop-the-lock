using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    
    private float _offset = 0.25f;
    private float _radius = 57f;
    public RectTransform target;
    public float normarizedPosition;
    
    public void Spawn(float normarizedPosition)
    {
        this.normarizedPosition = normarizedPosition;
        target.localPosition = new Vector3(
            _radius * Mathf.Cos((normarizedPosition + _offset) * 2 * Mathf.PI),
            _radius * Mathf.Sin((normarizedPosition + _offset) * 2 * Mathf.PI),
            0
        );
        target.DOScale(1f, 0.4f).From(0.2f);
    }
 
}
