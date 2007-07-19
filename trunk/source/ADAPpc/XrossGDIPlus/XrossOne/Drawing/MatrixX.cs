/*

Copyright (C) 2004 XrossOne Studio (www.xrossone.com), All rights reserved.
Author : Xinjie ZHANG (xjzhang@xrossone.com)

This license governs use of the accompanying software ("Software"), and your use
of the Software constitutes acceptance of this license.

You may use the Software for any commercial or noncommercial purpose, including
distributing derivative works.

In return, we simply require that you agree:
1.	Not to remove any copyright or other notices from the Software.
2.	That if you distribute the Software in source code form you do so only under
this license (i.e. you must include a complete copy of this license with your
distribution), and if you distribute the Software solely in object form you only
do so under a license that complies with this license.
3.	That the Software comes "as is", with no warranties. None whatsoever. This
means no express, implied or statutory warranty, including without limitation,
warranties of merchantability or fitness for a particular purpose or any
warranty of title or non-infringement. Also, you must pass this disclaimer on
whenever you distribute the Software or derivative works.
4.	That XrossOne Studio will be liable for any of those types of damages known 
as indirect, special, consequential, or incidental related to the Software or this
license, to the maximum extent the law permits, no matter what legal theory it's
based on. Also, you must pass this limitation of liability on whenever you distribute 
the Software or derivative works.
5.	That if you sue anyone over patents that you think may apply to the Software
for a person's use of the Software, your license to the Software ends
automatically.
6.	That the patent rights, if any, granted in this license only apply to the
Software, not to any derivative works you make.
7.	That your rights under this License end automatically if you breach it in
any way.
8.	That all rights not expressly granted to you in this license are reserved.

*/

