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
namespace XrossOne.FixedPoint
{
	/// <summary> [ff_sx, ff_rx, 0] [ff_ry, ff_sy, 0] [ff_tx, ff_ty, 1]</summary>
	public class MatrixFP
	{
		public int Sx = 0; // ScaleX
		public int Sy = 0; // ScaleY
		public int Rx = 0; // RotateSkewX
		public int Ry = 0; // RotateSkewY
		public int Tx = 0; // TranslateX
		public int Ty = 0; // TranslateY
		public MatrixFP()
		{
			Sx = Sy = SingleFP.One;
		}
		public MatrixFP(int ff_sx, int ff_sy, int ff_rx, int ff_ry, int ff_tx, int ff_ty)
		{
			Reset(ff_sx, ff_sy, ff_rx, ff_ry, ff_tx, ff_ty);
		}
		public MatrixFP(MatrixFP m)
		{
			Reset(m.Sx, m.Sy, m.Rx, m.Ry, m.Tx, m.Ty);
		}
		public void  Reset()
		{
			Reset(SingleFP.One, SingleFP.One, 0, 0, 0, 0);
		}
		public static MatrixFP Identity()
		{
			return new MatrixFP(SingleFP.One, SingleFP.One, 0, 0, 0, 0);
		}
		public bool IsIdentity 
		{
			get 
			{
				return Sx == SingleFP.One && Sy == SingleFP.One && Rx == 0 && Ry == 0 && Tx == 0 && Ty == 0;
			}
		}
		public bool IsInvertible 
		{
			get 
			{
				return Determinant() != 0;
			}
		}
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		public void  Reset(int ff_sx, int ff_sy, int ff_rx, int ff_ry, int ff_tx, int ff_ty)
		{
			this.Sx = ff_sx;
			this.Sy = ff_sy;
			this.Rx = ff_rx;
			this.Ry = ff_ry;
			this.Tx = ff_tx;
			this.Ty = ff_ty;
		}
		public MatrixFP Rotate(int ff_ang)
		{
			int ff_sin = MathFP.Sin(ff_ang);
			int ff_cos = MathFP.Cos(ff_ang);
			return Multiply(new MatrixFP(ff_cos, ff_cos, ff_sin, - ff_sin, 0, 0));
		}
		public MatrixFP RotateSkew(int ff_rx, int ff_ry)
		{
			return Multiply(new MatrixFP(SingleFP.One, SingleFP.One, ff_rx, ff_ry, 0, 0));
		}
		public MatrixFP Translate(int ff_dx, int ff_dy)
		{
			this.Tx += ff_dx;
			this.Ty += ff_dy;
			return this;
		}
		public MatrixFP Scale(int ff_sx, int ff_sy)
		{
			Reset(MathFP.Mul(ff_sx, this.Sx), MathFP.Mul(ff_sy, this.Sy), MathFP.Mul(ff_sy, this.Rx), MathFP.Mul(ff_sx, this.Ry), MathFP.Mul(ff_sx, this.Tx), MathFP.Mul(ff_sy, this.Ty));
			return this;
		}
		public MatrixFP Multiply(MatrixFP m)
		{
			Reset(MathFP.Mul(m.Sx, Sx) + MathFP.Mul(m.Ry, Rx), MathFP.Mul(m.Rx, Ry) + MathFP.Mul(m.Sy, Sy), MathFP.Mul(m.Rx, Sx) + MathFP.Mul(m.Sy, Rx), MathFP.Mul(m.Sx, Ry) + MathFP.Mul(m.Ry, Sy), MathFP.Mul(m.Sx, Tx) + MathFP.Mul(m.Ry, Ty) + m.Tx, MathFP.Mul(m.Rx, Tx) + MathFP.Mul(m.Sy, Ty) + m.Ty);
			return this;
		}
		public override bool Equals (object obj)
		{
			MatrixFP m = obj as MatrixFP;
			return m != null && m.Rx == Rx && m.Ry == Ry && m.Sx == Sx && m.Sy == Sy && m.Tx == Tx && m.Ty == Ty;
		}
		public int Determinant()
		{
			int ff_det;
			ff_det = MathFP.Mul(Sx, Sy) - MathFP.Mul(Rx, Ry);
			return ff_det;
		}
		public MatrixFP Invert()
		{
			int ff_det = Determinant();
			if (ff_det == 0)
			{
				Reset();
			}
			else
			{
				int ff_sx_new = MathFP.Div(Sy, ff_det);
				int ff_sy_new = MathFP.Div(Sx, ff_det);
				int ff_rx_new = - MathFP.Div(Rx, ff_det);
				int ff_ry_new = - MathFP.Div(Ry, ff_det);
				int ff_tx_new = MathFP.Div(MathFP.Mul(Ty, Ry) - MathFP.Mul(Tx, Sy), ff_det);
				int ff_ty_new = - MathFP.Div(MathFP.Mul(Ty, Sx) - MathFP.Mul(Tx, Rx), ff_det);
				Reset(ff_sx_new, ff_sy_new, ff_rx_new, ff_ry_new, ff_tx_new, ff_ty_new);
			}
			return this;
		}
		public override System.String ToString()
		{
			return " Matrix(sx,sy,rx,ry,tx,ty)=(" + new SingleFP(Sx) + "," + new SingleFP(Sy) + "," + new SingleFP(Rx) + "," + new SingleFP(Ry) + "," + new SingleFP(Tx) + "," + new SingleFP(Ty) + ")";
		}
	}
}