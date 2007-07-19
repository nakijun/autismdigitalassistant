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
	public class GradientBrushX : BrushX
	{
		RectangleF rectangle;
		float angle;
		//Color color1, color2;
		bool gammaCorrection;
		GradientBrushFP brushFP;
		BlendX blend;
		ColorBlendX colorBlend;
		Color [] linearColors;
		MatrixX matrix;
		WrapModeX wrapMode;

		protected virtual int GradientType
		{
			get
			{
				return GradientBrushFP.LINEAR_GRADIENT;
			}
		}
		public override BrushFP WrappedBrush
		{
			get
			{
				if (brushFP == null)
				{
					RectangleFP r = Utils.ToRectangleFP(rectangle);
					brushFP = new GradientBrushFP(r.Left, r.Top, r.Right, r.Bottom, 
						MathFP.ToRadians(SingleFP.FromFloat(angle)),GradientType);
					
					/*int length = PointFP.Distance(r.Width, r.Height);
					int ang = MathFP.ToRadians(SingleFP.FromFloat(
						(float)Math.Atan2(rectangle.Height, rectangle.Width) - angle));
					length = MathFP.Mul(length, MathFP.Cos(ang));*/
					float[] positions = InterpolationColors.Positions;
					Color[] colors = InterpolationColors.Colors;
					for (int i = 0; i < colors.Length; i++)
					{
						brushFP.SetGradientColor(SingleFP.FromFloat(positions[i]), colors[i].ToArgb());
					}
					//brushFP.SetGradientColor_0_1(0, color1.ToArgb());
					//brushFP.SetGradientColor_0_1(SingleFP.One, color2.ToArgb());
					//int s = MathFP.Div(SingleFP.One, MathFP.Cos(ang));
					//brushFP.matrix.Scale(s, s);
					brushFP.UpdateGradientTable();
				}
				return brushFP;
			}
		}
		public void Init(RectangleF rect, Color color1, Color color2, float angle)
		{
			rectangle = rect;
			//this.color1 = color1;
			//this.color2 = color2;
			this.InterpolationColors.Positions = new float[]{0F, 1F};
			this.InterpolationColors.Colors = new Color[]{color1, color2};
			this.angle = angle;
			brushFP = null;
		}
		public GradientBrushX (Point point1, Point point2, Color color1, Color color2):
			this(new PointF(point1.X, point1.Y), new PointF(point2.X, point2.Y), color1, color2)
		{
		}

		public GradientBrushX (PointF point1, PointF point2, Color color1, Color color2)
		{
			float dx = point2.X - point1.X;
			float dy = point2.Y - point2.Y;
			Init(new RectangleF(point1.X, point1.Y, (float)Math.Sqrt(dx * dx + dy * dy), 1), color1, color2, (float)Math.Atan2(dy, dx));
		}

		public GradientBrushX (Rectangle rect, Color color1, Color color2):
			this(rect, color1, color2, 0F)
		{
		}

		public GradientBrushX (Rectangle rect, Color color1, Color color2, float angle) 
		{
			Init(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height), color1, color2, angle);
		}

		public GradientBrushX (RectangleF rect, Color color1, Color color2):
			this(rect, color1, color2, 0F)
		{
		}

		public GradientBrushX (RectangleF rect, Color color1, Color color2, float angle)
		{
			Init(rect, color1, color2, angle);
		}

		public BlendX Blend 
		{
			get 
			{
				return blend;
			}
			set 
			{
				blend = value;
			}
		}

		public bool GammaCorrection 
		{
			get 
			{
				return gammaCorrection;
			}
			set 
			{
				gammaCorrection = value;
			}
		}

		public ColorBlendX InterpolationColors 
		{
			get 
			{
				if (colorBlend == null) colorBlend = new ColorBlendX();
				return colorBlend;
			}
			set 
			{
				colorBlend = value;
			}
		}
		
		public Color [] LinearColors 
		{
			get 
			{
				return linearColors;
			}
			set 
			{
				linearColors = value;
			}
		}

		public RectangleF Rectangle 
		{
			get 
			{
				return rectangle;
			}
		}

		public MatrixX Transform 
		{
			get 
			{
				return matrix;
			}
			set 
			{
				matrix = value;
			}
		}

		public WrapModeX WrapMode 
		{
			get 
			{
				return wrapMode;
			}
			set 
			{
				wrapMode = value;
			}
		}

		public void MultiplyTransform (MatrixX matrix)
		{
			MultiplyTransform (matrix, MatrixOrderX.Prepend);
		}

		public void MultiplyTransform (MatrixX matrix, MatrixOrderX order)
		{
			this.matrix.Multiply(matrix, order);
		}

		public void ResetTransform ()
		{
			matrix.Reset();
		}

		public void RotateTransform (float angle)
		{
			matrix.Rotate (angle, MatrixOrderX.Prepend);
		}

		public void RotateTransform (float angle, MatrixOrderX order)
		{
			matrix.Rotate(angle, order);
		}

		public void ScaleTransform (float sx, float sy)
		{
			ScaleTransform (sx, sy, MatrixOrderX.Prepend);
		}

		public void ScaleTransform (float sx, float sy, MatrixOrderX order)
		{
			matrix.Scale(sx, sy, order);
		}

		public void SetBlendTriangularShape (float focus)
		{
			SetBlendTriangularShape (focus, 1.0F);
		}

		public void SetBlendTriangularShape (float focus, float scale)
		{

		}

		public void SetSigmaBellShape (float focus)
		{
			SetSigmaBellShape (focus, 1.0F);
		}

		public void SetSigmaBellShape (float focus, float scale)
		{
		}

		public void TranslateTransform (float dx, float dy)
		{
			TranslateTransform (dx, dy, MatrixOrderX.Prepend);
		}

		public void TranslateTransform (float dx, float dy, MatrixOrderX order)
		{
			matrix.Translate(dx, dy, order);
		}
	}
}
