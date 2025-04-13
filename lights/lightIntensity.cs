
namespace Lighting
{
    public class LightIntensity
    {
        private float r, g, b;

        // Konstruktor główny
        public LightIntensity(float R, float G, float B)
        {
            r = R;
            g = G;
            b = B;
            Clamp();
        }

        // Inne konstruktory
        public LightIntensity(float R, float G) : this(R, G, 0.0f) { }
        public LightIntensity(float R) : this(R, 0.0f, 0.0f) { }
        public LightIntensity() : this(0.0f, 0.0f, 0.0f) { }

        public float GetRed() { return r; }
        public float GetGreen() { return g; }
        public float GetBlue() { return b; }

        // Dodawanie dwóch natężeń światła
        public static LightIntensity operator +(LightIntensity a, LightIntensity b)
        {
            return new LightIntensity(a.r + b.r, a.g + b.g, a.b + b.b);
        }

        // Mnożenie natężenia przez skalar
        public static LightIntensity operator *(LightIntensity a, float scalar)
        {
            return new LightIntensity(a.r * scalar, a.g * scalar, a.b * scalar);
        }

        // Dzielenie natężenia przez skalar
        public static LightIntensity operator /(LightIntensity a, float scalar)
        {
            if (scalar == 0)
                throw new DivideByZeroException("Nie można dzielić przez zero!");

            return new LightIntensity(a.r / scalar, a.g / scalar, a.b / scalar);
        }

        public static implicit operator Vector.Vector(LightIntensity d) {
            return new Vector.Vector(d.GetRed(),d.GetGreen(), d.GetBlue());
        }

        public static implicit operator Func<object, object, object>(LightIntensity v)
        {
            throw new NotImplementedException();
        }

        // aby wartości ujemne zamieniały się w 0
        private void Clamp()
        {
            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);
        }

        public LightIntensity Clamped()
        {
            return new LightIntensity(
                Math.Clamp(r, 0, 1),
                Math.Clamp(g, 0, 1),
                Math.Clamp(b, 0, 1)
            );
        }

        public override string ToString()
        {
            return $"LightIntensity(R: {r:F2}, G: {g:F2}, B: {b:F2})";
        }
    }
}
