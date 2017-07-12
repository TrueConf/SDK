object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Get Hardware List'
  ClientHeight = 553
  ClientWidth = 392
  Color = clBtnFace
  Constraints.MinHeight = 600
  Constraints.MinWidth = 400
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -13
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Scaled = False
  OnCreate = FormCreate
  PixelsPerInch = 120
  TextHeight = 16
  object TrueConfCallX1: TTrueConfCallX
    Left = 0
    Top = 0
    Width = 392
    Height = 177
    Align = alTop
    TabOrder = 0
    OnXAfterStart = TrueConfCallX1XAfterStart
    ExplicitTop = -6
    ControlData = {000C000069200000A30E0000}
  end
  object PageControl1: TPageControl
    Left = 0
    Top = 177
    Width = 392
    Height = 319
    ActivePage = TabSheet1
    Align = alClient
    TabOrder = 1
    ExplicitTop = 113
    ExplicitHeight = 440
    object TabSheet1: TTabSheet
      Caption = 'Cameras'
      ExplicitLeft = 8
      ExplicitHeight = 409
      object memoCameras: TMemo
        Left = 0
        Top = 0
        Width = 384
        Height = 288
        Align = alClient
        Lines.Strings = (
          'None')
        ReadOnly = True
        ScrollBars = ssBoth
        TabOrder = 0
        WordWrap = False
        ExplicitHeight = 409
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Microphones'
      ImageIndex = 1
      ExplicitWidth = 281
      ExplicitHeight = 162
      object memoMicrophones: TMemo
        Left = 0
        Top = 0
        Width = 384
        Height = 288
        Align = alClient
        Lines.Strings = (
          'None')
        ReadOnly = True
        ScrollBars = ssBoth
        TabOrder = 0
        WordWrap = False
        ExplicitTop = 4
      end
    end
    object TabSheet3: TTabSheet
      Caption = 'Speakers'
      ImageIndex = 2
      ExplicitWidth = 281
      ExplicitHeight = 162
      object memoSpeakers: TMemo
        Left = 0
        Top = 0
        Width = 384
        Height = 288
        Align = alClient
        Lines.Strings = (
          'None')
        ReadOnly = True
        ScrollBars = ssBoth
        TabOrder = 0
        WordWrap = False
        ExplicitHeight = 409
      end
    end
  end
  object Panel1: TPanel
    Left = 0
    Top = 496
    Width = 392
    Height = 57
    Align = alBottom
    BevelOuter = bvNone
    TabOrder = 2
    ExplicitTop = 498
    DesignSize = (
      392
      57)
    object btnRefresh: TButton
      Left = 4
      Top = 6
      Width = 113
      Height = 41
      Caption = 'Refresh'
      TabOrder = 0
      OnClick = btnRefreshClick
    end
    object btnClose: TButton
      Left = 275
      Top = 6
      Width = 113
      Height = 41
      Anchors = [akTop, akRight]
      Caption = 'Close'
      TabOrder = 1
      OnClick = btnCloseClick
    end
  end
end
