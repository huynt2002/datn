using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defines
{
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
