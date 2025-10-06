using System.Collections;
using System.Linq;
using UnityEngine;

public class MonsterAttackRunner : MonoBehaviour
{
    private Coroutine _huntingAnimation;
    
    public void TryToRun()
    {
        if (Main.I.Game.GameState.CheckStates["MonsterHunting"] == "Enabled")
        {
            Main.I.SoundService.PlayMusic("Hunting");
            Main.I.SoundService.PlaySfx("monster_enter");
            _huntingAnimation = StartCoroutine(RunAnimation());
        }
    }

    private IEnumerator RunAnimation()
    {
        var monsterDoor =  Main.I.Game.CurrentRoomView.DoorsViews.First(dv => dv.IsMonsterDoor == true);

        for (int i = 0; i <= 5; i++)
        {
            yield return Main.I.FadeScreenService.FadeOut(Random.Range(0.05f,0.1f));
            yield return new WaitForSeconds(Random.Range(0.05f,0.1f));
        }
        
        
        monsterDoor.OpenAnim();
        yield return new WaitForSeconds(.25f);
        Main.I.Monster.StartHunting(monsterDoor.transform);
        yield return new WaitForSeconds(.25f);
        monsterDoor.CloseAnim();
    }
    
    public void StopAttackingIfNeed()
    {
        if (Main.I.Game.GameState.CheckStates["MonsterHunting"]=="Enabled")
        {
            if (_huntingAnimation!=null)
                StopCoroutine(_huntingAnimation);
        }
        Main.I.Monster.gameObject.SetActive(false);
    }
}
