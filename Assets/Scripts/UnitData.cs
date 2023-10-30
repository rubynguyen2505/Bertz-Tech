public class UnitData
{
    public int hp;
    public int atk;
    public int def;
    public int maxlvl;
    public int lvl;
    public string role;
    public string type;
    public int stars;

    public UnitData(int hp, int atk, int def, int maxlvl, int lvl, string role, string type, int stars)
    {
        this.hp = hp;
        this.atk = atk;
        this.def = def;
        this.maxlvl = maxlvl;
        this.lvl = lvl;
        this.role = role;
        this.type = type;
        this.stars = stars;
    }

}
