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
	public class RectangleFP
	{
		virtual public int Bottom
		{
			get
			{
				return ff_ymax;
			}
		}
		virtual public int Top
		{
			get
			{
				return ff_ymin;
			}
		}
		virtual public int Left
		{
			get
			{
				return ff_xmin;
			}
		}
		virtual public int Right
		{
			get
			{
				return ff_xmax;
			}
		}
		virtual public int Width
		{
			get
			{
				return ff_xmax - ff_xmin;
			}
			
			set
			{
				if (value < 0)
					return ;
				ff_xmax = ff_xmin + value;
			}
			
		}
		virtual public int Height
		{
			get
			{
				return ff_ymax - ff_ymin;
			}
			
			set
			{
				if (value < 0)
					return ;
				ff_ymax = ff_ymin + value;
			}
			
		}
		virtual public int X
		{
			get
			{
				return ff_xmin;
			}
			
			set
			{
				ff_xmin = value;
			}
			
		}
		virtual public int Y
		{
			get
			{
				return ff_ymin;
			}
			set
			{
				ff_ymin = value;
			}
		}
		public static readonly RectangleFP Empty = new RectangleFP();
		private int ff_xmin;
		private int ff_xmax;
		private int ff_ymin;
		private int ff_ymax;
		/// <summary>  </summary>
		public RectangleFP()
		{
			ff_xmin = ff_xmax = ff_ymin = ff_ymax = SingleFP.NaN;
		}
		public RectangleFP(RectangleFP r)
		{
			Reset(r);
		}
		public RectangleFP(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			Reset(ff_xmin, ff_ymin, ff_xmax, ff_ymax);
		}
		public virtual RectangleFP Reset(int ff_xmin, int ff_ymin, int ff_xmax, int ff_ymax)
		{
			this.ff_xmin = MathFP.Min(ff_xmin, ff_xmax);
			this.ff_xmax = MathFP.Max(ff_xmin, ff_xmax);
			this.ff_ymin = MathFP.Min(ff_ymin, ff_ymax);
			this.ff_ymax = MathFP.Max(ff_ymin, ff_ymax);
			return this;
		}
		public virtual RectangleFP Reset(RectangleFP r)
		{
			return Reset(r.ff_xmin, r.ff_ymin, r.ff_xmax, r.ff_ymax);
		}
		public virtual bool IsEmpty()
		{
			return ff_xmin == SingleFP.NaN || ff_xmax == SingleFP.NaN || ff_ymin == SingleFP.NaN || ff_ymax == SingleFP.NaN;
		}
		public virtual void  Offset(int ff_dx, int ff_dy)
		{
			if (!IsEmpty())
			{
				ff_xmin += ff_dx;
				ff_xmax += ff_dx;
				ff_ymin += ff_dy;
				ff_ymax += ff_dy;
			}
		}
		
		public virtual RectangleFP Union(RectangleFP r)
		{
			if (!r.IsEmpty())
			{
				if (IsEmpty())
					Reset(r);
				else
					Reset(MathFP.Min(ff_xmin, r.ff_xmin), MathFP.Max(ff_xmax, r.ff_xmax), MathFP.Min(ff_ymin, r.ff_ymin), MathFP.Max(ff_ymax, r.ff_ymax));
			}
			return this;
		}
		
		public virtual RectangleFP Union(PointFP p)
		{
			if (!IsEmpty())
			{
				Reset(MathFP.Min(ff_xmin, p.X), MathFP.Max(ff_xmax, p.X), MathFP.Min(ff_ymin, p.Y), MathFP.Max(ff_ymax, p.Y));
			}
			return this;
		}
		
		public virtual bool IntersectsWith(RectangleFP r)
		{
			return ff_xmin <= r.ff_xmax && r.ff_xmin <= ff_xmax && ff_ymin <= r.ff_ymax && r.ff_ymin <= ff_ymax;
		}
		
		public virtual bool Contains(PointFP p)
		{
			return ff_xmin <= p.X && p.X <= ff_xmax && ff_ymin <= p.Y && p.Y <= ff_ymax;
		}
		public  override bool Equals(System.Object o)
		{
			RectangleFP r = (RectangleFP) o;
			if (r == null)
				return false;
			else
				return r.ff_xmax == ff_xmax && r.ff_xmin == ff_xmin && r.ff_ymax == ff_ymax && r.ff_ymin == ff_ymin;
		}
		public override System.String ToString()
		{
			return "Rectangle" + " (" + new SingleFP(ff_xmin) + "," + new SingleFP(ff_ymin) + ")-(" + new SingleFP(ff_xmax) + "," + new SingleFP(ff_ymax) + ")";
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}