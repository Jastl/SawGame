using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_battle : MonoBehaviour
{
    public void StartBattle()
    {
        WorldData.in_battle = true;
        SceneManager.LoadScene("Battle_" + LvlBattleManager.lvl_battle);
    }
}
