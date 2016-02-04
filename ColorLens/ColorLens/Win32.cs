using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ColorLens
{
	internal sealed class Win32
	{
		[ DllImport( "user32.dll" ) ]
		static extern IntPtr GetDC( IntPtr hwnd );

		[ DllImport( "user32.dll" ) ]
		static extern Int32 ReleaseDC( IntPtr hwnd, IntPtr hdc );

		[ DllImport( "gdi32.dll" ) ]
		static extern uint GetPixel( IntPtr hdc, int nXPos, int nYPos );

		public static System.Drawing.Color GetPixelColor( int x, int y )
		{
			IntPtr hdc = GetDC( IntPtr.Zero );
			uint pixel = GetPixel( hdc, x, y );
			ReleaseDC( IntPtr.Zero, hdc );
			Color color = Color.FromArgb(
				( int )( pixel & 0x000000FF ),
				( int )( pixel & 0x0000FF00 ) >> 8,
				( int )( pixel & 0x00FF0000 ) >> 16 );
			return color;
		}

		[ StructLayout( LayoutKind.Sequential ) ]
		public struct POINT
		{
			public int X;
			public int Y;

			public static implicit operator Point( POINT point )
			{
				return new Point( point.X, point.Y );
			}
		}

		[ DllImport( "user32.dll" ) ]
		public static extern bool GetCursorPos( out POINT lpPoint );

		public static Point CursorPosition
		{
			get
			{
				POINT lpPoint;
				GetCursorPos( out lpPoint );
				return lpPoint;
			}
		}
	}
}