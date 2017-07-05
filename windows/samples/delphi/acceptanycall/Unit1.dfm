object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Form1'
  ClientHeight = 550
  ClientWidth = 689
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -13
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  Scaled = False
  DesignSize = (
    689
    550)
  PixelsPerInch = 120
  TextHeight = 16
  object Shape1: TShape
    Left = 560
    Top = 24
    Width = 121
    Height = 57
    Anchors = [akTop, akRight]
    ExplicitLeft = 530
  end
  object TrueConfCallX1: TTrueConfCallX
    Left = 24
    Top = 24
    Width = 511
    Height = 500
    Anchors = [akLeft, akTop, akRight, akBottom]
    TabOrder = 0
    OnXAfterStart = TrueConfCallX1XAfterStart
    OnInviteReceived = TrueConfCallX1InviteReceived
    OnServerConnected = TrueConfCallX1ServerConnected
    OnXLogin = TrueConfCallX1XLogin
    ExplicitWidth = 481
    ExplicitHeight = 321
    ControlData = {000C0000402A000057290000}
  end
end
