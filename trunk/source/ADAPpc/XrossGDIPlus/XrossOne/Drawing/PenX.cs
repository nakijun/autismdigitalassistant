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
using System.Drawing.Drawing2D;
using XrossOne.DrawingFP;
using XrossOne.FixedPoint;

namespace XrossOne.Drawing
{
	/// <summary>
	/// Summary description for PenX.
	/// </summary>
	public class PenX
	{
		private PenFP pen;
		private LineCapX startCap;
		private LineCapX endCap;
		private LineJoinX lineJoin;

		public PenX (BrushX brush) : this (brush, 1.0F)
		{
		}

		public PenX (Color color) : this (color, 1.0F)
		{
		}

		public PenX (BrushX brush, float width)
		{
			pen = new PenFP(brush.WrappedBrush, SingleFP.FromFloat(width), PenFP.LINEJOIN_MITER, PenFP.LINEJOIN_MITER);
		}

		public PenX (Color color, float width)
		{
			pen = new PenFP(color.ToArgb(), SingleFP.FromFloat(width), PenFP.LINEJOIN_MITER, PenFP.LINEJOIN_MITER);
		}

		private PenX(PenFP aPen)
		{
			pen = aPen;
		}
		public static PenX FromPenFP(PenFP pen)
		{
			return new PenX(pen);
		}
		public static PenFP ToPenFP(PenX pen)
		{
			return pen.pen;
		}
		public BrushX Brush 
		{
			get 
			{
				return null;
				//return BrushX.FromBrushFP(pen.Brush);
			}
			set 
			{
				pen.Brush = value.WrappedBrush;
			}
		}

		public Color Color 
		{
			get 
			{
				if (pen.Brush is SolidBrushFP)
					return Color.FromArgb(((SolidBrushFP)pen.Brush).color);
				else
					return Color.FromArgb(0);
			}
			set 
			{
				if (pen.Brush is SolidBrushFP)
					((SolidBrushFP)pen.Brush).color = value.ToArgb();
			}
		}

		public LineCapX StartCap 
		{
			get 
			{
				return startCap;
			}
			set 
			{
				startCap = value;
				switch (value)
				{
					case LineCapX.Flat: 
						pen.StartCap = PenFP.LINECAP_BUTT;
						break;
					case LineCapX.Round:
						pen.StartCap = PenFP.LINECAP_ROUND;
						break;
					case LineCapX.Square:
						pen.StartCap = PenFP.LINECAP_SQUARE;
						break;
				}
			}
		}
 
		public LineCapX EndCap 
		{
			get 
			{
				return endCap;
			}
			set 
			{
				endCap = value;
				switch (value)
				{
					case LineCapX.Flat: 
						pen.EndCap = PenFP.LINECAP_BUTT;
						break;
					case LineCapX.Round:
						pen.EndCap = PenFP.LINECAP_ROUND;
						break;
					case LineCapX.Square:
						pen.EndCap = PenFP.LINECAP_SQUARE;
						break;
				}
			}
		}
 
		public LineJoinX LineJoin
		{

			get 
			{
				return lineJoin;
			}
			set 
			{
				lineJoin = value;
				switch (value)
				{
					case LineJoinX.Bevel:
						pen.LineJoin = PenFP.LINEJOIN_BEVEL;
						break;
					case LineJoinX.Miter:
						pen.LineJoin = PenFP.LINEJOIN_MITER;
						break;
					case LineJoinX.Round:
						pen.LineJoin = PenFP.LINEJOIN_ROUND;
						break;
				}
			}
                                
		}

		public PenTypeX PenType 
		{
			get 
			{
				if (Brush is SolidBrushX)
					return PenTypeX.SolidColor;
				else
					return PenTypeX.LinearGradient;
			}
		}

		public MatrixFP Transform 
		{

			get 
			{
				return pen.Brush.Matrix;
			}
			set 
			{
				pen.Brush.Matrix = value;
			}
		}
		public float Width 
		{
			get 
			{
				return SingleFP.ToFloat(pen.Width);
			}
			set 
			{
				pen.Width = SingleFP.FromFloat(value);
			}
		}

		public void SetLineCap (LineCapX startCap, LineCapX endCap)
		{
			StartCap = startCap;
			EndCap = endCap;
		}
	}	
}
