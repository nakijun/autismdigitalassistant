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
namespace XrossOne.FixedPoint
{
	public class SingleFP
	{
		public const int Epsilon = 1;
		public const int PositiveInfinity = Int32.MaxValue;
		public const int NegativeInfinity = Int32.MinValue;
		public const int MaxValue = PositiveInfinity - 1;
		public const int MinValue = NegativeInfinity + 2;
		public const int NaN = NegativeInfinity + 1;
		public const int DecimalBits = 16;
		public const int One = 1 << DecimalBits;
		
		public int Value;
		
		public SingleFP(int v)
		{
			Value = v;
		}
		public SingleFP(SingleFP f)
		{
			Value = f.Value;
		}
		public static bool IsNaN(int x)
		{
			return x == NaN;
		}
		public static bool IsInfinity(int x)
		{
			return x == NegativeInfinity || x == PositiveInfinity;
		}
		public static bool IsNegativeInfinity(int x)
		{
			return x == NegativeInfinity;
		}
		public static bool IsPositiveInfinity(int x)
		{
			return x == PositiveInfinity;
		}
		public static int FromFloat(float f)
		{
			return (int)(f * One);
		}
		public static float ToFloat(int x)
		{
			return (float)x / One;
		}
		public static int FromInt(int x)
		{
			return x << DecimalBits;
		}
		public static int ToInt(int ff_x)
		{
			return ff_x >> DecimalBits;
		}
		public static SingleFP Parse(System.String strValue)
		{
			System.String s = strValue;
			bool e_neg = false;
			int v, e = 0;
			;
			
			int posE = s.IndexOf((System.Char) 'E');
			if (posE == - 1)
				posE = s.IndexOf((System.Char) 'e');
			if (posE != - 1)
			{
				e = System.Int32.Parse(s.Substring(posE + 1));
				if (e < 0)
				{
					e_neg = true;
					e = - e;
				}
				s = s.Substring(0, (posE) - (0));
			}
			int posDot = s.IndexOf((System.Char) '.');
			if (posDot == - 1)
			{
				v = System.Int32.Parse(s);
				v = v << DecimalBits;
			}
			else
			{
				v = System.Int32.Parse(s.Substring(0, (posDot) - (0))) << DecimalBits;
				s = s.Substring(posDot + 1);
				s = s + "0000";
				s = s.Substring(0, (4) - (0));
				int f = System.Int32.Parse(s);
				f = (f << DecimalBits) / 10000;
				if (v < 0)
					v -= f;
				else
					v += f;
			}
			for (int i = 0; i < e; i++)
				if (e_neg)
					v /= 10;
				else
					v *= 10;
			return new SingleFP(v);
		}
		public override System.String ToString()
		{
			System.String s = "";
			int v = Value;
			if (v < 0)
			{
				s = "-";
				v = - v;
			}
			s = s + System.Convert.ToString(v >> DecimalBits);
			v = 0xFFFF & v;
			if (v != 0)
				s = s + ".";
			//while (v != 0)
			for (int i = 0; i < 4; i++)
			{
				v = v * 10;
				s = s + System.Convert.ToString(v >> DecimalBits);
				v = 0xFFFF & v;
			}
			return s;
		}
	}
}