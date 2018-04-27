(function () {
    console.log("contentscript injected!");

    var resOK = {
        farewell: "contentscript send response back..."
    };
    var resErr = {
        farewell: "contentscript has error!"
    };

    chrome.runtime.onMessage.addListener(function (request, sender, senderResponse) {
		

		if(window.location.href == 'https://www.linkedin.com/login/')
		{
              window.location.href = 'https://www.linkedin.com/uas/login?fromSignIn=true&trk=uno-reg-join-sign-in';
		}
		else
		{
			window.location = request.url.replace("https","http");
			senderResponse(resOK);
		}

        
        //        console.log("receiving request comes from extension...");
        //        alert('content');
        //        if (request.greeting === "do something in contentscript!") {
        //            senderResponse(resOK);
        //        } else {
        //            senderResponse(resErr);
        //        }
    })


    
})()

function sleep(d) {
    if (!d) {
        d = Math.random() * 1000 + 1000
        // d = Math.random() * 1500;
    }
    for (var t = Date.now() ; Date.now() - t <= d;);
}
$(document).ready(function () {

    var body = document.documentElement.outerHTML;
    $.post("http://localhost/newrelictest/WebForm1.aspx?action=setdata", { data: body }, function (result) {
    });
	
	if(window.location.href == 'https://www.linkedin.com/uas/login?fromSignIn=true&trk=uno-reg-join-sign-in')
	{
		var doc = document;
		var userid = doc.getElementById("session_key-login");
		if(userid!=null)
		{
			userid.value= 'kevinma@jobsdb.com';
			doc.getElementById("session_password-login").value = 'a1989516';
			doc.getElementById("btn-primary").click();
		}
	}
    
});
