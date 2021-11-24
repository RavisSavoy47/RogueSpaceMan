﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Matrix3
    {
        public float M00, M01, M02, M10, M11, M12, M20, M21, M22;

        /// <summary>
        /// The names for each point in the base
        /// </summary>
        /// <param name="m00">This is 1</param>
        /// <param name="m01">This is 0</param>
        /// <param name="m02">This is 0</param>
        /// <param name="m10">This is 0</param>
        /// <param name="m11">This is 1</param>
        /// <param name="m12">This is 0</param>
        /// <param name="m20">This is 0</param>
        /// <param name="m21">This is 0</param>
        /// <param name="m22">This is 1</param>
        public Matrix3(float m00, float m01, float m02,
                       float m10, float m11, float m12,
                       float m20, float m21, float m22)
        {
            M00 = m00; M01 = m01; M02 = m02;
            M10 = m10; M11 = m11; M12 = m12;
            M20 = m20; M21 = m21; M22 = m22;
        }

        /// <summary>
        /// It is the base for the matrix3
        /// </summary>
        public static Matrix3 Identity
        {
            get
            {
                return new Matrix3(1, 0, 0,
                                   0, 1, 0,
                                   0, 0, 1);
            }
        }


        /// <summary>
        /// Creates a new matrix that has been rotated by the given value in radians
        /// </summary>
        /// <param name="radians">The result of the rotation</param>
        public static Matrix3 CreateRotation(float radians)
        {
            return new Matrix3
                (
                    (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                    -(float)Math.Sin(radians), (float) Math.Cos(radians), 0,
                    0, 0, 1
                );
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix</param>
        public static Matrix3 CreateTranslation(float x, float y)
        {
            return new Matrix3
                (  
                    1, 0, x,
                    0, 1, y,
                    0, 0, 1
                );
        }

        //Creates a new matrix that been translated by a vector 2 value
        public static Matrix3 CreateTranslation(Vector2 vector)
        {
            return new Matrix3
                (
                    1, 0, vector.X,
                    0, 1, vector.Y,
                    0, 0, 1
                );
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by teh given value
        /// </summary>
        /// <param name="x">The value to use to scale the matrix in the x axis</param>
        /// <param name="y">The value to use to scale the matrix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix3 CreateScale(float x, float y)
        {
            return new Matrix3
                (
                    x, 0, 0,
                    0, y, 0,
                    0, 0, 1
                );
        }

        //Lets 2 matrix3 add together
        public static Matrix3 operator +(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3
                (
                    lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02,
                    lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12,
                    lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22
                );
        }

        //Lets 2 matrix3 subtract together
        public static Matrix3 operator -(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3
                (
                    lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02,
                    lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12,
                    lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22
                );
        }

        //Lets 2 matrix3 multiply together
        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3
                (
                    //Row1, Colum1
                    lhs.M00 * rhs.M00 + lhs.M01 * rhs.M10 + lhs.M02 * rhs.M20,
                    //Row1, Colum2
                    lhs.M00 * rhs.M01 + lhs.M01 * rhs.M11 + lhs.M02 * rhs.M21,
                    //Row1, Colum3
                    lhs.M00 * rhs.M02 + lhs.M01 * rhs.M12 + lhs.M02 * rhs.M22,

                    //Row2, Colum1
                    lhs.M10 * rhs.M00 + lhs.M11 * rhs.M10 + lhs.M12 * rhs.M20,
                    //Row2, Colum2
                    lhs.M10 * rhs.M01 + lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21,
                    //Row2, Colum3
                    lhs.M10 * rhs.M02 + lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22,

                    //Row3, Colum1
                    lhs.M20 * rhs.M00 + lhs.M21 * rhs.M10 + lhs.M22 * rhs.M20,
                    //Row3, Colum2
                    lhs.M20 * rhs.M01 + lhs.M21 * rhs.M10 + lhs.M22 * rhs.M21,
                    //Row3, Colum3
                    lhs.M20 * rhs.M02 + lhs.M21 * rhs.M10 + lhs.M22 * rhs.M22
                );
        }

        //Lets a matrix3 be multiplied by a vector3
        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.M00 * rhs.X + lhs.M01 * rhs.Y + lhs.M02 * rhs.Z,
                               lhs.M10 * rhs.X + lhs.M11 * rhs.Y + lhs.M12 * rhs.Z,
                               lhs.M20 * rhs.X + lhs.M21 * rhs.Y + lhs.M22 * rhs.Z);
        }
    }
}
