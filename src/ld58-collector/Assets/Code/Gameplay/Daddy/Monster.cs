using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Transform _view;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed = 5;
    public Transform SpawnPoint;
    public MonsterStateId MonsterStateId;

    public void StartHunting(Transform spawnPoint)
    {
        
        MonsterStateId = MonsterStateId.Hunting;
        SpawnPoint = spawnPoint;
        transform.position = new Vector3(spawnPoint.position.x,spawnPoint.position.y,transform.position.z);
        gameObject.SetActive(true);
        _animator.SetBool("b_walk", true);
        Main.I.SoundService.PlaySfx("wood_door");
    }

    private Vector3 _lastPosition;
    private bool _isRight;

    private void TurnRight()
    {
        //if (_isRight) return;
        _view.transform.localScale = new Vector3(1, 1, 1);
    }

    private void TurnLeft()
    {
        //if (!_isRight) return;
        _view.transform.localScale = new Vector3(-1, 1, 1);
    }
    
    
    private void Update()
    {
        if (transform.position.x > _lastPosition.x)
        {
            TurnRight();
        }
        else
        {
            TurnLeft();
        }
        
        _lastPosition = transform.position;
        if (MonsterStateId == MonsterStateId.Hunting)
        {
            Hunting();
        }
    }

    private void Kill()
    {
        _animator.SetBool("b_walk", false);
        MonsterStateId = MonsterStateId.Killing;
        Main.I.SoundService.PlaySfx("scream_girl");
        StartCoroutine(KillYield());
    }

    private IEnumerator KillYield()
    {
        Main.I.InputService.AddLocker(this);
        yield return new WaitForSeconds(.5f);
        yield return Main.I.FadeScreenService.FadeIn(.5f);
        yield return new WaitForSeconds(2);
        Main.I.InputService.RemoveLocker(this);
        if (Main.I.Game.IsCollectedAllTreasures)
            Main.I.Game.Complete();
        else
            Main.I.Game.LoadCheckpoint();
        //Проиграть звук
    }
    
    private void StopAttacking()
    {
        gameObject.SetActive(false);
        MonsterStateId = MonsterStateId.Hidding;
        GameState gs = Main.I.Game.GameState;
        gs.CheckStates["MonsterHunting"] = "Disabled";

        if (gs.TreasuresIds.Contains(TreasuresId.Picture) && !gs.TreasuresIds.Contains(TreasuresId.Flower))
        {
            gs.CheckStates["ParentSkaf"] = "Breaked";
            gs.CheckStates["Dr_Parent_Living"] = "Unlocked";
        }
        
        if (gs.TreasuresIds.Contains(TreasuresId.Picture) 
            && gs.TreasuresIds.Contains(TreasuresId.Flower)
            && gs.TreasuresIds.Contains(TreasuresId.Pendant)
            && !gs.TreasuresIds.Contains(TreasuresId.Doll))
        {
            gs.CheckStates["Dr_Living_Kitchen"] = "Unlocked";
        }

        Main.I.MonsterAttackRunner.StopAttackingIfNeed();
    }

    private void Hunting()
    {
        if (Main.I.Girl.gameObject.activeSelf)
        {
            Vector3 target = Main.I.Girl.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x,target.y,transform.position.z), 
                Time.deltaTime * _speed);
            if (Vector2.Distance(transform.position, Main.I.Girl.transform.position) < .5f)
            {
                //Main.I.SoundService.StopMusic();
                Kill();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(SpawnPoint.position.x,SpawnPoint.position.y,transform.position.z), 
                Time.deltaTime * _speed);
            if (Vector2.Distance(transform.position, SpawnPoint.position) < .5f)
            {
                Main.I.SoundService.PlayMusic("Game");
                StopAttacking();
            }
        }
    }

}
