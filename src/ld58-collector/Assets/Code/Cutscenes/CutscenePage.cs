using System;
using System.Collections;
using System.Collections.Generic;
using ArpaSubmodules.ArpaCommon.General.Extentions.Tween;
using DG.Tweening;
using UnityEngine;

public class CutscenePage : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _images;

    private int _index;
    private Action _onComplete;
    private Tween _tween;
    private Coroutine _yield;
    private Coroutine _ready;

    private bool _isShowing;
    private bool _isReady;
    
    public void Play(Action onComplete)
    {
        gameObject.SetActive(true);
        foreach (var spr in _images)
            spr.SetAlpha(0);
        _onComplete = onComplete;
        _yield = StartCoroutine(Yield());
    }

    private IEnumerator Yield()
    {
        _isShowing = true;
        yield return new WaitForSeconds(.25f);
        foreach (var spr in _images)
        {
            _tween?.Kill();
            _tween = spr.SetAlpha(1,1);
            yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(0.25f);
        }

        _isShowing = false;
        _ready = StartCoroutine(Ready());
    }

    public IEnumerator Ready()
    {
        _isReady = true;
        yield return new WaitForSeconds(7);
        Complete();
    }

    private void Complete()
    {
        _isReady = false;
        _onComplete?.Invoke();
    }
    
    private void Skip()
    {
        _tween?.Kill();
        if (_isShowing)
        {
            foreach (var spr in _images)
                spr.SetAlpha(1);
            if (_yield!=null)
                StopCoroutine(_yield);
            _isShowing = false;
            _ready = StartCoroutine(Ready());
            return;
        }
        
        if (_isReady)
        {
            if (_ready!=null)
                StopCoroutine(_ready);
            _onComplete?.Invoke();
            return;
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
            Skip();
    }
}
