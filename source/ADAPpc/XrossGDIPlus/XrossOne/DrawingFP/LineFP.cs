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
	public class LineFP
	{
		public int Length
		{
			get
			{
				return PointFP.Distance(P1, P2);
			}
			
		}
		public PointFP Center
		{
			get
			{
				return new PointFP((P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2);
			}
		}
		public PointFP P1 = new PointFP(0, 0);
		public PointFP P2 = new PointFP(0, 0);
		public LineFP()
		{
		}
		public LineFP(LineFP l)
		{
			Reset(l.P1, l.P2);
		}
		public LineFP(PointFP p1, PointFP p2)
		{
			Reset(p1, p2);
		}
		public LineFP(int ff_x1, int ff_y1, int ff_x2, int ff_y2)
		{
			Reset(ff_x1, ff_y1, ff_x2, ff_y2);
		}
		public void  Reset(LineFP l)
		{
			Reset(l.P1, l.P2);
		}
		public void  Reset(PointFP p1, PointFP p2)
		{
			this.P1.Reset(p1);
			this.P2.Reset(p2);
		}
		public void  Reset(int ff_x1, int ff_y1, int ff_x2, int ff_y2)
		{
			P1.Reset(ff_x1, ff_y1);
			P2.Reset(ff_x2, ff_y2);
		}
		public LineFP GetHeadOutline(int ff_rad)
		{
			PointFP p = new PointFP(P1.X - P2.X, P1.Y - P2.Y);
			int len = Length;
			p.Reset(MathFP.Div(- p.Y, len), MathFP.Div(p.X, len));
			p.Reset(MathFP.Mul(p.X, ff_rad), MathFP.Mul(p.Y, ff_rad));
			return new LineFP(P1.X - p.X, P1.Y - p.Y, P1.X + p.X, P1.Y + p.Y);
		}
		public LineFP GetTailOutline(int ff_rad)
		{
			PointFP c = Center;
			PointFP p = new PointFP(P2.X - c.X, P2.Y - c.Y);
			p.Reset(p.Y, - p.X);
			int dis = PointFP.Distance(PointFP.Origin, p);
			if (dis == 0)
				dis = 1;
			p.Reset(MathFP.Div(MathFP.Mul(p.X, ff_rad), dis), MathFP.Div(MathFP.Mul(p.Y, ff_rad), dis));
			return new LineFP(P2.X - p.X, P2.Y - p.Y, P2.X + p.X, P2.Y + p.Y);
		}
		private static bool IsEqual(int ff_val1, int ff_val2)
		{
			return IsZero(ff_val1 - ff_val2);
		}
		private static bool IsZero(int ff_val)
		{
			return MathFP.Abs(ff_val) < (1 << SingleFP.DecimalBits / 2);
		}
		public static bool Intersects(LineFP l1, LineFP l2, PointFP intersection)
		{
			int x = SingleFP.NaN;
			int y = SingleFP.NaN;
			
			if (intersection != null)
				intersection.Reset(x, y);
			
			int ax0 = l1.P1.X;
			int ax1 = l1.P2.X;
			int ay0 = l1.P1.Y;
			int ay1 = l1.P2.Y;
			int bx0 = l2.P1.X;
			int bx1 = l2.P2.X;
			int by0 = l2.P1.Y;
			int by1 = l2.P2.Y;
			
			int adx = (ax1 - ax0);
			int ady = (ay1 - ay0);
			int bdx = (bx1 - bx0);
			int bdy = (by1 - by0);
			
			if (IsZero(adx) && IsZero(bdx))
				return IsEqual(ax0, bx0);
			else if (IsZero(ady) && IsZero(bdy))
				return IsEqual(ay0, by0);
			else if (IsZero(adx))
			{
				// A  vertical
				x = ax0;
				y = IsZero(bdy)?by0:MathFP.Mul(MathFP.Div(bdy, bdx), x - bx0) + by0;
			}
			else if (IsZero(bdx))
			{
				// B vertical
				x = bx0;
				y = IsZero(ady)?ay0:MathFP.Mul(MathFP.Div(ady, adx), x - ax0) + ay0;
			}
			else if (IsZero(ady))
			{
				y = ay0;
				x = MathFP.Mul(MathFP.Div(bdx, bdy), y - by0) + bx0;
			}
			else if (IsZero(bdy))
			{
				y = by0;
				x = MathFP.Mul(MathFP.Div(adx, ady), y - ay0) + ax0;
			}
			else
			{
				int xma = MathFP.Div(ady, adx); // slope segment A
				int xba = ay0 - (MathFP.Mul(ax0, xma)); // y intercept of segment A
				
				int xmb = MathFP.Div(bdy, bdx); // slope segment B
				int xbb = by0 - (MathFP.Mul(bx0, xmb)); // y intercept of segment B
				
				// parallel lines? 
				if (xma == xmb)
				{
					// Need trig functions
					return xba == xbb;
				}
				else
				{
					// Calculate points of intersection
					// At the intersection of line segment A and B, XA=XB=XINT and YA=YB=YINT
					x = MathFP.Div((xbb - xba), (xma - xmb));
					y = (MathFP.Mul(xma, x)) + xba;
				}
			}
			
			// After the point or points of intersection are calculated, each
			// solution must be checked to ensure that the point of intersection lies
			// on line segment A and B.
			
			int minxa = MathFP.Min(ax0, ax1);
			int maxxa = MathFP.Max(ax0, ax1);
			
			int minya = MathFP.Min(ay0, ay1);
			int maxya = MathFP.Max(ay0, ay1);
			
			int minxb = MathFP.Min(bx0, bx1);
			int maxxb = MathFP.Max(bx0, bx1);
			
			int minyb = MathFP.Min(by0, by1);
			int maxyb = MathFP.Max(by0, by1);
			
			if (intersection != null)
				intersection.Reset(x, y);
			return ((x >= minxa) && (x <= maxxa) && (y >= minya) && (y <= maxya) && (x >= minxb) && (x <= maxxb) && (y >= minyb) && (y <= maxyb));
		}
	}
}