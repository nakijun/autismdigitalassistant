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
using System.Drawing;

namespace XrossOne.DrawingFP
{
	public sealed class GraphicsFP
	{
		private PenFP lineStyle;
		private BrushFP fillStyle;
		private GraphicsPathRenderer renderer = new GraphicsPathRenderer();
		private int paintMode;
		private System.Drawing.Bitmap bufferedImage;
		private MatrixFP matrix = null;

		public GraphicsFP()
		{
			InitBlock();
		}
		public GraphicsFP(int width, int height)
		{
			InitBlock();
			Resize(width, height);
		}
		private void  InitBlock()
		{
			bufferedImage = null;
			lineStyle = new PenFP(0x0, SingleFP.One);
			fillStyle = new SolidBrushFP(0x0);
			paintMode = GraphicsPathRenderer.MODE_XOR;
		}
		public BrushFP Brush
		{
			get
			{
				return fillStyle;
			}
			set
			{
				fillStyle = value;
			}
		}
		public PenFP Pen
		{
			get
			{
				return lineStyle;
			}
			set
			{
				lineStyle = value;
			}
		}
		public int PaintMode
		{
			get
			{
				return paintMode;
			}
			set
			{
				paintMode = value;
			}
		}
		public System.Drawing.Image BufferedImage
		{
			get
			{
				int w = renderer.Width;
				int h = renderer.Height;
				if (bufferedImage == null)
				{
					BitmapBuffer tempBuffer = new BitmapBuffer(w, h);
					for (int i = 0; i < w; i++)
					{
						for (int j = 0; j < h; j++)
							tempBuffer[i, j] = renderer[i, j];
					}
					bufferedImage = tempBuffer.CreateBitmap();
				}
				return bufferedImage;
			}
		}
		public MatrixFP Matrix
		{
			get
			{
				return matrix;
			}
			set
			{
				matrix = value == null ? null : new MatrixFP(value);
			}
		}
		public void  Resize(int width, int height)
		{
			bufferedImage = null;
			renderer.Reset(width, height, width);
		}
		
		public void  Clear(int color)
		{
			bufferedImage = null;
			renderer.Clear(color);
		}
		
		public void  DrawLine(int ff_x1, int ff_y1, int ff_x2, int ff_y2)
		{
			DrawPath(GraphicsPathFP.CreateLine(ff_x1, ff_y1, ff_x2, ff_y2));
		}
		public void  DrawPolyline(PointFP[] points)
		{
			DrawPath(GraphicsPathFP.CreatePolyline(points));
		}
		public void  DrawPolygon(PointFP[] points)
		{
			DrawPath(GraphicsPathFP.CreatePolygon(points));
		}
		public void  DrawCurves(PointFP[] points, int offset, int numberOfSegments, int ff_factor)
		{
			DrawPath(GraphicsPathFP.CreateSmoothCurves(points, offset, numberOfSegments, ff_factor, false));
		}
		public void  DrawClosedCurves(PointFP[] points, int offset, int numberOfSegments, int ff_factor)
		{
			DrawPath(GraphicsPathFP.CreateSmoothCurves(points, offset, numberOfSegments, ff_factor, true));
		}
		public void  DrawRoundRect(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_rx, int ff_ry)
		{
			DrawPath(GraphicsPathFP.CreateRoundRect(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_rx, ff_ry));
		}
		public void  DrawRect(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			DrawPath(GraphicsPathFP.CreateRect(ff_xmin, ff_ymin, ff_xmax, ff_ymax));
		}
		public void  DrawOval(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			DrawPath(GraphicsPathFP.CreateOval(ff_xmin, ff_ymin, ff_xmax, ff_ymax));
		}
		public void  DrawArc (int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_startangle, int ff_sweepangle)
		{
			DrawPath(GraphicsPathFP.CreateArc(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_startangle, ff_sweepangle, false));
		}
		public void  DrawPie (int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_startangle, int ff_sweepangle)
		{
			DrawPath(GraphicsPathFP.CreateArc(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_startangle, ff_sweepangle, true));
		}
		public void  DrawPath(GraphicsPathFP path)
		{
			bufferedImage = null;
			renderer.DrawPath(path.CalcOutline(lineStyle), matrix, lineStyle.Brush, GraphicsPathRenderer.MODE_ZERO);
		}
		public void  FillClosedCurves(PointFP[] points, int offset, int numberOfSegments, int ff_factor)
		{
			FillPath(GraphicsPathFP.CreateSmoothCurves(points, offset, numberOfSegments, ff_factor, true));
		}
		public void  FillPolygon(PointFP[] points)
		{
			FillPath(GraphicsPathFP.CreatePolygon(points));
		}
		public void  FillRoundRect(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_rx, int ff_ry)
		{
			FillPath(GraphicsPathFP.CreateRoundRect(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_rx, ff_ry));
		}
		public void  FillRect(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			GraphicsPathFP path = GraphicsPathFP.CreateRect(ff_xmin, ff_ymin, ff_xmax, ff_ymax);
			path.AddClose();
			FillPath(path);
		}
		public void  FillOval(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			GraphicsPathFP path = GraphicsPathFP.CreateOval(ff_xmin, ff_ymin, ff_xmax, ff_ymax);
			path.AddClose();
			FillPath(path);
		}
		public void  FillPie (int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax, int ff_startangle, int ff_sweepangle)
		{
			FillPath(GraphicsPathFP.CreateArc(ff_xmin, ff_ymin, ff_xmax, ff_ymax, ff_startangle, ff_sweepangle, true));
		}
		public void  FillPath(GraphicsPathFP path)
		{
			bufferedImage = null;
			renderer.DrawPath(path, matrix, fillStyle, paintMode);
		}
		public void  FinalizeBuffer(int color)
		{
			bufferedImage = null;
			renderer.FinalizeBuffer(color);
		}
	}
}