using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorLens
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow( )
		{
			InitializeComponent( );
		}

		void MainWindow_OnLoaded( object sender, RoutedEventArgs e )
		{
			new TaskFactory( ).StartNew(
				( ) =>
				{
					while ( true )
					{
						var _position = Win32.CursorPosition;
						var _color = Win32.GetPixelColor( _position.X, _position.Y );
						Dispatcher.Invoke(
							( ) =>
							{
								rgbDisplay.Text = string.Format( "{0}; {1}; {2};", _color.R, _color.G, _color.B );
								colorDisplay.Fill = new SolidColorBrush( Color.FromRgb( _color.R, _color.G, _color.B ) );
								Top = _position.Y + 10;
								Left = _position.X + 10;
							} );
						Thread.Sleep( 10 );
					}
				} );
		}
	}
}
