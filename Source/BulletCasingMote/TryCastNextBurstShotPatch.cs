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
    [HarmonyPatch(typeof(Verb), "TryCastNextBurstShot")]
    public static class TryCastNextBurstShotPatch
    {
        [HarmonyPostfix]
        public static void TryCastNextBurstShot_Postfix(Verb __instance)
        {
            if (__instance.verbProps.muzzleFlashScale > 0.01f && __instance.verbProps.verbClass != typeof(Verb_ShootOneUse))
            {
                if (__instance.CasterIsPawn)
                {
                    if (__instance.CasterPawn.equipment.Primary.def.defName.Contains("Charge", StringComparison.OrdinalIgnoreCase))
                    {
                        ThrowCasing(__instance.CasterPawn, __instance.caster.Map, __instance.GetProjectile().projectile.GetDamageAmount(1f), BulletCasingMoteDefOf.Mote_BulletCasing_Charge);
                    }
                    else if (__instance.CasterPawn.equipment.Primary.def.defName.Contains("Shotgun", StringComparison.OrdinalIgnoreCase))
                    {
                        ThrowCasing(__instance.CasterPawn, __instance.caster.Map, __instance.GetProjectile().projectile.GetDamageAmount(1f), BulletCasingMoteDefOf.Mote_BulletCasing_Shotgun);
                    }
                    else
                    {
                        ThrowCasing(__instance.CasterPawn, __instance.caster.Map, __instance.GetProjectile().projectile.GetDamageAmount(1f), BulletCasingMoteDefOf.Mote_BulletCasing);
                    }
                }
            } 
        }

        public static Mote ThrowCasing(Pawn caster, Map map, int weaponDamage, ThingDef moteDef)
        {
            if (!caster.Position.ShouldSpawnMotesAt(map) || map.moteCounter.Saturated)
            {
                return null;
            }
            float angle = (caster.TargetCurrentlyAimingAt.CenterVector3 - caster.DrawPos).AngleFlat();
            MoteThrownCasing moteThrown = (MoteThrownCasing)ThingMaker.MakeThing(moteDef, null);
            moteThrown.Scale = GenMath.LerpDouble(5f, 30f, 0.2f, 0.4f, weaponDamage);
            moteThrown.exactPosition = caster.Position.ToVector3Shifted();
            moteThrown.exactPosition += Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0f, 0f, 0.3f); //puts the casing slightly infront of the pawn
            moteThrown.rotationRate = Rand.Range(-360f, 360f);
            moteThrown.speed = Rand.Range(BulletCasingMoteSettings.velocityFactor.min, BulletCasingMoteSettings.velocityFactor.max);
            moteThrown.rotation = angle;
            GenSpawn.Spawn(moteThrown, caster.Position, map, WipeMode.Vanish);
            return moteThrown;
        }
    }
    public class MoteThrownCasing : MoteThrown
    {
        public float speed;
        public float rotation;
        protected override Vector3 NextExactPosition(float deltaTime)
        {
            Vector3 velocity = (Quaternion.AngleAxis((-Mathf.Rad2Deg * Mathf.Atan(1f - AgeSecs * 2f)) % 360f, Vector3.up) * new Vector3(1f, 0f, 0.25f) * speed);
            if (BulletCasingMoteSettings.useWeaponRotation)
            {
                velocity = velocity.RotatedBy(rotation);
            }
            return exactPosition + velocity * deltaTime;
        }
    }

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}