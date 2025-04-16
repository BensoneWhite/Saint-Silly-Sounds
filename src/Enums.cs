namespace SaintSillySounds;

public static class Enums
{
    public static SoundID[] Boom;

    public static void RegisterValues()
    {
        Boom = (
        [
            new SoundID("Beep", true),
            new SoundID("Boom", true),
            new SoundID("Buawa", true),
            new SoundID("Damn", true),
            new SoundID("Gong", true),
            new SoundID("GunShot", true),
            new SoundID("pixelated", true),
            new SoundID("scotlandBoom", true),
            new SoundID("SUS", true),
            new SoundID("weezer", true),
            new SoundID("YEOW", true)
        ]);
    }
}