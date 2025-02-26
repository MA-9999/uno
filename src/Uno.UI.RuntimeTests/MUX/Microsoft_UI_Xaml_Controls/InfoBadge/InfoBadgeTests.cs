﻿// MUX Reference: InfoBadgeTests.cs, commit 76bd573a73595ac66e8ff5ce755537d19f50a96d

using System;
using Common;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.AnimatedVisuals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MUXControlsTestApp.Utilities;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Symbol = Windows.UI.Xaml.Controls.Symbol;

namespace Windows.UI.Xaml.Tests.MUXControls.ApiTests
{
	[TestClass]
	[Uno.UI.RuntimeTests.RunsOnUIThread]
	public class InfoBadgeTests : MUXApiTestBase
	{
		[TestMethod]
		public void InfoBadgeDisplayKindTest()
		{
			InfoBadge infoBadge = null;
			SymbolIconSource symbolIconSource = null;
			RunOnUIThread.Execute(() =>
			{
				infoBadge = new InfoBadge();
				symbolIconSource = new SymbolIconSource();
				symbolIconSource.Symbol = Symbol.Setting;

				Content = infoBadge;
				Content.UpdateLayout();
			});

			IdleSynchronizer.Wait();

			RunOnUIThread.Execute(() =>
			{
				FrameworkElement textBlock = (FrameworkElement)infoBadge.FindVisualChildByName("ValueTextBlock");
				Verify.IsNotNull(textBlock, "The underlying value text block could not be retrieved");

				FrameworkElement iconViewBox = (FrameworkElement)infoBadge.FindVisualChildByName("IconPresenter");
				Verify.IsNotNull(textBlock, "The underlying icon presenter view box could not be retrieved");

				Verify.AreEqual(Visibility.Collapsed, textBlock.Visibility, "The value text block should be initally collapsed since the default value is -1");
				Verify.AreEqual(Visibility.Collapsed, iconViewBox.Visibility, "The icon presenter should be initally collapsed since the default value is null");

				infoBadge.IconSource = symbolIconSource;
				Content.UpdateLayout();

				Verify.AreEqual(Visibility.Collapsed, textBlock.Visibility, "The value text block should be initally collapsed since the default value is -1");
				Verify.AreEqual(Visibility.Visible, iconViewBox.Visibility, "The icon presenter should be visible since we've set the icon source property and value is -1");

				infoBadge.Value = 10;
				Content.UpdateLayout();

				Verify.AreEqual(Visibility.Visible, textBlock.Visibility, "The value text block should be visible since the value is set to 10");
				Verify.AreEqual(Visibility.Collapsed, iconViewBox.Visibility, "The icon presenter should be collapsed since we've set the icon source property but value is not -1");

				infoBadge.IconSource = null;
				Content.UpdateLayout();

				Verify.AreEqual(Visibility.Visible, textBlock.Visibility, "The value text block should be visible since the value is set to 10");
				Verify.AreEqual(Visibility.Collapsed, iconViewBox.Visibility, "The icon presenter should be collapsed since the icon source property is null");

				infoBadge.Value = -1;
				Content.UpdateLayout();

				Verify.AreEqual(Visibility.Collapsed, textBlock.Visibility, "The value text block should be collapsed since the value is set to -1");
				Verify.AreEqual(Visibility.Collapsed, iconViewBox.Visibility, "The icon presenter should be collapsed since the value is set to null");
			});
		}

		[TestMethod]
		public void InfoBadgeSupportsAllIconTypes()
		{
			InfoBadge infoBadge = null;
			SymbolIconSource symbolIconSource = null;
			PathIconSource pathIconSource = null;
			AnimatedIconSource animatedIconSource = null;
			BitmapIconSource bitmapIconSource = null;
			ImageIconSource imageIconSource = null;
			FontIconSource fontIconSource = null;

			RunOnUIThread.Execute(() =>
			{
				infoBadge = new InfoBadge();
				symbolIconSource = new SymbolIconSource();
				symbolIconSource.Symbol = Symbol.Setting;

				fontIconSource = new FontIconSource();
				fontIconSource.Glyph = "99+";
				fontIconSource.FontFamily = new FontFamily("XamlAutoFontFamily");

				bitmapIconSource = new BitmapIconSource();
				bitmapIconSource.ShowAsMonochrome = false;
				Uri bitmapUri = new Uri("ms-appx:/Assets/ingredient1.png");
				bitmapIconSource.UriSource = bitmapUri;

				imageIconSource = new ImageIconSource();
				var imageUri = new Uri("https://raw.githubusercontent.com/DiemenDesign/LibreICONS/master/svg-color/libre-camera-panorama.svg");
				imageIconSource.ImageSource = new SvgImageSource(imageUri);

				pathIconSource = new PathIconSource();
				var geometry = new RectangleGeometry();
				geometry.Rect = new Windows.Foundation.Rect { Width = 5, Height = 2, X = 0, Y = 0 };
				pathIconSource.Data = geometry;

				animatedIconSource = new AnimatedIconSource();
				animatedIconSource.Source = new AnimatedSettingsVisualSource();

				Content = infoBadge;
				Content.UpdateLayout();
			});

			IdleSynchronizer.Wait();

			RunOnUIThread.Execute(() =>
			{
				Log.Comment("Switch to Symbol Icon");
				infoBadge.IconSource = symbolIconSource;
				Content.UpdateLayout();

				Log.Comment("Switch to Path Icon");
				infoBadge.IconSource = pathIconSource;
				Content.UpdateLayout();

				Log.Comment("Switch to Font Icon");
				infoBadge.IconSource = fontIconSource;
				Content.UpdateLayout();

				Log.Comment("Switch to bitmap Icon");
				infoBadge.IconSource = bitmapIconSource;
				Content.UpdateLayout();

				Log.Comment("Switch to Image Icon");
				infoBadge.IconSource = imageIconSource;
				Content.UpdateLayout();

				Log.Comment("Switch to Animated Icon");
				infoBadge.IconSource = animatedIconSource;
				Content.UpdateLayout();
			});
		}

		[TestMethod]
		public void InfoBadgeValueLessThanNegativeOneCrashes()
		{
			InfoBadge infoBadge = null;
			RunOnUIThread.Execute(() =>
			{
				infoBadge = new InfoBadge();
				Content = infoBadge;
				Content.UpdateLayout();
			});

			IdleSynchronizer.Wait();

			RunOnUIThread.Execute(() =>
			{
				Verify.Throws<ArgumentOutOfRangeException>(() => { infoBadge.Value = -10; });
			});
		}

	}
}
