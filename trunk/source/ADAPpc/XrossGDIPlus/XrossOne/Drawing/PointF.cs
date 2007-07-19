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

namespace XrossOne.Drawing
{
	public struct PointF
	{
		private float cx, cy;

		public static readonly PointF Empty;

		public static PointF operator + (PointF pt, Size sz)
		{
			return new PointF (pt.X + sz.Width, pt.Y + sz.Height);
		}

		public static bool operator == (PointF pt_a, PointF pt_b)
		{
			return ((pt_a.X == pt_b.X) && (pt_a.Y == pt_b.Y));
		}

		public static bool operator != (PointF pt_a, PointF pt_b)
		{
			return ((pt_a.X != pt_b.X) || (pt_a.Y != pt_b.Y));
		}

		public static PointF operator - (PointF pt, Size sz)
		{
			return new PointF (pt.X - sz.Width, pt.Y - sz.Height);
		}
		
		public PointF (float x, float y)
		{
			cx = x;
			cy = y;
		}

		public bool IsEmpty 
		{
			get 
			{
				return ((cx == 0.0) && (cy == 0.0));
			}
		}
		
		public float X 
		{
			get 
			{
				return cx;
			}
			set 
			{
				cx = value;
			}
		}

		public float Y 
		{
			get 
			{
				return cy;
			}
			set 
			{
				cy = value;
			}
		}

		public override bool Equals (object o)
		{
			if (!(o is PointF))
				return false;

			return (this == (PointF) o);
		}

		public override int GetHashCode ()
		{
			return (int) cx ^ (int) cy;
		}

		public override string ToString ()
		{
			return String.Format ("{{X={0}, Y={1}}}", cx, cy);
		}
	}
}
