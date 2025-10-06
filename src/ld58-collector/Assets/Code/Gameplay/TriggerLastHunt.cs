using UnityEngine;

public class TriggerLastHunt : MonoBehaviour
{
    private bool _used;
    private void OnTriggerEnter(Collider collider)
    {
        if (_used) return;
        _used = true;
        var gs = Main.I.Game.GameState;
        if (gs.TreasuresIds.Contains(TreasuresId.Flower) &&
            gs.TreasuresIds.Contains(TreasuresId.Pendant) &&
            gs.TreasuresIds.Contains(TreasuresId.Picture)&&
            gs.TreasuresIds.Contains(TreasuresId.Doll))
        {
            Main.I.Game.GameState.CheckStates["MonsterHunting"] = "Enabled";
            Main.I.MonsterAttackRunner.TryToRun();
        }
    }
}
