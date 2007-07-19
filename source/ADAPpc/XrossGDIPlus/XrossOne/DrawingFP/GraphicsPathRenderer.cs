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
using XrossOne.FixedPoint;

namespace XrossOne.DrawingFP
{
	public sealed class GraphicsPathRenderer:GraphicsPathSketch
	{
		public const int MODE_XOR = 1;
		public const int MODE_ZERO = 2;

		public const int RENDERER_FRAC_Y = 4; 
		public const int RENDERER_FRAC_X = 4; 
		public const int RENDERER_REAL_X = 12; 
		public const int RENDERER_REAL_Y = 11; 
		public const int BUFFERSIZE = 0xFFFF; 
		
		public const int RENDERER_REAL_X_MASK = (1 << RENDERER_REAL_X) - 1;
		public const int RENDERER_REAL_Y_MASK = (1 << RENDERER_REAL_Y) - 1;
		public const int RENDERER_FRAC_X_FACTOR = 1 << RENDERER_FRAC_X;
		public const int RENDERER_FRAC_X_MASK = (1 << RENDERER_FRAC_X) - 1;
		
		private MatrixFP transformMatrix = null;
		private BrushFP fillStyle = null;
		private int[] scanbuf = null;
		private int[] scanbuf_tmp = null;
		private int[] buffer = null;
		private int scanline = 0;
		private int width = 0;
		private int height = 0;
		private int drawMode = MODE_XOR;
		private int scanIndex = 0;
		private int ff_xmin;
		private int ff_xmax;
		private int ff_ymin;
		private int ff_ymax;
		
		private static int[] counts = new int[256];
		private static int[] index = new int[256];
		
