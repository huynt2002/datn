public class Defines
{
    public enum DetectType
    {
        DetectPlayer, DetectEnemy
    }

    public enum MonsterType
    {
        Enemy, Ally
    }

    public static class Tag
    {
        public static string Player = "Player";
        public static string Enemy = "Enemy";
        public static string Ally = "Ally";
        public static string MainCam = "MainCamera";
        public static string StateLevel = "StateLevel";
        public static string ChestPosition = "ChestPos";
        public static string OneWayPlatform = "OneWayPlatform";
    }

    public enum DamageType
    {
        Entity, Trap, Projectile
    }
    public static class InfoButText
    {
        public static string Check = "Check";
        public static string Talk = "Talk";
        public static string PickUp = "Pick Up";
        public static string Buy = "Buy";
        public static string Refresh = "Refresh";

    }

    public static class DialogNPCText
    {
        public static string SaleNPC = "Welcome!";
    }

    public static class Physics
    {
        public static float GravityScale = 2;
    }
}
