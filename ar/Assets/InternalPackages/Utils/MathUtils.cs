namespace PhishAR.Utils
{
    public static class MathUtils
    {
        public static float ConvertRange(float oldMin, float oldMax, float newMin, float newMax, float value)
        {
            var scale = (newMax - newMin) / (oldMax - oldMin);
            return newMin + (value - oldMin) * scale;
        }
    }
}
