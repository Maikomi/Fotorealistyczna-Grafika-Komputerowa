
namespace Vector
{
    public class Ray 
    {
        public Vector Origin {get; set;}
        public Vector Direction {get; set;}

        public Ray( Vector origin, Vector direction) 
        {
            Origin = origin ?? new Vector(0, 0, 0);
            Direction = direction ?? new Vector(1, 0, 0);
            NormalizeDirection();
        }

        public static bool operator ==(Ray r1, Ray r2)
        {
            return r1.Origin == r2.Origin && r1.Direction == r2.Direction;
        }

        public static bool operator !=(Ray r1, Ray r2)
        {
            return !(r1 == r2);
        }

        public Vector PointAt(float distance)
        {
            return Vector.Add(Origin, Vector.MultiplyScalar(Direction, distance));
        }

        public void NormalizeDirection()
        {
            Direction = Direction.Normalize();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}