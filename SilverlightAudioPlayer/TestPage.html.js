// JScript source code

//contains calls to silverlight.js, example below loads Page.xaml
function createSilverlight()
{
	Sys.Silverlight.createObjectEx({
		source: "SimplePlayer.xaml",
		parentElement: document.getElementById("SilverlightControlHost"),
		id: "SilverlightControl",
		properties: {
			width: "700",
			height: "120",
			version: "0.95",
			enableHtmlAccess: true
		},
		events: {}
	});
}
