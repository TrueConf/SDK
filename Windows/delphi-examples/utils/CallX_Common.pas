unit CallX_Common;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes;

type
  TCallState = (csBeforeInit, csNone, csConnect, csLogin, csNormal, csWait, csConference, csClose);


function GetShiftDown : Boolean;
function IntToCallState(AState: integer): TCallState;

implementation

function GetShiftDown : Boolean;
begin
  Result := HiWord(GetKeyState(VK_SHIFT)) <> 0;
end;

function IntToCallState(AState: integer): TCallState;
begin
  Result := csNone;

  case AState of
    0: Result := csNone;
    1: Result := csConnect;
    2: Result := csLogin;
    3: Result := csNormal;
    4: Result := csWait;
    5: Result := csConference;
    6: Result := csClose;
    else
      Result := csNone;
  end;
end;

end.
