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
	public class GraphicsPathFP
	{
		private const sbyte CMD_NOP = 0;
		private const sbyte CMD_MOVETO = 1;
		private const sbyte CMD_LINETO = 2;
		private const sbyte CMD_QCURVETO = 3;
		private const sbyte CMD_CCURVETO = 4;
		//public static final int CMD_ARCTO			= 5;
		private const sbyte CMD_CLOSE = 6;
		
		private const int BLOCKSIZE = 16;
		
		internal sbyte[] cmds = null;
		internal PointFP[] pnts = null;
		internal int cmdsSize = 0;
		internal int pntsSize = 0;
		
		public static GraphicsPathFP CreateLine(int ff_x1, int ff_y1, int ff_x2, int ff_y2)
		{
			GraphicsPathFP path = new GraphicsPathFP();
			path.AddMoveTo(new PointFP(ff_x1, ff_y1));
			path.AddLineTo(new PointFP(ff_x2, ff_y2));
			return path;
		}
		public static GraphicsPathFP CreateOval(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			GraphicsPathFP path = GraphicsPathFP.CreateArc(ff_xmin, ff_ymin, ff_xmax, ff_ymax, 0, MathFP.PI * 2, false);
			path.AddClose();
			return path;
		}
		
		public static GraphicsPathFP CreateRoundRect(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_rx, int ff_ry)
		{
			int ff_rmax;
			int FF_PI = MathFP.PI;
			GraphicsPathFP path = new GraphicsPathFP();
			path.AddMoveTo(new PointFP(ff_xmin + ff_rx, ff_ymin));
			path.AddLineTo(new PointFP(ff_xmax - ff_rx, ff_ymin));
			ff_rmax = MathFP.Min(ff_xmax - ff_xmin, ff_ymax - ff_ymin) / 2;
			if (ff_rx > ff_rmax)
				ff_rx = ff_rmax;
			if (ff_ry > ff_rmax)
				ff_ry = ff_rmax;
			if (ff_rx != 0 && ff_ry != 0)
				path.AddPath(GraphicsPathFP.CreateArc(ff_xmax - ff_rx * 2, ff_ymin, ff_xmax, ff_ymin + ff_ry * 2, (- FF_PI) / 2, 0, false, false));
			path.AddLineTo(new PointFP(ff_xmax, ff_ymin + ff_ry));
			path.AddLineTo(new PointFP(ff_xmax, ff_ymax - ff_ry));
			if (ff_rx != 0 && ff_ry != 0)
				path.AddPath(GraphicsPathFP.CreateArc(ff_xmax - ff_rx * 2, ff_ymax - ff_ry * 2, ff_xmax, ff_ymax, 0, FF_PI / 2, false, false));
			path.AddLineTo(new PointFP(ff_xmax - ff_rx, ff_ymax));
			path.AddLineTo(new PointFP(ff_xmin + ff_rx, ff_ymax));
			if (ff_rx != 0 && ff_ry != 0)
				path.AddPath(GraphicsPathFP.CreateArc(ff_xmin, ff_ymax - ff_ry * 2, ff_xmin + ff_rx * 2, ff_ymax, FF_PI / 2, FF_PI, false, false));
			path.AddLineTo(new PointFP(ff_xmin, ff_ymax - ff_ry));
			path.AddLineTo(new PointFP(ff_xmin, ff_ymin + ff_ry));
			if (ff_rx != 0 && ff_ry != 0)
				path.AddPath(GraphicsPathFP.CreateArc(ff_xmin, ff_ymin, ff_xmin + ff_rx * 2, ff_ymin + ff_ry * 2, - FF_PI, (- FF_PI) / 2, false, false));
			path.AddClose();
			return path;
		}
		public static PointFP CalcControlPoint(PointFP p1, PointFP p2, PointFP p3, int ff_factor)
		{
			PointFP ps = new PointFP(p2.X + MathFP.Mul(p2.X - p1.X, ff_factor), p2.Y + MathFP.Mul(p2.Y - p1.Y, ff_factor));
			return new LineFP(new LineFP(p2, ps).Center, new LineFP(p2, p3).Center).Center;
		}
		public static GraphicsPathFP CreateSmoothCurves(PointFP[] points,  int offset, int numberOfSegments, int ff_factor, bool closed)
		{
			int len = points.Length;
			GraphicsPathFP path = new GraphicsPathFP();

			if (numberOfSegments < 1 ||
				numberOfSegments > points.Length - 1 ||
				offset < 0 ||
				offset + numberOfSegments > len - 1) return path;

			PointFP[] PC1s = new PointFP[points.Length];
			PointFP[] PC2s = new PointFP[points.Length];
			if (!closed)
			{
				PC1s[0] = points[0];
				PC2s[len - 1] = points[len - 1];
			}
			else
			{
				PC1s[0] = CalcControlPoint(points[len - 1], points[0], points[1], ff_factor);
				PC2s[0] = CalcControlPoint(points[1], points[0], points[len - 1], ff_factor);
				PC1s[len - 1] = CalcControlPoint(points[len - 2], points[len - 1], points[0], ff_factor);
				PC2s[len - 1] = CalcControlPoint(points[0], points[len - 1], points[len - 2], ff_factor);
			}
			for (int i = 1; i < len - 1; i++)
			{
				PC1s[i] = CalcControlPoint(points[i - 1], points[i], points[i + 1], ff_factor);
				PC2s[i] = CalcControlPoint(points[i + 1], points[i], points[i - 1], ff_factor);
			}

			path.AddMoveTo(points[offset]);
			for (int i = 0; i < numberOfSegments; i++)
				path.AddCurveTo(PC1s[offset + i], PC2s[offset + i + 1], points[offset + i + 1]);
			if (closed)
			{
				path.AddCurveTo(PC1s[len - 1], PC2s[0], points[0]);
				path.AddClose();
			}
			return path;
		}
		public static GraphicsPathFP CreatePolyline(PointFP[] points)
		{
			GraphicsPathFP path = new GraphicsPathFP();
			if (points.Length > 0)
			{
				path.AddMoveTo(points[0]);
				for (int i = 1; i < points.Length; i++)
					path.AddLineTo(points[i]);
			}
			return path;
		}
		public static GraphicsPathFP CreatePolygon(PointFP[] points)
		{
			GraphicsPathFP path = CreatePolyline(points);
			if (points.Length > 0) path.AddClose();
			return path;
		}
		public static GraphicsPathFP CreateRect(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			return CreatePolygon(
				new PointFP[]{
					new PointFP(ff_xmin, ff_ymin), 
					new PointFP(ff_xmax, ff_ymin), 
					new PointFP(ff_xmax, ff_ymax),
					new PointFP(ff_xmin, ff_ymax)});
		}
		public GraphicsPathFP()
		{
		}
		
		public GraphicsPathFP(GraphicsPathFP from)
		{
			cmdsSize = from.cmdsSize;
			pntsSize = from.pntsSize;
			if (cmdsSize > 0)
			{
				cmds = new sbyte[cmdsSize];
				pnts = new PointFP[pntsSize];
				Array.Copy(from.cmds, 0, cmds, 0, cmdsSize);
				for (int i = 0; i < pntsSize; i++)
				{
					pnts[i] = new PointFP(from.pnts[i]);
				}
			}
		}
		public void  AddPath(GraphicsPathFP path)
		{
			if (path.cmdsSize > 0)
			{
				ExtendIfNeeded(path.cmdsSize, path.pntsSize);
				Array.Copy(path.cmds, 0, cmds, cmdsSize, path.cmdsSize);
				for (int i = 0; i < path.pntsSize; i++)
				{
					pnts[i + pntsSize] = new PointFP(path.pnts[i]);
				}
				cmdsSize += path.cmdsSize;
				pntsSize += path.pntsSize;
			}
		}
		public void  AddMoveTo(PointFP point)
		{
			ExtendIfNeeded(1, 1);
			cmds[cmdsSize++] = CMD_MOVETO;
			pnts[pntsSize++] = new PointFP(point);
		}
		public void  AddLineTo(PointFP point)
		{
			ExtendIfNeeded(1, 1);
			cmds[cmdsSize++] = CMD_LINETO;
			pnts[pntsSize++] = new PointFP(point);
		}
		public static GraphicsPathFP  CreateArc(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_arg1, int ff_arg2, bool closed)
		{
			return CreateArc(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_arg1, ff_arg2, closed, true);
		}
		public static GraphicsPathFP  CreateArc(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_startangle, int ff_sweepangle, bool closed, bool standalone)
		{
			if (ff_sweepangle < 0) 
			{
				ff_startangle += ff_sweepangle;
				ff_sweepangle = - ff_sweepangle;
			}
			int segments = MathFP.Round(MathFP.Div(4 * MathFP.Abs(ff_sweepangle), MathFP.PI)) >> SingleFP.DecimalBits;
			if (segments == 0)
				segments = 1;
			GraphicsPathFP path = new GraphicsPathFP();
			int ff_darg = ff_sweepangle / segments;
			int ff_arg = ff_startangle;
			int ff_lastcos = MathFP.Cos(ff_startangle);
			int ff_lastsin = MathFP.Sin(ff_startangle);
			int ff_xc = (ff_xmin + ff_xmax) / 2;
			int ff_yc = (ff_ymin + ff_ymax) / 2;
			int ff_rx = (ff_xmax - ff_xmin) / 2;
			int ff_ry = (ff_ymax - ff_ymin) / 2;
			int ff_RXBETA = MathFP.Mul(17381, ff_rx);
			int ff_RYBETA = MathFP.Mul(17381, ff_ry);
			int ff_currcos, ff_currsin, ff_x1, ff_y1, ff_x2, ff_y2;

			if (closed)
				path.AddMoveTo(new PointFP(ff_xc, ff_yc));

			for (int i = 1; i <= segments; i++)
			{
				ff_arg = i == segments?ff_startangle + ff_sweepangle:ff_arg + ff_darg;
				ff_currcos = MathFP.Cos(ff_arg);
				ff_currsin = MathFP.Sin(ff_arg);
				ff_x1 = ff_xc + MathFP.Mul(ff_rx, ff_lastcos);
				ff_y1 = ff_yc + MathFP.Mul(ff_ry, ff_lastsin);
				ff_x2 = ff_xc + MathFP.Mul(ff_rx, ff_currcos);
				ff_y2 = ff_yc + MathFP.Mul(ff_ry, ff_currsin);
				if (i == 1) 
					if (closed)
						path.AddLineTo(new PointFP(ff_x1, ff_y1));
					else
						if (standalone)
							path.AddMoveTo(new PointFP(ff_x1, ff_y1));

				path.AddCurveTo(
					new PointFP(ff_x1 - MathFP.Mul(ff_RXBETA, ff_lastsin), ff_y1 + MathFP.Mul(ff_RYBETA, ff_lastcos)),
					new PointFP(ff_x2 + MathFP.Mul(ff_RXBETA, ff_currsin), ff_y2 - MathFP.Mul(ff_RYBETA, ff_currcos)), 
					new PointFP(ff_x2, ff_y2));
				ff_lastcos = ff_currcos;
				ff_lastsin = ff_currsin;
			}
			if (closed) 
				path.AddClose();
			return path;
		}
		public void  AddCurveTo(PointFP control, PointFP point)
		{
			if (control.Equals(point))
			{
				AddLineTo(point);
				return;
			}
			ExtendIfNeeded(1, 2);
			cmds[cmdsSize++] = CMD_QCURVETO;
			pnts[pntsSize++] = new PointFP(control);
			pnts[pntsSize++] = new PointFP(point);
		}
		public void  AddCurveTo(PointFP control1, PointFP control2, PointFP point)
		{
			if (pnts[pntsSize-1].Equals(control1))
			{
				AddCurveTo(control2, point);
				return;
			}
			if (point.Equals(control2))
			{
				AddCurveTo(control1, point);
				return;
			}
			ExtendIfNeeded(1, 3);
			cmds[cmdsSize++] = CMD_CCURVETO;
			pnts[pntsSize++] = new PointFP(control1);
			pnts[pntsSize++] = new PointFP(control2);
			pnts[pntsSize++] = new PointFP(point);
		}
		public virtual void  AddClose()
		{
			ExtendIfNeeded(1, 0);
			cmds[cmdsSize++] = CMD_CLOSE;
		}
		private void  Init()
		{
			cmds = null;
			pnts = null;
			cmdsSize = pntsSize = 0;
		}
		public virtual void  Visit(GraphicsPathIterator iterator)
		{
			if (iterator != null)
			{
				PointFP p0 = new PointFP();
				PointFP p1 = new PointFP();
				PointFP p2 = new PointFP();
				iterator.Begin();
				int j = 0;
				for (int i = 0; i < cmdsSize; i++)
				{
					switch (cmds[i])
					{
						
						case CMD_NOP: 
							break;
						
						case CMD_MOVETO: 
							iterator.MoveTo(pnts[j++]);
							break;
						
						case CMD_LINETO: 
							iterator.LineTo(pnts[j++]);
							break;
						
						case CMD_QCURVETO: 
							iterator.CurveTo(pnts[j++], pnts[j++]);
							break;
						
						case CMD_CCURVETO: 
							iterator.CurveTo(pnts[j++], pnts[j++], pnts[j++]);
							break;
						
						case CMD_CLOSE: 
							iterator.Close();
							break;
						
						default: 
							return ;
						
					}
				}
				iterator.End();
			}
		}
		
		protected internal virtual void  ExtendIfNeeded(int cmdsAddNum, int pntsAddNum)
		{
			if (cmds == null)
				cmds = new sbyte[BLOCKSIZE];
			if (pnts == null)
				pnts = new PointFP[BLOCKSIZE];
			
			if (cmdsSize + cmdsAddNum > cmds.Length)
			{
				sbyte[] newdata = new sbyte[cmds.Length + (cmdsAddNum > BLOCKSIZE?cmdsAddNum:BLOCKSIZE)];
				if (cmdsSize > 0)
					Array.Copy(cmds, 0, newdata, 0, cmdsSize);
				cmds = newdata;
			}
			if (pntsSize + pntsAddNum > pnts.Length)
			{
				PointFP[] newdata = new PointFP[pnts.Length + (pntsAddNum > BLOCKSIZE?pntsAddNum:BLOCKSIZE)];
				if (pntsSize > 0)
					Array.Copy(pnts, 0, newdata, 0, pntsSize);
				pnts = newdata;
			}
		}
		
		internal static PointFP[] roundCap = new PointFP[7];
		internal static PointFP[] squareCap = new PointFP[2];
		internal static int One;
		public virtual GraphicsPathFP CalcOutline(PenFP lineStyle)
		{
			GraphicsPathFP outline = new GraphicsPathFP();
			GraphicsPathSketch outlineGenerator = new GraphicsPathOutline(this, outline, lineStyle);
			Visit(outlineGenerator);
			return outline;
		}
		static GraphicsPathFP()
		{
			One = SingleFP.One;
			roundCap[0] = new PointFP(25080, 60547);
			roundCap[1] = new PointFP(46341, 46341);
			roundCap[2] = new PointFP(60547, 25080);
			roundCap[3] = new PointFP(One, 0);
			roundCap[4] = new PointFP(60547, - 25080);
			roundCap[5] = new PointFP(46341, - 46341);
			roundCap[6] = new PointFP(25080, - 60547);
				
			squareCap[0] = new PointFP(One, One);
			squareCap[1] = new PointFP(One, - One);
		}
	}
}