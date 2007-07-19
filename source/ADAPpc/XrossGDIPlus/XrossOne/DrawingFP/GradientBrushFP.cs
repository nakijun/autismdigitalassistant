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
	public class GradientBrushFP:BrushFP
	{
		override public bool MonoColor
		{
			get
			{
				return false;
			}
		}
		public const int LINEAR_GRADIENT = 1;
		public const int RADIAL_GRADIENT = 2;
		public const int RATIO_BITS = 10;
		public const int RATIO_MAX = (1 << RATIO_BITS) - 1;
		private int type = LINEAR_GRADIENT;
		private int[] gradientColors = new int[1 << RATIO_BITS];
		private int[] ratios = new int[64];
		private int ratioCount = 0;
		private int ff_length;
		private int ff_currpos;
		private int ff_deltapos;
		protected RectangleFP bounds = new RectangleFP();

		public GradientBrushFP(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_angle):
			this(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_angle,LINEAR_GRADIENT)
		{
		}
		public GradientBrushFP(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_angle, int type)
		{
			bounds.Reset(ff_xmin, ff_ymin, 
				ff_xmax == ff_xmin ? ff_xmin + 1 : ff_xmax, 
				ff_ymax == ff_ymin ? ff_ymin + 1 : ff_ymax);
			matrix = new MatrixFP();
			matrix.Translate(-(ff_xmin + ff_xmax) / 2, -(ff_ymin + ff_ymax) / 2);
			matrix.Rotate(-ff_angle);
			this.type = type;
			if (type == RADIAL_GRADIENT)
				matrix.Scale(MathFP.Div(SingleFP.One, bounds.Width), MathFP.Div(SingleFP.One, bounds.Height));
            int ff_ang = MathFP.Atan(MathFP.Div(bounds.Height, bounds.Width == 0 ? 1 : bounds.Width));
			int ff_len = PointFP.Distance(bounds.Height, bounds.Width);
			ff_length = MathFP.Mul(ff_len, MathFP.Max(
				MathFP.Abs(MathFP.Cos(ff_angle - ff_ang)),
				MathFP.Abs(MathFP.Cos(ff_angle + ff_ang))));
		}
		public void  SetGradientColor(int ratio, int color)
		{
			ratio = ratio >> SingleFP.DecimalBits - RATIO_BITS;
			int i;
			ratio = ratio < 0?0:(ratio > RATIO_MAX?RATIO_MAX:ratio);
			if (ratioCount == ratios.Length)
			{
				int[] rs = new int[ratioCount + 16];
				Array.Copy(ratios, 0, rs, 0, ratioCount);
				ratios = rs;
			}
			gradientColors[ratio] = color;
			for (i = ratioCount; i > 0; i--)
				if (ratio >= ratios[i - 1])
					break;
			if (!(i > 0 && ratio == ratios[i]))
			{
				if (i < ratioCount)
					Array.Copy(ratios, i, ratios, i + 1, ratioCount - i);
				ratios[i] = ratio;
				ratioCount++;
			}
		}
		public void  UpdateGradientTable()
		{
			if (ratioCount == 0)
				return ;
			int i;
			for (i = 0; i < ratios[0]; i++)
			{
				gradientColors[i] = gradientColors[ratios[0]];
			}
			for (i = 1; i < ratioCount; i++)
			{
				int r1 = ratios[i - 1];
				int r2 = ratios[i];
				for (int j = r1 + 1; j < r2; j++)
				{
					gradientColors[j] = Interpolate(gradientColors[r1], gradientColors[r2], 256 * (j - r1) / (r2 - r1));
				}
			}
			for (i = ratios[ratioCount - 1]; i <= RATIO_MAX; i++)
			{
				gradientColors[i] = gradientColors[ratios[ratioCount - 1]];
			}
		}
		public static int Interpolate(int a, int b, int pos)
		{
			int p2 = pos & 0xFF;
			int p1 = 0xFF - p2;
			int ca = ((a >> 24) & 0xFF) * p1 + ((b >> 24) & 0xFF) * p2;
			int cr = ((a >> 16) & 0xFF) * p1 + ((b >> 16) & 0xFF) * p2;
			int cg = ((a >> 8) & 0xFF) * p1 + ((b >> 8) & 0xFF) * p2;
			int cb = (a & 0xFF) * p1 + (b & 0xFF) * p2;
			return ((ca >> 8) << 24) | ((cr >> 8) << 16) | ((cg >> 8) << 8) | ((cb >> 8));
		}
		public override int GetColorAt(int x, int y, bool singlePoint)
		{
			int pos;
			PointFP p = new PointFP(x << SingleFP.DecimalBits, y << SingleFP.DecimalBits);
			PointFP p1 = null;
			if (!singlePoint) p1 = new PointFP(p.X + SingleFP.One, p.Y);
			if (finalMatrix != null)
			{
				p.Transform(finalMatrix);
				if (!singlePoint) p1.Transform(finalMatrix);
			}
			int width = bounds.Width;
			int height = bounds.Height;
			if (type == LINEAR_GRADIENT)
			{
				int v = p.X  + ff_length / 2;
				if (v < 0) v = 0; 
				else if (v > ff_length - 1) v = ff_length - 1;
				ff_currpos = (int)(((long)v << RATIO_BITS + SingleFP.DecimalBits) / ff_length);
				if (!singlePoint) 
					ff_deltapos = (int)(((long)(p1.X - p.X) << RATIO_BITS + SingleFP.DecimalBits) / ff_length);
				pos = ff_currpos >> SingleFP.DecimalBits;
			}
			else
			{
				ff_currpos = PointFP.Distance(p.X, p.Y);
				if (!singlePoint) 
					ff_deltapos = PointFP.Distance(p1.X, p1.Y) - ff_currpos;
				//if (ff_currpos > SingleFP.One - 1) pos = SingleFP.One - 1;
				pos = ff_currpos >> SingleFP.DecimalBits - RATIO_BITS;
			}
			//pos >>= BrushFP.XY_MAX_BITS - RATIO_BITS;
			pos = pos < 0?0:(pos > RATIO_MAX?RATIO_MAX:pos);
			return gradientColors[pos];
		}
		public override int GetNextColor()
		{
			ff_currpos += ff_deltapos;
			int pos = ff_currpos >> SingleFP.DecimalBits;
			pos = pos < 0?0:(pos > RATIO_MAX?RATIO_MAX:pos);
			return gradientColors[pos];
		}
	}
}