using System;
using XrossOne.FixedPoint;
using XrossOne.DrawingFP;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace XrossOne.Drawing
{
	/// <summary>
	/// Summary description for MatrixX.
	/// </summary>
	public class MatrixX
	{
		internal MatrixFP matrix;
		public MatrixX()
		{
			matrix = MatrixFP.Identity();
		}
        
		internal MatrixX(MatrixFP m)
		{
			matrix = m == null ? MatrixFP.Identity() : new MatrixFP(m);
		}
		public MatrixX (float m11, float m12, float m21, float m22, float dx, float dy)
		{
			matrix = new MatrixFP(
				SingleFP.FromFloat(m11), 
				SingleFP.FromFloat(m22),  
				SingleFP.FromFloat(m12),
				SingleFP.FromFloat(m21),
				SingleFP.FromFloat(dx),
				SingleFP.FromFloat(dy));
		}
        
		public float[] Elements 
		{
			get 
			{
				return matrix == null ?
					null : new float[]{
						SingleFP.ToFloat(matrix.Sx), 
						SingleFP.ToFloat(matrix.Rx), 
						SingleFP.ToFloat(matrix.Ry), 
						SingleFP.ToFloat(matrix.Sy),
						SingleFP.ToFloat(matrix.Tx), 
						SingleFP.ToFloat(matrix.Ty)};
			}
		}
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		public bool IsIdentity 
		{
			get 
			{
				return matrix == null ? false : matrix.IsIdentity;
			}
		}
        
		public bool IsInvertible 
		{
			get 
			{
				return matrix == null ? false : matrix.IsInvertible;
			}
		}
        
		public float OffsetX 
		{
			get 
			{
				return SingleFP.ToFloat(matrix.Tx);
			}
		}
        
		public float OffsetY 
		{
			get 
			{
				return SingleFP.ToFloat(matrix.Ty);
			}
		}

		public MatrixX Clone()
		{
			return new MatrixX (matrix);
		}
                       
		public override bool Equals (object obj)
		{
			MatrixX m = obj as MatrixX;
            return m != null && m.matrix.Equals(matrix);
		}
                      
		public void Invert ()
		{
			matrix.Invert();
		}
        
		public void Multiply (MatrixX m)
		{
			Multiply (m, MatrixOrderX.Prepend);
		}
        
		public void Multiply (MatrixX m, MatrixOrderX order)
		{
			if (order == MatrixOrderX.Prepend)
			{
				MatrixFP m1 = new MatrixFP(m.matrix);
				m1.Multiply(matrix);
				matrix = m1;
			}
			else
			{
				matrix.Multiply(m.matrix);
			}
		}
        
		public void Reset()
		{
			matrix = MatrixFP.Identity();
		}
        
		public void Rotate (float angle)
		{
			Rotate (angle, MatrixOrderX.Prepend);
		}
        
		public void Rotate (float angle, MatrixOrderX order)
		{
			if (order == MatrixOrderX.Prepend)
			{
				MatrixFP m = MatrixFP.Identity();
				m.Rotate(MathFP.ToRadians(SingleFP.FromFloat(angle)));
				m.Multiply(matrix);
				matrix = m;
			}
			else
			{
				matrix.Rotate(MathFP.ToRadians(SingleFP.FromFloat(angle)));
			}
		}
        
		public void RotateAt (float angle, PointF point)
		{
			RotateAt (angle, point, MatrixOrderX.Prepend);
		}
        
		public void RotateAt (float angle, PointF point, MatrixOrderX order)
		{
			if (order == MatrixOrderX.Prepend)
			{
				MatrixFP m = MatrixFP.Identity();
				m.Translate(SingleFP.FromFloat(-point.X), SingleFP.FromFloat(-point.Y));
				m.Rotate(MathFP.ToRadians(SingleFP.FromFloat(angle)));
				m.Multiply(matrix);
				m.Translate(SingleFP.FromFloat(point.X), SingleFP.FromFloat(point.Y));
				matrix = m;
			}
			else
			{
				matrix.Translate(SingleFP.FromFloat(-point.X), SingleFP.FromFloat(-point.Y));
				matrix.Rotate(MathFP.ToRadians(SingleFP.FromFloat(angle)));
				matrix.Translate(SingleFP.FromFloat(point.X), SingleFP.FromFloat(point.Y));
			}
		}
        
		public void Scale (float scaleX, float scaleY)
		{
			Scale (scaleX, scaleY, MatrixOrderX.Prepend);
		}
        
		public void Scale (float scaleX, float scaleY, MatrixOrderX order)
		{
			if (order == MatrixOrderX.Prepend)
			{
				MatrixFP m = MatrixFP.Identity();
				m.Scale(SingleFP.FromFloat(scaleX), SingleFP.FromFloat(scaleY));
				m.Multiply(matrix);
				matrix = m;
			}
			else
			{
				matrix.Scale(SingleFP.FromFloat(scaleX), SingleFP.FromFloat(scaleY));
			}
		}
        
		public void Shear (float shearX, float shearY)
		{
			Shear (shearX, shearY, MatrixOrderX.Prepend);
		}
        
		public void Shear (float shearX, float shearY, MatrixOrderX order)
		{
			if (order == MatrixOrderX.Prepend)
			{
				MatrixFP m = MatrixFP.Identity();
				m.RotateSkew(SingleFP.FromFloat(shearX), SingleFP.FromFloat(shearY));
				m.Multiply(matrix);
				matrix = m;
			}
			else
			{
				matrix.RotateSkew(SingleFP.FromFloat(shearX), SingleFP.FromFloat(shearY));
			}
		}
        
		public void TransformPoints (Point[] pts)
		{
			PointFP[] pnts = Utils.ToPointFPArray(pts);
			for (int i = 0; i < pts.Length; i ++)
				pnts[i].Transform(matrix);
			Point[] pnts1 = Utils.ToPointArray(pnts);
			for (int i = 0; i < pts.Length; i ++)
			{
				pts[i].X = pnts1[i].X;
				pts[i].Y = pnts1[i].Y;
			}
		}
        
		public void TransformPoints (PointF[] pts)
		{
			PointFP[] pnts = Utils.ToPointFPArray(pts);
			for (int i = 0; i < pts.Length; i ++)
				pnts[i].Transform(matrix);
			PointF[] pnts1 = Utils.ToPointFArray(pnts);
			for (int i = 0; i < pts.Length; i ++)
			{
				pts[i].X = pnts1[i].X;
				pts[i].Y = pnts1[i].Y;
			}
		}
        
		public void TransformVectors (Point[] pts)
		{
			int tx = matrix.Tx;
			int ty = matrix.Ty;
			matrix.Tx = 0; 
			matrix.Ty = 0;
			TransformPoints(pts);
			matrix.Tx = tx; 
			matrix.Ty = ty;
		}
        
		public void TransformVectors (PointF[] pts)
		{
			int tx = matrix.Tx;
			int ty = matrix.Ty;
			matrix.Tx = 0; 
			matrix.Ty = 0;
			TransformPoints(pts);
			matrix.Tx = tx; 
			matrix.Ty = ty;
		}
        
		public void Translate (float offsetX, float offsetY)
		{
			Translate (offsetX, offsetY, MatrixOrderX.Prepend);
		}
        
		public void Translate (float offsetX, float offsetY, MatrixOrderX order)
		{
			if (order == MatrixOrderX.Prepend)
			{
				MatrixFP m = MatrixFP.Identity();
				m.Translate(SingleFP.FromFloat(offsetX), SingleFP.FromFloat(offsetY));
				m.Multiply(matrix);
				matrix = m;
			}
			else
			{
				matrix.Translate(SingleFP.FromFloat(offsetX), SingleFP.FromFloat(offsetY));
			}
		}
        
		public void VectorTransformPoints (Point[] pts)
		{
			TransformVectors (pts);
		}	
	}
}
