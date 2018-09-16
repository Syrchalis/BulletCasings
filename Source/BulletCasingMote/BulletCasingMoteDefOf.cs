using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;
using Verse.AI;
using System.Reflection;

namespace BulletCasingMote
{
    [DefOf]
    public static class BulletCasingMoteDefOf
    {
        static BulletCasingMoteDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RecipeDefOf));
        }
        public static ThingDef Mote_BulletCasing;
        public static ThingDef Mote_BulletCasing_Charge;
        public static ThingDef Mote_BulletCasing_Shotgun;
        public static ThingDef Filth_BulletCasingsRifle;
        public static ThingDef Filth_BulletCasingsShotgun;
        public static ThingDef Filth_BulletCasingsCharge;
    }
}
