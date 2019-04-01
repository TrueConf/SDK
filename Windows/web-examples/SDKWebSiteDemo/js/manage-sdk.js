var sdk; //activex component
var server = "ru10.trueconf.net"; //server name or IP
var id = "demo_sdk_user"; //trueconf id
var password = "123456"; //trueconf password
var isLogin = false; //login flag
var inConference = false; //busy flag

function connectToAXEvent(o, etype, func) //func for add eventListener to activex component
{
  if (o.attachEvent) {
    o.attachEvent(etype, func);
  } else {
    var id = o.id, f = func;
    eval('(function(){' +
      'function ' + id + '::' + etype + '(){' +
      'f.apply(f,arguments);' +
      '}' +
      '})();');
  }
}

function start() { //body onload listener

    isLogin = false;
    isStart = false;
	$("#status").html("Starting...");
    
    sdk = document.getElementById("activex"); //get activex component

    document.getElementById("submit_call").addEventListener("click", function(e) {
      if(!inConference && $("#user_id").val() !== "") {
        sdk.call($("#user_id").val());
		$("#status").html("Calling...");
      }
    }, false);
    
    document.getElementById("hungup_call").addEventListener("click", function(e) {
        if(inConference) {
          sdk.hangup();
        }
      }, false);

    connectToAXEvent(sdk, 'OnXAfterStart', function (e) { //add handling for OnXAfterStart event
      isStart = true;
      sdk.XSetCameraByIndex(0); //select camera
      sdk.XSelectMicByIndex(0); //select microphone
      sdk.XSelectSpeakerByIndex(0); //select speakers
      sdk.connectToServer(server); //connect to the server/service
	  $("#status").html("Connecting...");
    });

    connectToAXEvent(sdk, 'OnServerConnected', function (e) { //add handling for OnServerConnected event
      sdk.login(id, password); //login
    });

    connectToAXEvent(sdk, 'OnXError', function (e) { //add handling for OnXError event
      isLogin = false;
      $("#status").html("Error logged in on server!");
    });

    connectToAXEvent(sdk, 'OnXLogin', function (e) { //add handling for OnXLogin event
      isLogin = true;
      $("#status").html("You successfully logged in on server!");
    });
    connectToAXEvent(sdk, 'OnXLoginError', function (e) { //add handling for OnXLoginError event
      isLogin = false;
      $("#status").html("Error logged in on server!");
    });
    connectToAXEvent(sdk, 'OnConferenceCreated', function (e) { //add handling for OnConferenceCreated event
      inConference = true; // call or conference started
      $("#status").html("Call or conference started!");
    });
    connectToAXEvent(sdk, 'OnConferenceDeleted', function (e) { //add handling for OnConferenceDeleted event
      inConference = false; //call or conference ended
      $("#status").html("Call or conference ended!");
    });  
    connectToAXEvent(sdk, 'OnInviteReceived', function (e) { //add handling for OnConferenceDeleted event      
      sdk.accept(); //accept incoming call
    });
	connectToAXEvent(sdk, 'OnRejectReceived', function (e) { //add handling for OnRejectReceived event  
		var msg = JSON.parse(e);
		switch(msg.cause) { //show status
			case 0:
			$("#status").html("Call rejected by participant");
			break;
			case 1:
			$("#status").html("Conference is busy");
			break;
			case 2:
			$("#status").html("Participant is busy");
			break;
			case 3:
			$("#status").html("Participant not available now");
			break;
			case 4:
			$("#status").html("Invalid conference");
			break;
			case 5:
			$("#status").html("Invalid participant");
			break;
			case 6:
			$("#status").html("Join ok");
			break;
			case 7:
			$("#status").html("Reach money limit");
			break;
			case 8:
			$("#status").html("Call rejected by access denied");
			break;
			case 9:
			$("#status").html("Call rejected by logout");
			break;
			case 10:
			$("#status").html("Call rejected by resource limit");
			break;
			case 11:
			$("#status").html("Call rejected by local resource");
			break;
			case 12:
			$("#status").html("Conference password required");
			break;
			case 13:
			$("#status").html("Call rejected by wrong password");
			break;
			case 14:
			$("#status").html("Call rejected because user is not in your Address book");
			break;
			case 15:
			$("#status").html("Call rejected by bad rating");
			break;
			case 16:
			$("#status").html("Call rejected by timeout");
			break;
			case 17:
			$("#status").html("This is conference");
		}
    });    
  } 