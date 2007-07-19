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
	public class MathFP
	{
		public const int PI = 205887; 
		public const int E = 178145;
		
		public static int Min(int x, int y)
		{
			return x < y?x:y;
		}
		
		public static int Max(int x, int y)
		{
			return x > y?x:y;
		}
		
		public static int Abs(int x)
		{
			return x < 0?- x:x;
		}
		
		public static int Mul(int x, int y)
		{
			long res = (long) x * (long) y >> SingleFP.DecimalBits;
			return (int) res;
		}
		
		public static int Div(int x, int y)
		{
			long res = ((long) x << SingleFP.DecimalBits) / (long) y;
			return (int) res;
		}
		
		public static int Sqrt(int n)
		{
			int s;
			if (n < (1000 << SingleFP.DecimalBits))
				s = n / 20;
			else if (n < (2500 << SingleFP.DecimalBits))
				s = n / 40;
			else if (n < (5000 << SingleFP.DecimalBits))
				s = n / 60;
			else if (n < (10000 << SingleFP.DecimalBits))
				s = n / 86;
			else if (n < (25000 << SingleFP.DecimalBits))
				s = n / 132;
			else
				s = n / 168;
			
			s = (s + Div(n, s)) >> 1;
			s = (s + Div(n, s)) >> 1;
			s = (s + Div(n, s)) >> 1;
			return s;
		}
		
		public static int IEEERemainder(int n, int m)
		{
			return n - Mul(Floor(Div(n, m)), m);
		}
		
		public static int Floor(int x)
		{
			return x < 0 && (- x & 0xFFFF) != 0?- ((- x + SingleFP.One >> SingleFP.DecimalBits) << SingleFP.DecimalBits):((x >> SingleFP.DecimalBits) << SingleFP.DecimalBits);
		}
		
		public static int Round(int x)
		{
			if (x < 0)
				return - (((- x + SingleFP.One / 2) >> SingleFP.DecimalBits) << SingleFP.DecimalBits);
			else
				return ((x + SingleFP.One / 2) >> SingleFP.DecimalBits) << SingleFP.DecimalBits;
		}
		
		public static int ToDegrees(int f)
		{
			return Div(f * 180, PI);
		}
		public static int ToRadians(int f)
		{
			return Mul(f, PI) / 180;
		}
		
		public static int Sin(int f)
		{
			if (f < 0 || f >= PI * 2)
				f = IEEERemainder(f, PI * 2);
			int sign = 1;
			if ((f > PI / 2) && (f <= PI))
			{
				f = PI - f;
			}
			else if ((f > PI) && (f <= (PI + PI / 2)))
			{
				f = f - PI;
				sign = - 1;
			}
			else if (f > (PI + PI / 2))
			{
				f = (PI << 1) - f;
				sign = - 1;
			}
			
			int sqr = Mul(f, f);
			int result = 498;
			result = Mul(result, sqr);
			result -= 10882;
			result = Mul(result, sqr);
			result += (1 << SingleFP.DecimalBits);
			result = Mul(result, f);
			return sign * result;
		}
		
		public static int Cos(int ff_ang)
		{
			return Sin(PI / 2 - ff_ang);
		}
		
		public static int Tan(int f)
		{
			return Div(Sin(f), Cos(f));
		}
		
		public static int Atan(int ff_val)
		{
			int ff_val1 = ff_val > SingleFP.One?Div(SingleFP.One,ff_val):(ff_val < - SingleFP.One?Div(SingleFP.One, - ff_val):ff_val);
			int sqr = Mul(ff_val1, ff_val1);
			int result = 1365;
			result = Mul(result, sqr);
			result -= 5579;
			result = Mul(result, sqr);
			result += 11805;
			result = Mul(result, sqr);
			result -= 21646;
			result = Mul(result, sqr);
			result += 65527;
			result = Mul(result, ff_val1);
			return ff_val > SingleFP.One?PI / 2 - result:(ff_val < - SingleFP.One?- (PI / 2 - result):result);
		}
		
		public static int Asin(int f)
		{
			return PI / 2 - Acos(f);
		}
		
		public static int Acos(int f)
		{
			int fRoot = Sqrt(SingleFP.One - f);
			int result = - 1228;
			result = Mul(result, f);
			result += 4866;
			result = Mul(result, f);
			result -= 13901;
			result = Mul(result, f);
			result += 102939;
			result = Mul(fRoot, result);
			return result;
		}
		
		public static long Min(long x, long y)
		{
			return x < y?x:y;
		}
		
		public static long Max(long x, long y)
		{
			return x > y?x:y;
		}
		public static long Abs(long x)
		{
			return x < 0?- x:x;
		}
	}
}