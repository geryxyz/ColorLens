using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

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
			KeyDown +=
				( _s, _a ) =>
				{
					if ( _a.Key == Key.LeftCtrl ||
					     _a.Key == Key.RightCtrl )
					{
						isMoving = false;
						helpDisplayer.Visibility = Visibility.Visible;
					}
					if ( _a.Key == Key.Escape )
					{
						Close( );
					}
					if ( _a.Key == Key.R )
					{
						Clipboard.SetText( color.R.ToString( ) );
						ColorAnimation _fade =
							new ColorAnimation( Colors.Transparent, Colors.Red, new Duration( TimeSpan.FromSeconds( .25 ) ) )
							{
								AutoReverse = true,
							};
						rgbBrush.BeginAnimation( SolidColorBrush.ColorProperty, _fade );
					}
					if ( _a.Key == Key.G )
					{
						Clipboard.SetText( color.G.ToString( ) );
						ColorAnimation _fade =
							new ColorAnimation( Colors.Transparent, Colors.Green, new Duration( TimeSpan.FromSeconds(.25 ) ) )
							{
								AutoReverse = true
							};
						rgbBrush.BeginAnimation( SolidColorBrush.ColorProperty, _fade );
					}
					if ( _a.Key == Key.B )
					{
						Clipboard.SetText( color.B.ToString( ) );
						ColorAnimation _fade =
							new ColorAnimation( Colors.Transparent, Colors.Blue, new Duration( TimeSpan.FromSeconds( .25 ) ) )
							{
								AutoReverse = true
							};
						rgbBrush.BeginAnimation( SolidColorBrush.ColorProperty, _fade );
					}
					if ( _a.Key == Key.H )
					{
						Clipboard.SetText( ColorTranslator.ToHtml( color ) );
						ColorAnimation _fade =
							new ColorAnimation( Colors.Transparent, Colors.Black, new Duration( TimeSpan.FromSeconds( .25 ) ) )
							{
								AutoReverse = true
							};
						htmlBrush.BeginAnimation( SolidColorBrush.ColorProperty, _fade );
					}					
				};
			KeyUp += 
				( _s, _a ) =>
				{
					if ( _a.Key == Key.LeftCtrl ||
					     _a.Key == Key.RightCtrl )
					{
						isMoving = true;
						helpDisplayer.Visibility = Visibility.Collapsed;
					}
				};
		}

		bool isMoving = true;
		System.Drawing.Color color;

		void MainWindow_OnLoaded( object sender, RoutedEventArgs e )
		{
			new TaskFactory( ).StartNew(
				( ) =>
				{
					while ( true )
					{
						if ( isMoving )
						{
							var _position = Win32.CursorPosition;
							color = Win32.GetPixelColor( _position.X, _position.Y );
							Dispatcher.Invoke(
								( ) =>
								{
									rgbDisplay.Text = string.Format( "{0}; {1}; {2};", color.R, color.G, color.B );
									htmlDisplay.Text = ColorTranslator.ToHtml( color );
									colorDisplay.Fill = new SolidColorBrush( Color.FromRgb( color.R, color.G, color.B ) );
									Top = _position.Y - ( ActualHeight + 10 );
									Left = _position.X + 10;
								} );
						}
						Thread.Sleep( 10 );
					}
				} );
		}
	}
}
