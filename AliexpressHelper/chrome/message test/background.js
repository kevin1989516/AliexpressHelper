(function(){
	chrome.browserAction.onClicked.addListener(function(tab){
		chrome.tabs.sendMessage(tab.id,{greeting:"do something in contentscript!"},
			function(response){
			/** 回调函数，用来处理请求返回的json对象:response **/
			console.log(response.farewell);
		})
	})
})();


function geturl() {
    //alert(1);
//    chrome.tabs.sendMessage(tab.id, { greeting: "do something in contentscript!" },
//			function (response) {
//			    /** 回调函数，用来处理请求返回的json对象:response **/
//			    console.log(response.farewell);
    //	})
    $.get("http://localhost/newrelictest/WebForm1.aspx?action=geturl", function (result) {
        if (result != '')
        {
            //alert(result);
            //$.get("http://localhost/newrelictest/WebForm1.aspx?action=clearurl", function (data) {});
            sendMessageToTabs({ greeting: "do something in contentscript!", url: result }, test);
        }
        
    });


    //sleep(5000);
    //geturl();
}

function httpget(url)
{
    var data = '';
    $.ajax({
        type: "get",
        url: "http://localhost/newrelictest/WebForm1.aspx?action=geturl",
        data: "",
        async: false,
        success: function (data) {
            data = data;
        }
    });
    return data;
}

function test()
{
    $.get("http://localhost/newrelictest/WebForm1.aspx?action=clearurl", function (data) { });
  //alert('test');
}

//geturl();
setInterval(geturl, 18000);

function sendMessageToTabs(message, callbackFunc) {
    chrome.tabs.query({}, function (tabsArray) {
        for (var i = 0; i < tabsArray.length; i++) {
            //console.log("Tab id: "+tabsArray[i].id);
            chrome.tabs.sendMessage(tabsArray[i].id, message, callbackFunc);
        }
    });
}

function sleep(d) {
    if (!d) {
        d = Math.random() * 1000 + 1000
        // d = Math.random() * 1500;
    }
    for (var t = Date.now() ; Date.now() - t <= d;);
}