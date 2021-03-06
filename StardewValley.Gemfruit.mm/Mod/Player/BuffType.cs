using System;

namespace Gemfruit.Mod.Player
{
    public enum BuffType
    {
        Full = 6,
        Quenched = 7,
        GoblinsCurse= 12,
        Slimed = 13,
        Jinxed = 14,
        ChickenedOut = 15,
        Tipsy = 17,
        Spooked = 18,
        Frozen = 19,
        WarriorEnergy = 20,
        YobaBlessing = 21,
        AdrenalineRush = 22,
        OilOfGarlic = 23
    }
    
    public static class BuffTypeExt
    {
        public static readonly string[] Names = Enum.GetNames(typeof(BuffType));
        public static readonly int[] Values = (int[])Enum.GetValues(typeof(BuffType));
        public static readonly int Count = Names.Length;
    }
}