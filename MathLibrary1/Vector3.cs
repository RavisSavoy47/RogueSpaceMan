using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalization. Returns an empty vector if the magnitude is zero</returns>
        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();

            return this /= Magnitude;
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                Vector3 value = this;
                return value.Normalize();
            }

        }

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3((lhs.Y * rhs.Z) - (lhs.Z * rhs.Y),
                               (lhs.Z * rhs.X) - (lhs.X * rhs.Z),
                               (lhs.X * rhs.Y) - (lhs.Y * rhs.X));
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The dot product of the first vector on the second</returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        /// <summary>
        /// Finds the distance from the first vector to the second
        /// </summary>
        /// <param name="lhs">The starting point</param>
        /// <param name="rhs">The ending point</param>
        /// <returns>A scalar representing the distance</returns>
        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the x value of the second vector to the first, and adds the y vaqlue to the first
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increadr the 1st vector</param>
        /// <returns>The result of the vector addition</returns>
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        /// <summary>
        /// Subtract the x value of the second vector to the first, and subtracts the y vaqlue to the first
        /// </summary>
        /// <param name="lhs">The vector that is being subtracted from</param>
        /// <param name="rhs">The vector used to subtract from the 1st vector</param>
        /// <returns>The result of the vector subtraction</returns>
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        /// <summary>
        /// Multiplies the vector's x and y values by the scalor
        /// </summary>
        /// <param name="lhs">The vector that is being scaled</param>
        /// <param name="scalor">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling/returns>
        public static Vector3 operator *(Vector3 lhs, float rhs)
        {
            return new Vector3 { X = lhs.X * rhs, Y = lhs.Y * rhs, Z = lhs.Z * rhs };
        }

        public static Vector3 operator *(float rhs ,Vector3 lhs)
        {
            return new Vector3 { X = lhs.X * rhs, Y = lhs.Y * rhs, Z = lhs.Z * rhs };
        }

        /// <summary>
        /// Divides the vector's x and y values by the scalor
        /// </summary>
        /// <param name="lhs">The vector that is being scaled</param>
        /// <param name="scalor">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector3 operator /(Vector3 lhs, float rhs)
        {
            return new Vector3 { X = lhs.X / rhs, Y = lhs.Y / rhs, Z = lhs.Z / rhs };
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of teh comparison</param>
        /// <param name="rhs">The right side of the comparison</param>
        /// <returns>True if the x values of both vectors match and the y values match</returns>
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of teh comparison</param>
        /// <param name="rhs">the right side of the comparison</param>
        /// <returns>True if the x values of both vectors don't match and the y values don't match</returns>
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }
    }
}
