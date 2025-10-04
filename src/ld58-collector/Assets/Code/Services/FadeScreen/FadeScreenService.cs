using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenService : MonoBehaviour
{
    [SerializeField] private Image _img;
    [SerializeField] private GameObject _canvas;
    
    public IEnumerator FadeIn(float time)
    {
        _canvas.gameObject.SetActive(true);
        _img.color = new Color(0,0,0,0);
        _img.DOColor(new Color(0, 0, 0, 1), time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
    }
    
    public IEnumerator FadeOut(float time)
    {
        _img.color = new Color(0,0,0,1);
        _img.DOColor(new Color(0, 0, 0, 0), time).SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        _canvas.gameObject.SetActive(false);
    }
}
