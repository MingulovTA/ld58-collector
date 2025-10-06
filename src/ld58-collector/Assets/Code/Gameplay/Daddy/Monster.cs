using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private float _speed = 5;
    public Transform SpawnPoint;
    public MonsterStateId MonsterStateId;

    private void Update()
    {
        if (MonsterStateId == MonsterStateId.Hunting)
        {
            Hunting();
        }
    }

    private void Kill()
    {
        MonsterStateId = MonsterStateId.Killing;
        
    }

    private IEnumerator KillYield()
    {
        Main.I.InputService.AddLocker(this);
        yield return new WaitForSeconds(.5f);
        yield return Main.I.FadeScreenService.FadeIn(.5f);
        Main.I.InputService.RemoveLocker(this);
        if (Main.I.Game.IsCollectedAllTreasures)
            Main.I.Game.Complete();
        else
            Main.I.Game.LoadCheckpoint();
        //Проиграть звук
    }
    
    private void Hide()
    {
        MonsterStateId = MonsterStateId.Hidding;
    }

    private void Hunting()
    {
        if (Main.I.Girl.gameObject.activeSelf)
        {
            transform.position = Vector3.MoveTowards(transform.position, Main.I.Girl.transform.position,
                Time.deltaTime * _speed);
            if (Vector2.Distance(transform.position, Main.I.Girl.transform.position) < .5f)
            {
                Kill();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, SpawnPoint.position,
                Time.deltaTime * _speed);
            if (Vector2.Distance(transform.position, Main.I.Girl.transform.position) < .5f)
            {
                Hide();
            }
        }
    }

}
