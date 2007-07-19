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
using System.IO;
using System.Drawing;

namespace XrossOne.DrawingFP
{
	/// <summary>
	/// Summary description for BitmapBuffer.
	/// </summary>
	public class BitmapBuffer
	{
		private const int offset		= 54;
		private short BYTES				= 3;
		private byte[] buffer			= null;
		private int width				= 0;
		private int height				= 0;
		private int byteWidth			= 0;
		private MemoryStream stream		= null;

		public BitmapBuffer()
		{
			Init(0, 0);
		}
		public BitmapBuffer(int width, int height)
		{
			Init(width, height);
		}
		private void Init(int aWidth, int aHeight)
		{
			uint	biSize				= 40;			// Size of this structure
			int		biWidth				= aWidth;		// Width of bitmap (pixels)
			int		biHeight			= aHeight;		// Height of bitmap (pixels)
			short	biPlanes			= 1;			// Number of color planes
			short	biBitCount			= 24;			// Pixel bit depth
			uint	biCompression		= 0;			// Compression type
			uint	biSizeImage			= 0;			// Size of uncompressed image
			int		biXPelsPerMeter		= 0;			// Horizontal pixels per meter
			int		biYPelsPerMeter		= 0;			// Vertical pixels per meter
			uint	biClrUsed			= 0;			// Number of colors used
			uint	biClrImportant		= 0;			// Important colors
			ushort  reserved			= 0;

			width = aWidth;
			byteWidth = width * BYTES;
			int r = byteWidth % 4;
			if (r != 0) byteWidth += 4 - r;
			height = aHeight;

			uint buffersize = (uint)(offset + byteWidth * aHeight);
			buffer = new byte[buffersize];
			stream = new MemoryStream(buffer, true);
			BinaryWriter writer = new BinaryWriter(stream);

			writer.Write ('B');
			writer.Write ('M');
			writer.Write (buffersize);
			writer.Write (reserved);
			writer.Write (reserved);
			writer.Write (offset);
					
			writer.Write (biSize);
			writer.Write (biWidth);
			writer.Write (biHeight);
			writer.Write (biPlanes);
			writer.Write (biBitCount);
			writer.Write (biCompression);
			writer.Write (biSizeImage);
			writer.Write (biXPelsPerMeter);
			writer.Write (biYPelsPerMeter);
			writer.Write (biClrUsed);
			writer.Write (biClrImportant);
		}
		/// <summary>
		/// Pixel Width of the bitmap.
		/// </summary>
		public int Width  
		{
			get
			{
				return width; 
			} 
		}

		/// <summary>
		/// Pixel height of the bitmap.
		/// </summary>
		public int Height 
		{
			get
			{
				return height; 
			} 
		} 
		
		public void Resize(int aWidth, int aHeight)
		{
			Init(aWidth, aHeight);
		}

		public int this[int x, int y]
		{
			get
			{
				int index = offset + (height - y - 1) * byteWidth + x * BYTES;
				//int a = buffer[index++];
				int b = buffer[index++];
				int g = buffer[index++];
				int r = buffer[index];
				return r << 16 | g << 8 | b;
			}
			set
			{
				int index = offset + (height - y - 1) * byteWidth + x * BYTES;
				//buffer[index++] = (byte)((value >> 24) & 0xFF);
				buffer[index++] = (byte)(value & 0xFF);
				buffer[index++] = (byte)((value >> 8) & 0xFF);
				buffer[index] = (byte)((value >> 16) & 0xFF);
			}
		}
		public void Clear(int color)
		{
			/*
			for (int i = 0; i < width * height; i++)
			{
				int index = i;
				buffer[offset + index ++] = 0;
				buffer[offset + index ++] = 0;
				buffer[offset + index ++] = 0;
				buffer[offset + index ++] = 0;
			}*/
		}
		public Bitmap CreateBitmap()
		{
			return new Bitmap(stream);
		}
	}
}
