using HarmonyLib;
using JumpKing.Mods;
using JumpKing.Workshop;
using System.Reflection;
using System.Linq;
using System;
using JumpKing;

namespace JumpKing_DisabledScreenEvents
{
    [JumpKingMod("YutaGoto.JumpKing_DisabledScreenEvents")]
    public static class ModEntry
    {
        internal static readonly string harmonyId = "YutaGoto.JumpKing_DisabledScreenEvents";
        internal static Harmony harmony = new Harmony(harmonyId);
        internal static string[] Tags = new string[0];

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad() { }

        /// <summary>
        /// Called by Jump King when the level unloads
        /// </summary>
        [OnLevelUnload]
        public static void OnLevelUnload() { }

        /// <summary>
        /// Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        public static void OnLevelStart()
        {
            Tags = XmlSerializerHelper.Deserialize<Level.LevelSettings>(Game1.instance.contentManager.root + "\\" + Level.FileName).Tags;

            if (Tags.Contains("DisableLightning"))
            {
                MethodInfo ligntingOnNewScreen = AccessTools.TypeByName("NBPTowerLightning").GetMethod("OnNewScreen", new Type[] { typeof(int) });
                MethodInfo prefixOnLigntningNewScreen = typeof(ModEntry).GetMethod("PrefixOnLigntningNewScreen");
                harmony.Patch(ligntingOnNewScreen, prefix: new HarmonyMethod(prefixOnLigntningNewScreen));
            }

            if (Tags.Contains("DisableFlyingGargoyle"))
            {
                MethodInfo flyingGargoyleOnNewScreen = AccessTools.TypeByName("OwlFlyingGargoyle").GetMethod("OnNewScreen", new Type[] { typeof(int) });
                MethodInfo prefixOnFlyingGargoyleNewScreen = typeof(ModEntry).GetMethod("PrefixOnFlyingGargoyleNewScreen");
                harmony.Patch(flyingGargoyleOnNewScreen, prefix: new HarmonyMethod(prefixOnFlyingGargoyleNewScreen));
            }
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() { }

        public static bool PrefixOnLigntningNewScreen(ref int p_screen1)
        {
            return false;
        }

        public static bool PrefixOnFlyingGargoyleNewScreen(ref int p_screen1)
        {
            return false;
        }
    }
}
