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
using System.Drawing;
using XrossOne.DrawingFP;
using XrossOne.FixedPoint;

namespace XrossOne.Drawing
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class Utils
	{
		public static RectangleFP ToRectangleFP(Rectangle rect)
		{
			return new RectangleFP(
				SingleFP.FromInt(rect.Left),
				SingleFP.FromInt(rect.Top),
				SingleFP.FromInt(rect.Right),
				SingleFP.FromInt(rect.Bottom));
		}
		public static RectangleFP ToRectangleFP(RectangleF rect)
		{
			return new RectangleFP(
				SingleFP.FromFloat(rect.Left),
				SingleFP.FromFloat(rect.Top),
				SingleFP.FromFloat(rect.Right),
				SingleFP.FromFloat(rect.Bottom));
		}
		public static PointFP ToPointFP(Point pnt)
		{
			return new PointFP(SingleFP.FromInt(pnt.X), SingleFP.FromInt(pnt.Y));
		}
		public static PointFP ToPointFP(PointF pnt)
		{
			return new PointFP(SingleFP.FromFloat(pnt.X), SingleFP.FromFloat(pnt.Y));
		}
		public static Point ToPoint(PointFP pnt)
		{
			return new Point(SingleFP.ToInt(pnt.X), SingleFP.ToInt(pnt.Y));
		}
		public static PointF ToPointF(PointFP pnt)
		{
			return new PointF(SingleFP.ToFloat(pnt.X), SingleFP.ToFloat(pnt.Y));
		}

		public static PointFP[] ToPointFPArray(Point[] pnts)
		{
			PointFP[] result = new PointFP[pnts.Length];
			for (int i = 0; i < pnts.Length; i ++)
				result[i] = ToPointFP(pnts[i]);
			return result;
		}
		public static PointFP[] ToPointFPArray(PointF[] pnts)
		{
			PointFP[] result = new PointFP[pnts.Length];
			for (int i = 0; i < pnts.Length; i ++)
				result[i] = ToPointFP(pnts[i]);
			return result;
		}
		public static Point[] ToPointArray(PointFP[] pnts)
		{
			Point[] result = new Point[pnts.Length];
			for (int i = 0; i < pnts.Length; i ++)
				result[i] = ToPoint(pnts[i]);
			return result;
		}
		public static PointF[] ToPointFArray(PointFP[] pnts)
		{
			PointF[] result = new PointF[pnts.Length];
			for (int i = 0; i < pnts.Length; i ++)
				result[i] = ToPointF(pnts[i]);
			return result;
		}
		public static Color FromArgb(int alpha, Color c)
		{
			int color =  c.ToArgb();
			color = (alpha << 24) | (color & 0xFFFFFF);
			return Color.FromArgb(color);
		}
	}
}
