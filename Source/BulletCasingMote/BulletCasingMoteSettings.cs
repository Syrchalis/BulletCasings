using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RimWorld;
using Verse;

namespace BulletCasingMote
{
    public class BulletCasingMoteSettings : ModSettings
    {
        public static bool useWeaponRotation;
        public static IntRange velocityFactor;
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref useWeaponRotation, "BulletCasingMote_useWeaponRotation", true, true);
            Scribe_Values.Look<IntRange>(ref velocityFactor, "BulletCasingMote_velocityFactor", new IntRange(2, 3), true);
        }
    }
}
