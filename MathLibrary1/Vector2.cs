using System;

namespace MathLibrary1
{
    public struct Vector2
    {
        public float Y;
        public float X;

        /// <summary>
        /// Adds the x value of the second vector to the first, and adds the y vaqlue to the first
        /// </summary>
        /// <param name="lhs">The vector that is increasing</param>
        /// <param name="rhs">The vector used to increadr the 1st vector</param>
        /// <returns>The result of the vector addition</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y };
        }

        /// <summary>
        /// Subtract the x value of the second vector to the first, and subtracts the y vaqlue to the first
        /// </summary>
        /// <param name="lhs">The vector that is being subtracted from</param>
        /// <param name="rhs">The vector used to subtract from the 1st vector</param>
        /// <returns>The result of the vector subtraction</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y };
        }

        /// <summary>
        /// Multiplies the vector's x and y values by the scalor
        /// </summary>
        /// <param name="lhs">The vector that is being scaled</param>
        /// <param name="scalor">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling/returns>
        public static Vector2 operator *(Vector2 lhs, float scalor)
        {
            return new Vector2 { X = lhs.X * scalor, Y = lhs.Y * scalor };
        }

        /// <summary>
        /// Divides the vector's x and y values by the scalor
        /// </summary>
        /// <param name="lhs">The vector that is being scaled</param>
        /// <param name="scalor">The value to scale the vector by</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator /(Vector2 lhs, float scalor)
        {
            return new Vector2 { X = lhs.X / scalor, Y = lhs.Y / scalor };
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of teh comparison</param>
        /// <param name="rhs">the right side of the comparison</param>
        /// <returns>True if the x values of both vectors match and the y values match</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        /// <summary>
        /// Compares the x and y values of two vectors
        /// </summary>
        /// <param name="lhs">The left side of teh comparison</param>
        /// <param name="rhs">the right side of the comparison</param>
        /// <returns>True if the x values of both vectors don't match and the y values don't match</returns>
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y;
        }
    }


}
