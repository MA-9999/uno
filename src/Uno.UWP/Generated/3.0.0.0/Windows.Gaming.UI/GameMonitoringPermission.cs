#pragma warning disable 108 // new keyword hiding
#pragma warning disable 114 // new keyword hiding
namespace Windows.Gaming.UI
{
#if __ANDROID__ || __IOS__ || IS_UNIT_TESTS || __WASM__ || __MACOS__
#if __ANDROID__ || __IOS__ || IS_UNIT_TESTS || __WASM__ || __MACOS__
	[global::Uno.NotImplemented]
#endif
	public enum GameMonitoringPermission
	{
#if __ANDROID__ || __IOS__ || IS_UNIT_TESTS || __WASM__ || __MACOS__
		Allowed,
#endif
#if __ANDROID__ || __IOS__ || IS_UNIT_TESTS || __WASM__ || __MACOS__
		DeniedByUser,
#endif
#if __ANDROID__ || __IOS__ || IS_UNIT_TESTS || __WASM__ || __MACOS__
		DeniedBySystem,
#endif
	}
#endif
}
