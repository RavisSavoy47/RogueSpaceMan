﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Matrix4
    {
        public float M00, M01, M02, M03, M10, M11, M12, M13, M20, M21, M22, M23, M30, M31, M32, M33;

        /// <summary>
        /// The names for each point in the base
        /// </summary>
        /// <param name="m00">This is 1</param>
        /// <param name="m01">This is 0</param>
        /// <param name="m02">This is 0</param>
        /// <param name="m03">This is 0</param>
        /// <param name="m10">This is 0</param>
        /// <param name="m11">This is 1</param>
        /// <param name="m12">This is 0</param>
        /// <param name="m13">This is 0</param>
        /// <param name="m20">This is 0</param>
        /// <param name="m21">This is 0</param>
        /// <param name="m22">This is 1</param>
        /// <param name="m23">This is 0</param>
        /// <param name="m30">This is 0</param>
        /// <param name="m31">This is 0</param>
        /// <param name="m32">This is 0</param>
        /// <param name="m33">This is 1</param>
        public Matrix4(float m00, float m01, float m02, float m03,
                       float m10, float m11, float m12, float m13,
                       float m20, float m21, float m22, float m23,
                       float m30, float m31, float m32, float m33)
        {
            M00 = m00; M01 = m01; M02 = m02; M03 = m03;
            M10 = m10; M11 = m11; M12 = m12; M13 = m13;
            M20 = m20; M21 = m21; M22 = m22; M23 = m23;
            M30 = m30; M31 = m31; M32 = m32; M33 = m33;
        }

        /// <summary>
        /// It is the base for the matrix4
        /// </summary>
        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4(1, 0, 0, 0,
                                   0, 1, 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);
            }
        }


        /// <summary>
        /// Creates a new matrix that has been rotated by the x value in radians
        /// </summary>
        /// <param name="radians">The result of the rotation</param>
        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4
                (
                    1, 0, 0, 0,
                    0, (float)Math.Cos(radians), -(float)Math.Sin(radians), 0,
                    0, (float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                    0, 0, 0, 1
                );
        }

        /// <summary>
        /// Creates a new matrix that has been rotated by the y value in radians
        /// </summary>
        /// <param name="radians">The result of the rotation</param>
        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4
                (
                    (float)Math.Cos(radians), 0, (float)Math.Sin(radians), 0,
                    0, 1, 0, 0,
                    -(float)Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
                    0, 0, 0, 1
                );
        }

        /// <summary>
        /// Creates a new matrix that has been rotated by the z value in radians
        /// </summary>
        /// <param name="radians">The result of the rotation</param>
        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4
                (
                    (float)Math.Cos(radians), -(float)Math.Sin(radians), 0, 0,
                    (float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                );
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix</param>
        /// <param name="z">The z position of the new matrix</param>
        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
            return new Matrix4
                (
                    1, 0, 0, x,
                    0, 1, 0, y,
                    0, 0, 1, z,
                    0, 0, 0, 1
                );
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">The value to use to scale the matrix in the x axis</param>
        /// <param name="y">The value to use to scale the matrix in the y axis</param>
        /// <param name="z">The value to use to scale the matrix in the z axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix4 CreateScale(float x, float y, float z)
        {
            return new Matrix4
                (
                    x, 0, 0, 0,
                    0, y, 0, 0,
                    0, 0, z, 0,
                    0, 0, 0, 1
                );
        }

        //Lets the matrix4 be added together by another matrix4
        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02, lhs.M03 + rhs.M03,
                    lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12, lhs.M13 + rhs.M13,
                    lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22, lhs.M23 + rhs.M23,
                    lhs.M30 + rhs.M30, lhs.M31 + rhs.M31, lhs.M32 + rhs.M32, lhs.M33 + rhs.M33
                );
        }

        //Lets the matrix4 be subtracted by another matrix4
        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                   lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02, lhs.M03 - rhs.M03,
                   lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12, lhs.M13 - rhs.M13,
                   lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22, lhs.M23 - rhs.M23,
                   lhs.M30 - rhs.M30, lhs.M31 - rhs.M31, lhs.M32 - rhs.M32, lhs.M33 - rhs.M33
                );
        }

        //Lets the matrix4 be multiplied by another matrix4
        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    //Row1, Colum1
                    (lhs.M00 * rhs.M00) + (lhs.M01 * rhs.M10) + (lhs.M02 * rhs.M20) + (lhs.M03 * rhs.M30),
                    //Row1, Colum2
                    (lhs.M00 * rhs.M01) + (lhs.M01 * rhs.M11) + (lhs.M02 * rhs.M21) + (lhs.M03 * rhs.M31),
                    //Row1, Colum3
                    (lhs.M00 * rhs.M02) + (lhs.M01 * rhs.M12) + (lhs.M02 * rhs.M22) + (lhs.M03 * rhs.M32),
                    //Row1, Colum4
                    (lhs.M00 * rhs.M03) + (lhs.M01 * rhs.M13) + (lhs.M02 * rhs.M23) + (lhs.M03 * rhs.M33),

                    //Row2, Colum1
                    (lhs.M10 * rhs.M00) + (lhs.M11 * rhs.M10) + (lhs.M12 * rhs.M20) + (lhs.M13 * rhs.M30),
                    //Row2, Colum2
                    (lhs.M10 * rhs.M01) + (lhs.M11 * rhs.M11) + (lhs.M12 * rhs.M21) + (lhs.M13 * rhs.M31),
                    //Row2, Colum3
                    (lhs.M10 * rhs.M02) + (lhs.M11 * rhs.M12) + (lhs.M12 * rhs.M22) + (lhs.M13 * rhs.M32),
                    //Row2, Colum4
                    (lhs.M10 * rhs.M03) + (lhs.M11 * rhs.M13) + (lhs.M12 * rhs.M23) + (lhs.M13 * rhs.M33),

                    //Row3, Colum1
                    (lhs.M20 * rhs.M00) + (lhs.M21 * rhs.M10) + (lhs.M22 * rhs.M20) + (lhs.M23 * rhs.M30),
                    //Row3, Colum2
                    (lhs.M20 * rhs.M01) + (lhs.M21 * rhs.M11) + (lhs.M22 * rhs.M21) + (lhs.M23 * rhs.M31),
                    //Row3, Colum3
                    (lhs.M20 * rhs.M02) + (lhs.M21 * rhs.M12) + (lhs.M22 * rhs.M22) + (lhs.M23 * rhs.M32),
                    //Row3, Colum4
                    (lhs.M20 * rhs.M03) + (lhs.M21 * rhs.M13) + (lhs.M22 * rhs.M23) + (lhs.M23 * rhs.M33),

                    //Row4, Colum1
                    (lhs.M30 * rhs.M00) + (lhs.M31 * rhs.M10) + (lhs.M32 * rhs.M20) + (lhs.M33 * rhs.M30),
                    //Row4, Colum2
                    (lhs.M30 * rhs.M01) + (lhs.M31 * rhs.M11) + (lhs.M32 * rhs.M21) + (lhs.M33 * rhs.M31),
                    //Row4, Colum3
                    (lhs.M30 * rhs.M02) + (lhs.M31 * rhs.M12) + (lhs.M32 * rhs.M22) + (lhs.M33 * rhs.M32),
                    //Row4, Colum4
                    (lhs.M30 * rhs.M03) + (lhs.M31 * rhs.M13) + (lhs.M32 * rhs.M23) + (lhs.M33 * rhs.M33)
                );
        }

        //Lets a matrix4 be multiplied by a vector4
        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.M00 * rhs.X + lhs.M01 * rhs.Y + lhs.M02 * rhs.Z + lhs.M03 * rhs.W,
                               lhs.M10 * rhs.X + lhs.M11 * rhs.Y + lhs.M12 * rhs.Z + lhs.M13 * rhs.W,
                               lhs.M20 * rhs.X + lhs.M21 * rhs.Y + lhs.M22 * rhs.Z + lhs.M23 * rhs.W,
                               lhs.M30 * rhs.X + lhs.M31 * rhs.Y + lhs.M32 * rhs.Z + lhs.M33 * rhs.W);
        }
    }
}
