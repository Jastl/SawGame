using UnityEngine;
using UnityEngine.UI;

public class Battle_menu : MonoBehaviour
{
    public TextMesh lvl_text;
    public Image lvl_bar_image;
    public Image energy_bar_image;

    private int max_energy = 50; //
    private int curent_energy = 33; //
    private void Update()
    {
        lvl_text.text = LevelManager.current_lvl.ToString();
        lvl_bar_image.fillAmount = (float)LevelManager.current_experiense / LevelManager.need_experience;
        energy_bar_image.fillAmount = (float)curent_energy / max_energy;
    }
}
