var sdk; //activex component
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
    
    sdk = document.getElementById("activex"); //get activex component

    document.getElementById("submit_call").addEventListener("click", function(e) {
      if(!inConference && $("#user_id").val() !== "") {
        sdk.call($("#user_id").val());
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
      sdk.connectToServer(""); /connect to the server/online
    });

    connectToAXEvent(sdk, 'OnServerConnected', function (e) { //add handling for OnServerConnected event
      sdk.login(id, password); //login
    });

    connectToAXEvent(sdk, 'OnXError', function (e) { //add handling for OnXError event
      isLogin = false;
      alert("Error logged in on server!");
    });

    connectToAXEvent(sdk, 'OnXLogin', function (e) { //add handling for OnXLogin event
      isLogin = true;
      alert("You successfully logged in on server!");
    });
    connectToAXEvent(sdk, 'OnXLoginError', function (e) { //add handling for OnXLoginError event
      isLogin = false;
      alert("Error logged in on server!");
    });
    connectToAXEvent(sdk, 'OnConferenceCreated', function (e) { //add handling for OnConferenceCreated event
      inConference = true; // call or conference started
      alert("Call or conference started!");
    });
    connectToAXEvent(sdk, 'OnConferenceDeleted', function (e) { //add handling for OnConferenceDeleted event
      inConference = false; //call or conference ended
      alert("Call or conference ended!");
    });  
    connectToAXEvent(sdk, 'OnInviteReceived', function (e) { //add handling for OnConferenceDeleted event      
      sdk.accept(); //accept incoming call
    });    
  } 