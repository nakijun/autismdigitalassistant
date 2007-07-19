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
	sealed class GraphicsPathOutline:GraphicsPathSketch
	{
		private GraphicsPathFP path;
		private int ff_rad;
		private int startLineCap;
		private int endLineCap;
		private int lineJoin;
		private bool needDrawStartCap = false;
		//private bool needDrawEndCap = false;
		private PointFP lastPoint = null;
		private LineFP lastCurveTail = null;
		private GraphicsPathFP curvePath1 = null;
		private GraphicsPathFP curvePath2 = null;
		private PointFP curveBegin = null;
		private bool drawingCurve = false;
		private GraphicsPathFP outline;
		private PenFP lineStyle;
		private PointFP startCapP1, startCapP2;
		bool closed = true;
		public GraphicsPathOutline(GraphicsPathFP pathFP, GraphicsPathFP outline, PenFP lineStyle):base()
		{
			this.path = pathFP;
			this.outline = outline;
			this.lineStyle = lineStyle;
			ff_rad = lineStyle.Width / 2;
			startLineCap = lineStyle.StartCap;
			endLineCap = lineStyle.EndCap;
			lineJoin = lineStyle.LineJoin;
		}
		
		private void FinishCurrentSegment()
		{
			if (closed) return;
			if (startCapP1 != null && startCapP2 != null)
				AddLineCap(startCapP2, startCapP1, startLineCap);
			if (lastPoint != null)
				AddLineCap(lastPoint, currPoint, endLineCap); 				
		}
		public override void  End()
		{
			FinishCurrentSegment();
		}
		
		public override void  MoveTo(PointFP point)
		{
			FinishCurrentSegment();
			needDrawStartCap = true;
			closed = false;
			startCapP1 = startCapP2 = null;
			base.MoveTo(point);
		}
		
		private void  AddLineCap(PointFP p1, PointFP p2, int lineCap)
		{
			if (lineCap == PenFP.LINECAP_BUTT || p1.Equals(p2))
				return ;
			int dx = p2.X - p1.X;
			int dy = p2.Y - p1.Y;
			int len = PointFP.Distance(dx, dy);
			PointFP[] cap = lineCap == PenFP.LINECAP_ROUND?GraphicsPathFP.roundCap:GraphicsPathFP.squareCap;
			
			dx = MathFP.Mul(ff_rad, MathFP.Div(dx, len));
			dy = MathFP.Mul(ff_rad, MathFP.Div(dy, len));
			
			MatrixFP m = new MatrixFP(dx, dx, dy, - dy, p2.X, p2.Y);
			outline.AddMoveTo(new PointFP(0, GraphicsPathFP.One).Transform(m));
			for (int i = 0; i < cap.Length; i++)
				outline.AddLineTo(new PointFP(cap[i]).Transform(m));
			outline.AddLineTo(new PointFP(0, - GraphicsPathFP.One).Transform(m));
			outline.AddClose();
		}
		
		private void  CalcHeadTail(PointFP p1, PointFP p2, LineFP head, LineFP tail)
		{
			LineFP curr = new LineFP(p1, p2);
			head.Reset(curr.GetHeadOutline(ff_rad));
			int dx = p2.X - p1.X;
			int dy = p2.Y - p1.Y;
			tail.Reset(head.P1.X + dx, head.P1.Y + dy, head.P2.X + dx, head.P2.Y + dy);
		}
		
		private void  AddLineJoin(PointFP lastPoint, PointFP currPoint, PointFP nextPoint)
		{
			if (lastPoint == null || currPoint == null || nextPoint == null || nextPoint.Equals(currPoint) || lastPoint.Equals(currPoint))
				return ;
			
			PointFP p1 = null, p2 = null;
			LineFP head, tail, lastHead, lastTail;
			CalcHeadTail(currPoint, nextPoint, head = new LineFP(), tail = new LineFP());
			CalcHeadTail(lastPoint, currPoint, lastHead = new LineFP(), lastTail = new LineFP());
			bool cross1, cross2, needLineJoin = false;
			PointFP pi1 = new PointFP();
			PointFP pi2 = new PointFP();
			
			cross1 = LineFP.Intersects(new LineFP(head.P1, tail.P1), new LineFP(lastHead.P1, lastTail.P1), pi1);
			cross2 = LineFP.Intersects(new LineFP(head.P2, tail.P2), new LineFP(lastHead.P2, lastTail.P2), pi2);
			if (cross1 && !cross2 && pi1.X != SingleFP.NaN)
			{
				p1 = lastTail.P2;
				p2 = head.P2;
				needLineJoin = true;
			}
			else if (!cross1 && cross2 && pi2.X != SingleFP.NaN)
			{
				p1 = lastTail.P1;
				p2 = head.P1;
				needLineJoin = true;
			}
			if (needLineJoin)
			{
				outline.AddMoveTo(cross1 ? pi1 : pi2);
				outline.AddLineTo(cross1 ? p2 : p1);
				if (lineJoin == PenFP.LINEJOIN_MITER)
				{
					outline.AddLineTo(cross1 ? pi2 : pi1);
				}
				outline.AddLineTo(cross1 ? p1 : p2);
				outline.AddClose();
				if (lineJoin == PenFP.LINEJOIN_ROUND)
				{
					AddLineCap(cross2 ? pi2 : pi1, currPoint, PenFP.LINECAP_ROUND);
				}
			}
		}
		
		private void  CurveBegin(PointFP control)
		{
			AddLineJoin(lastPoint, currPoint, control);
			drawingCurve = true;
			curvePath1 = new GraphicsPathFP();
			curvePath2 = new GraphicsPathFP();
			curveBegin = new PointFP(currPoint);
		}
		
		private void  CurveEnd(PointFP control1, PointFP control2, PointFP curveEnd)
		{
			drawingCurve = false;
			if (needDrawStartCap)
			{
				startCapP1 = new PointFP(curveBegin);
				startCapP2 = new PointFP(control1);
				//AddLineCap(control1, curveBegin, startLineCap);
				needDrawStartCap = false;
			}
			LineFP head = new LineFP();
			LineFP tail = new LineFP();
			CalcHeadTail(curveBegin, control1, head, new LineFP());
			outline.AddMoveTo(head.P1);
			outline.AddPath(curvePath1);
			CalcHeadTail(control2, curveEnd, new LineFP(), tail);
			outline.AddLineTo(tail.P1);
			outline.AddLineTo(tail.P2);
			outline.ExtendIfNeeded(curvePath1.cmdsSize, curvePath1.pntsSize);
			int j = curvePath2.pntsSize - 1;
			for (int i = curvePath2.cmdsSize - 1; i >= 0; i--)
			{
				outline.AddLineTo(curvePath2.pnts[j--]);
			}
			outline.AddLineTo(head.P2);
			outline.AddClose();
			curvePath1 = null;
			curvePath2 = null;
			lastCurveTail = null;
			lastPoint = new PointFP(control2);
			drawingCurve = false;
		}
		
		public override void  CurveTo(PointFP control, PointFP point)
		{
			CurveBegin(control);
			base.CurveTo(control, point);
			CurveEnd(control, control, point);
		}
		
		public override void  CurveTo(PointFP control1, PointFP control2, PointFP point)
		{
			CurveBegin(control1);
			base.CurveTo(control1, control2, point);
			CurveEnd(control1, control2, point);
		}
		public override void  Close()
		{
			closed = true;
			if (startCapP1 != null && startCapP2 != null && lastPoint != null && currPoint != null)
				AddLineJoin(startCapP1.Equals(currPoint) ? lastPoint : currPoint, startCapP1, startCapP2);
			LineTo(startPoint);
			started = false;
		}	

		public override void  LineTo(PointFP point)
		{
			if (point.Equals(currPoint))
				return ;
			
			LineFP head, tail;
			CalcHeadTail(currPoint, point, head = new LineFP(), tail = new LineFP());
			
			if (drawingCurve)
			{
				if (lastCurveTail != null)
				{
					curvePath1.AddLineTo(lastCurveTail.P1);
					curvePath2.AddLineTo(lastCurveTail.P2);
				}
				lastCurveTail = new LineFP(tail);
			}
			else
			{
				if (needDrawStartCap)
				{
					//AddLineCap(point, currPoint, startLineCap);
					startCapP1 = new PointFP(currPoint);
					startCapP2 = new PointFP(point);
					needDrawStartCap = false;
				}
				AddLineJoin(lastPoint, currPoint, point);
				
				outline.AddMoveTo(head.P1);
				outline.AddLineTo(tail.P1);
				outline.AddLineTo(tail.P2);
				outline.AddLineTo(head.P2);
				outline.AddLineTo(head.P1);
				outline.AddClose();
				lastPoint = new PointFP(currPoint);
			}
			base.LineTo(point);
		}
	}
}