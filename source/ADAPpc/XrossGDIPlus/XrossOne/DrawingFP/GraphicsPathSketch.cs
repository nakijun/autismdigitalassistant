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
namespace XrossOne.DrawingFP
{
	public class GraphicsPathSketch : GraphicsPathIterator
	{
		virtual public PointFP CurrentPoint
		{
			get
			{
				return currPoint;
			}
		}
		virtual public PointFP StartPoint
		{
			get
			{
				return startPoint;
			}
		}
		protected const int SUBDIVIDE = 24;
		protected const int SUBDIVIDE2 = SUBDIVIDE * SUBDIVIDE;
		protected const int SUBDIVIDE3 = SUBDIVIDE2 * SUBDIVIDE;
		
		protected PointFP startPoint = new PointFP();
		protected PointFP currPoint = new PointFP();
		protected bool started;
		
		public GraphicsPathSketch()
		{
		}
		
		public virtual void  Begin()
		{
			started = false;
		}
		
		public virtual void  End()
		{
		}
		
		public virtual void  MoveTo(PointFP point)
		{
			if (!started)
			{
				startPoint.Reset(point);
				started = true;
			}
			currPoint.Reset(point);
		}
		
		public virtual void  LineTo(PointFP point)
		{
			currPoint.Reset(point);
		}
		
		public virtual void  CurveTo(PointFP control, PointFP point)
		{
			// Compute forward difference values for a quadratic
			// curve of type A*(1-t)^2 + 2*B*t*(1-t) + C*t^2
			
			PointFP f = new PointFP(currPoint);
			PointFP tmp = new PointFP((currPoint.X - control.X * 2 + point.X) / SUBDIVIDE2, (currPoint.Y - control.Y * 2 + point.Y) / SUBDIVIDE2);
			PointFP ddf = new PointFP(tmp.X * 2, tmp.Y * 2);
			PointFP df = new PointFP(tmp.X + (control.X - currPoint.X) * 2 / SUBDIVIDE, tmp.Y + (control.Y - currPoint.Y) * 2 / SUBDIVIDE);
			
			for (int c = 0; c < SUBDIVIDE - 1; c++)
			{
				f.Add(df); df.Add(ddf); LineTo(f);
			}
			
			// We specify the last point manually since
			// we obtain rounding errors during the
			// forward difference computation.
			LineTo(point);
		}
		
		public virtual void  CurveTo(PointFP control1, PointFP control2, PointFP point)
		{
			PointFP tmp1 = new PointFP(currPoint.X - control1.X * 2 + control2.X, currPoint.Y - control1.Y * 2 + control2.Y);
			PointFP tmp2 = new PointFP((control1.X - control2.X) * 3 - currPoint.X + point.X, (control1.Y - control2.Y) * 3 - currPoint.Y + point.Y);
			
			PointFP f = new PointFP(currPoint);
			PointFP df = new PointFP((control1.X - currPoint.X) * 3 / SUBDIVIDE + tmp1.X * 3 / SUBDIVIDE2 + tmp2.X / SUBDIVIDE3, (control1.Y - currPoint.Y) * 3 / SUBDIVIDE + tmp1.Y * 3 / SUBDIVIDE2 + tmp2.Y / SUBDIVIDE3);
			PointFP ddf = new PointFP(tmp1.X * 6 / SUBDIVIDE2 + tmp2.X * 6 / SUBDIVIDE3, tmp1.Y * 6 / SUBDIVIDE2 + tmp2.Y * 6 / SUBDIVIDE3);
			PointFP dddf = new PointFP(tmp2.X * 6 / SUBDIVIDE3, tmp2.Y * 6 / SUBDIVIDE3);
			
			for (int c = 0; c < SUBDIVIDE - 1; c++)
			{
				f.Add(df); df.Add(ddf); ddf.Add(dddf); LineTo(f);
			}
			
			LineTo(point);
		}
		
		public virtual void  Close()
		{
			// Connect start point with end point
			LineTo(startPoint);
			started = false;
		}
	}
}