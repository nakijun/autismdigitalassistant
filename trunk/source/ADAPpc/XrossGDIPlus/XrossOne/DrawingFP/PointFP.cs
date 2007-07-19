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

namespace XrossOne.DrawingFP
{
	public class PointFP
	{
		public int X = 0;
		public int Y = 0;
		public static readonly PointFP Origin = new PointFP(0, 0);
		public static readonly PointFP Empty = new PointFP(SingleFP.NaN, SingleFP.NaN);
		
		public PointFP()
		{
		}
		public PointFP(PointFP p)
		{
			Reset(p);
		}
		public PointFP(int ff_x, int ff_y)
		{
			Reset(ff_x, ff_y);
		}
		public static bool IsEmpty(PointFP p)
		{
			return Empty.Equals(p);
		}
		public virtual PointFP Reset(PointFP p)
		{
			return Reset(p.X, p.Y);
		}
		public virtual PointFP Reset(int ff_x, int ff_y)
		{
			this.X = ff_x;
			this.Y = ff_y;
			return this;
		}
		public virtual PointFP Transform(MatrixFP m)
		{
			Reset(MathFP.Mul(X, m.Sx) + MathFP.Mul(Y, m.Ry) + m.Tx, MathFP.Mul(Y, m.Sy) + MathFP.Mul(X, m.Rx) + m.Ty);
			return this;
		}
		static public int Distance(PointFP p1, PointFP p2)
		{
			return Distance(p1.X - p2.X, p1.Y - p2.Y);
		}
		static public int Distance(int dx, int dy)
		{
			dx = MathFP.Abs(dx);
			dy = MathFP.Abs(dy);
			if (dx == 0)
				return dy;
			else if (dy == 0)
				return dx;
			
			long len = (((long) dx * dx) >> SingleFP.DecimalBits) + (((long) dy * dy) >> SingleFP.DecimalBits);
			long s = (dx + dy) - (MathFP.Min(dx, dy) >> 1);
			s = (s + ((len << SingleFP.DecimalBits) / s)) >> 1;
			s = (s + ((len << SingleFP.DecimalBits) / s)) >> 1;
			return (int) s;
		}
		public virtual PointFP Add(PointFP p)
		{
			Reset(X + p.X, Y + p.Y);
			return this;
		}
		public virtual PointFP Sub(PointFP p)
		{
			Reset(X - p.X, Y - p.Y);
			return this;
		}
		public  override bool Equals(System.Object obj)
		{
			PointFP p = (PointFP) obj;
			if (p != null)
				return X == p.X && Y == p.Y;
			else
				return false;
		}
		public override System.String ToString()
		{
			return "Point(" + new SingleFP(X) + "," + new SingleFP(Y) + ")";
		}
		static PointFP()
		{
			Empty = new PointFP(SingleFP.NaN, SingleFP.NaN);
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="jlca1306_3"'
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}