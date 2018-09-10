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
    public class BulletCasingMote : Mod
    {
        public static BulletCasingMoteSettings settings;
        public BulletCasingMote(ModContentPack content) : base(content)
        {
            settings = GetSettings<BulletCasingMoteSettings>();
            var harmony = HarmonyInstance.Create("Syrchalis.Rimworld.BulletCasingMote");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public override string SettingsCategory() => "BulletCasingMoteSettingsCategory".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            checked
            {
                Listing_Standard listing_Standard = new Listing_Standard();
                listing_Standard.Begin(inRect);
                listing_Standard.CheckboxLabeled("BulletCasingUseWeaponRotation".Translate(), ref BulletCasingMoteSettings.useWeaponRotation, ("BulletCasingUseWeaponRotationTooltip".Translate()));
                listing_Standard.Label("BulletCasingVelocityFactor".Translate());
                listing_Standard.IntRange(ref BulletCasingMoteSettings.velocityFactor, 0, 10);
                listing_Standard.End();
                settings.Write();
            }
        }
        public override void WriteSettings()
        {
            base.WriteSettings();
        }
    }
}
