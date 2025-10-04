using System;
using System.Collections;
using ArpaSubmodules.ArpaCommon.General.Extentions.Tween;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogService : MonoBehaviour
{
    [SerializeField] private Text _textTitle;
    [SerializeField] private Text _text;
    [SerializeField] private Image _bg;
    [SerializeField] private Canvas _canvas;

    private Action _onHide;
    private Coroutine _coroutine;
    private Tween _bgAlphaTween;
    private Tween _titleTween;

    private string _textText;
    private bool _isShowing;
    private bool _isShowed;

    private void Awake()
    {
        _canvas.gameObject.SetActive(false);
    }
    
    public void Show(string text, string title, Action onHide = null)
    {
        _canvas.gameObject.SetActive(true);
        _textText = text;
        _text.text = "";
        _textTitle.text = title;
        _onHide = onHide;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (string.IsNullOrEmpty(text))
        {
            _bg.SetAlpha(0);
            _text.text = "";
        }
        else
        { 
            _coroutine = StartCoroutine(Animation(text));
        }
    }

    private IEnumerator Animation(string text)
    {
        _isShowing = true;
        _bgAlphaTween?.Kill();
        _titleTween?.Kill();
        _bg.SetAlpha(0);
        _textTitle.SetAlpha(0);
        _bgAlphaTween = _bg.SetAlpha(.5f, 0.25f);
        _titleTween = _textTitle.SetAlpha(1, 0.25f);
        yield return new WaitForSeconds(0.1f);
        string outText = "";
        for (var i = 0; i < text.Length; i++)
        {
            outText += text[i];
            _text.text = outText;
            yield return new WaitForSeconds(0.01f);
            yield return null;
        }

        _isShowing = false;
        _isShowed = true;
    }

    private void Update()
    {
        if (_isShowing && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) ||
                           Input.GetKeyDown(KeyCode.Space)))
        {
            StopCoroutine(_coroutine);
            _text.text = _textText;
            _isShowing = false;
            _isShowed = true;
        }
        
        
        if (_isShowed && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E) ||
                          Input.GetKeyDown(KeyCode.Space)))
        {
            StartCoroutine(Hide());
            _isShowed = false;
        }
    }

    public IEnumerator Hide()
    {
        _bgAlphaTween.Kill();
        _titleTween.Kill();
        _bg.SetAlpha(0.25f);
        _bgAlphaTween = _bg.SetAlpha(0, .25f);
        _titleTween = _textTitle.SetAlpha(0, .25f);
        _text.text = "";
        yield return new WaitForSeconds(.25f);
        _canvas.gameObject.SetActive(false);
        _onHide?.Invoke();
    }
}
