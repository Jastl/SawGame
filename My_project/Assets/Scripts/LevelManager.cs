public static class LevelManager
{
    private const int first_lvl_need_exp = 50;
    public static int need_experience { get; set; } = first_lvl_need_exp;
    public static int current_experiense { get; set; }
    public static int current_lvl { get; set; } = 1;
    private static void LvlUp(int remainder)
    {
        current_lvl++;
        need_experience = GetNeedExperience(current_lvl);
        current_experiense = remainder;
    }
    private static int GetNeedExperience(int lvl)
    {
        return(first_lvl_need_exp + first_lvl_need_exp / 2 * (lvl - 1));
    }
    public static void AddExperience(int exp)
    {
        if (need_experience - current_experiense > exp) current_experiense += exp;
        else LvlUp(exp - (need_experience - current_experiense));
    }
}

/* 1lvl - 50exp; 2lvl - 75exp; 3lvl - 100exp... etc

