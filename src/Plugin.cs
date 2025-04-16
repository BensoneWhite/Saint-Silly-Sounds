using System;
using BepInEx;
using BepInEx.Logging;
using MonoMod.Cil;
using SaintSillySounds;
using Random = UnityEngine.Random;

[BepInPlugin(MOD_ID, MOD_NAME, VERSION)]
public class Ascensionsounds : BaseUnityPlugin
{
    private const string MOD_ID = "Ascensionsounds";
    private const string MOD_NAME = "Ascension Sounds";
    private const string VERSION = "1.2.1";
    private bool Init;

    private static new ManualLogSource Logger;

    private void DebugError(object data) => Logger.LogError(data);
    private void DebugInfo(object data) => Logger.LogInfo(data);

    public float volume;

    public void OnEnable()
    {
        Enums.RegisterValues();

        Logger = base.Logger;
        DebugInfo($"{MOD_NAME} is now active!! {VERSION}");
        On.RainWorld.OnModsInit += RainWorld_OnModsInit1;
    }

    private void RainWorld_OnModsInit1(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);
        try
        {
            if (Init) return;
            Init = true;

            IL.Player.ClassMechanicsSaint += Player_ClassMechanicsSaint;
        }
        catch (Exception ex)
        {
            DebugError(ex);
            throw;
        }
    }

    private void Player_ClassMechanicsSaint(ILContext il)
    {
        ILCursor cursor = new(il);
        try
        {
            if (cursor.TryGotoNext(MoveType.After, [i => i.MatchLdcR4(1f)]))
            {
                cursor.MoveAfterLabels();
                cursor.EmitDelegate((float _) => 0.1f);
            }
            else
            {
                DebugError("Failed to match float sound 1f");
            }

            cursor.Index = 0;

            if (cursor.TryGotoNext(MoveType.After, [i => i.MatchLdcR4(0.5f)]))
            {
                cursor.MoveAfterLabels();
                cursor.EmitDelegate((float _) => 0.60f);
            }
            else
            {
                DebugError("Failed to match float sound 0.5f");
            }

            cursor.Index = 0;

            if (cursor.TryGotoNext(MoveType.After, [i => i.MatchLdsfld(typeof(SoundID), nameof(SoundID.Firecracker_Bang))] ))
            {
                cursor.MoveAfterLabels();
                cursor.EmitDelegate((SoundID _) => SoundID.None);
            }
            else
            {
                DebugError("Failed to match SoundID");
            }

            cursor.Index = 0;

            if (cursor.TryGotoNext(MoveType.After, [i => i.MatchLdsfld(typeof(SoundID), nameof(SoundID.SS_AI_Give_The_Mark_Boom))] ))
            {
                cursor.MoveAfterLabels();
                cursor.EmitDelegate((SoundID _) => Enums.Boom[Random.Range(0, Enums.Boom.Length)]);
            }
            else
            {
                DebugError("Failed to match SoundID type");
            }
        }
        catch (Exception ex)
        {
            DebugError("Could find an injection point" + ex);
        }
    }
}