		public GraphicsPathRenderer():this(1, 1)
		{
		}
		public GraphicsPathRenderer(int width, int height)
		{
			ff_xmin = SingleFP.MaxValue;
			ff_xmax = SingleFP.MinValue;
			ff_ymin = SingleFP.MaxValue;
			ff_ymax = SingleFP.MinValue;
			Reset(width, height, width);
			scanbuf = new int[BUFFERSIZE];
			scanbuf_tmp = new int[BUFFERSIZE];
		}
		public int this[int x, int y]
		{
			get 
			{
				return buffer[x + y * width];
			}
			set
			{
				buffer[x + y * width] = value;
			}
		}
		public static void  RadixSort(int[] data_src, int[] data_tmp, int num)
		{
			int shift, i;
			int[] src = data_src;
			int[] dst = data_tmp;
			int[] tmp;
			for (shift = 0; shift <= 24; shift += 8)
			{
				for (i = 0; i < 256; i++)
					counts[i] = 0;
				
				for (i = 0; i < num; i++)
				{
					counts[(src[i] >> shift) & 0xFF]++;
				}
				int indexnow = 0;
				for (i = 0; i < 256; i++)
				{
					index[i] = indexnow;
					indexnow += counts[i];
				}
				for (i = 0; i < num; i++)
				{
					dst[index[(src[i] >> shift) & 0xFF]++] = src[i];
				}
				tmp = src;
				src = dst;
				dst = tmp;
			}
		}
		public int Width
		{
			get
			{
				return width;
			}
		}
		public int Height
		{
			get
			{
				return height;
			}
		}
		public void  DrawPath(GraphicsPathFP path, BrushFP style, int mode)
		{
			scanIndex = 0;
			drawMode = mode;
			path.Visit(this);
			RadixSort(scanbuf, scanbuf_tmp, scanIndex);
			fillStyle = style;
			//fillStyle.SetBounds(ff_xmin, ff_ymin, ff_xmax, ff_ymax);
			if (transformMatrix != null)
				fillStyle.GraphicsMatrix = transformMatrix;
			DrawBuffer();
			fillStyle = null;
		}
		public void  ScanPath(GraphicsPathFP path, MatrixFP matrix, int mode)
		{
			scanIndex = 0;
			drawMode = mode;
			transformMatrix = matrix;
			path.Visit(this);
			transformMatrix = null;
			RadixSort(scanbuf, scanbuf_tmp, scanIndex);
		}
		public void  DrawPath(GraphicsPathFP path, MatrixFP matrix, BrushFP fillStyle, int mode)
		{
			transformMatrix = matrix;
			DrawPath(path, fillStyle, mode);
			transformMatrix = null;
		}
		public void  Reset(int width, int height, int scanline)
		{
			buffer = new int[width * height];
			this.width = width;
			this.height = height;
			this.scanline = scanline;
		}
		public void  Clear(int color)
		{
			for (int i = 0; i < buffer.Length; i++) buffer[i] = color;
		}
		public void  FinalizeBuffer(int color)
		{
			Color bk = Color.FromArgb(color);
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Color c = Color.FromArgb(this[x, y]);
					if (c.A != 0x00)
					{
						if (c.A != 0xFF)
						{
							this[x, y] = Color.FromArgb(
								(c.R * c.A + (0xFF - c.A) * bk.R) >> 8, 
								(c.G * c.A + (0xFF - c.A) * bk.G) >> 8,
								(c.B * c.A + (0xFF - c.A) * bk.B) >> 8).ToArgb();
						}
					}
					else
					{
						this[x, y] = color;
					}
				}
			}
		}
		PointFP transformedPoint;
		public override void  MoveTo(PointFP point)
		{
			transformedPoint = new PointFP(point);
			if (transformMatrix != null)
				transformedPoint.Transform(transformMatrix);
			base.MoveTo(point);
		}
		public override void  LineTo(PointFP point)
		{
			//PointFP a = new PointFP(CurrentPoint);
			PointFP pntTemp = new PointFP(point);
			
			ff_xmin = MathFP.Min(ff_xmin, CurrentPoint.X);
			ff_xmax = MathFP.Max(ff_xmax, point.X);
			ff_ymin = MathFP.Min(ff_ymin, CurrentPoint.Y);
			ff_ymax = MathFP.Max(ff_ymax, point.Y);
			
			if (transformMatrix != null)
			{
				pntTemp.Transform(transformMatrix);
				//b.Transform(transformMatrix);
			}
			
			Scanline(transformedPoint.X, transformedPoint.Y, pntTemp.X, pntTemp.Y);
			transformedPoint = pntTemp;
			base.LineTo(point);
		}
		
		public void  DrawBuffer()
		{
			int curd = 0; 
			int cure = 0; 
			int cura = 0; 
			int cula = 0; 
			
			int cury = 0; 
			int curx = 0; 
			
			int curs = 0;
			
			int count = scanIndex;
			
			for (int c = 0; c <= count; c++)
			{
				curs = c == count?0:scanbuf[c];
				
				int newy = ((curs >> (RENDERER_REAL_X + RENDERER_FRAC_X + 1)) & RENDERER_REAL_Y_MASK);
				int newx = ((curs >> (RENDERER_FRAC_X + 1)) & RENDERER_REAL_X_MASK);
				if ((newx != curx) || (newy != cury))
				{
					int alp = (256 * cure) / (RENDERER_FRAC_Y) + 
						(256 * cula) / (RENDERER_FRAC_Y * (RENDERER_FRAC_X_FACTOR - 1)) + 
						(256 * cura) / (RENDERER_FRAC_Y * (RENDERER_FRAC_X_FACTOR - 1));
					if (alp != 0)
					{
						if (drawMode == MODE_XOR)
							alp = (alp & 0x100) != 0?(0xFF - (alp & 0xFF)):(alp & 0xFF);
						else
							alp = System.Math.Min(255, System.Math.Abs(alp));
						if (alp != 0)
							MergePixels(curx, cury, 1, alp);
					}
					cure = curd;
					
					if (newy == cury)
					{
						if (curd != 0)
						{
							alp = (256 * curd) / RENDERER_FRAC_Y;
							if (alp != 0)
							{
								if (drawMode == MODE_XOR)
									alp = (alp & 0x100) != 0?(0xFF - (alp & 0xFF)):(alp & 0xFF);
								else
									alp = System.Math.Min(255, System.Math.Abs(alp));
								if (alp != 0)
									MergePixels(curx + 1, cury, newx - curx - 1, alp);
							}
						}
					}
					else
					{
						cury = newy;
						curd = cure = 0;
					}
					
					curx = newx;
					cura = cula = 0;
				}
				
				if ((curs & 1) != 0)
				{
					curd++;
					cula += ((~ (curs >> 1)) & RENDERER_FRAC_X_MASK);
				}
				else
				{
					curd--;
					cura -= ((~ (curs >> 1)) & RENDERER_FRAC_X_MASK);
				}
			}
		}
		private void  Scanline(int ff_sx, int ff_sy, int ff_ex, int ff_ey)
		{
			int sx = ff_sx >> (SingleFP.DecimalBits - RENDERER_FRAC_X);
			int ex = ff_ex >> (SingleFP.DecimalBits - RENDERER_FRAC_X);
			int sy = (ff_sy * RENDERER_FRAC_Y) >> SingleFP.DecimalBits;
			int ey = (ff_ey * RENDERER_FRAC_Y) >> SingleFP.DecimalBits;
			int xmin = System.Math.Min(sx, ex);
			int xmax = System.Math.Max(sx, ex);
			int ymin = System.Math.Min(sy, ey);
			int ymax = System.Math.Max(sy, ey);
			int incx = ff_sx < ff_ex && ff_sy < ff_ey || ff_sx >= ff_ex && ff_sy >= ff_ey?1:- 1;
			int x = incx == 1?xmin:xmax;
			int dire = ff_sy < ff_ey?1:0;
			
			if (((ymin < 0) && (ymax < 0)) || ((ymin >= (height * RENDERER_FRAC_Y)) && (ymax >= (height * RENDERER_FRAC_Y))))
				return ;
			
			int n = System.Math.Abs(xmax - xmin);
			int d = System.Math.Abs(ymax - ymin);
			int i = d;
			
			ymax = System.Math.Min(ymax, height * RENDERER_FRAC_Y);
			
			for (int y = ymin; y < ymax; y++)
			{
				if (y >= 0)
				{
					scanbuf[scanIndex++] = ((y / RENDERER_FRAC_Y) << (RENDERER_REAL_X + RENDERER_FRAC_X + 1)) | (System.Math.Max(0, System.Math.Min((width * RENDERER_FRAC_X_FACTOR) - 1, x)) << 1) | dire;
					if ((scanIndex) >= BUFFERSIZE)
						return ;
				}
				i += n;
				if (i > d)
				{
					int idivd = (i - 1) / d;
					x += incx * idivd;
					i -= d * idivd;
				}
			}
		}
		private void  MergePixels(int x, int y, int count, int opacity)
		{
			bool isMonoColor = fillStyle.MonoColor;
			int color = 0;
			if (isMonoColor)
			{
				color = fillStyle.GetNextColor();
				color = ((((color >> 24) & 0xFF) * opacity) >> 8) << 24 | color & 0xFFFFFF;
			}
			int lastBackColor = 0;
			int lastMergedColor = 0;
			for (int i = 0; i < count; i++)
			{
				int bkColor = this[x + i, y];
				if (!isMonoColor)
				{
					color = i == 0 ? 
						fillStyle.GetColorAt(x + i, y, count == 1) :
						fillStyle.GetNextColor();
					if (opacity != 0xFF) 
						color = ((((color >> 24) & 0xFF) * opacity) >> 8) << 24 | color & 0xFFFFFF;
				}
				if (lastBackColor == bkColor && isMonoColor)
					this[x + i, y] = lastMergedColor;
				else
				{
					this[x + i, y] = Merge(bkColor, color);
					lastBackColor = bkColor;
					lastMergedColor = this[x + i, y];
				}
			}
		}
		public static int Merge(int color1, int color2)
		{
			int a2 = (color2 >> 24) & 0xFF;
			if (a2 == 0xFF || color1 == 0x0)
				return color2;
			else if (a2 == 0)
			{
				return color1;
				//return 0xFF << 24 | c1.R << 16 | c1.G << 8 | c1.B;
			}
			else
			{
				int a1 = 0xFF - ((color1 >> 24) & 0xFF);
				int a3 = 0xFF - a2;
				int b1 = color1 & 0xFF;
				int g1 = (color1 >> 8) & 0xFF;
				int r1 = (color1 >> 16) & 0xFF;
				int b2 = color2 & 0xFF;
				int g2 = (color2 >> 8) & 0xFF;
				int r2 = (color2 >> 16) & 0xFF;

				int Ca = (0xFF * 0xFF - a1 * a3) >> 8;
				int Cr = (r1 * a3 + r2 * a2) >> 8;
				int Cg = (g1 * a3 + g2 * a2) >> 8;
				int Cb = (b1 * a3 + b2 * a2) >> 8;
				return Ca << 24 | Cr << 16 | Cg << 8 | Cb;
			}
		}
	}
}