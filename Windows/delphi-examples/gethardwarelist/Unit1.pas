unit Unit1;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.ComCtrls, Vcl.OleCtrls,
  TrueConf_CallXLib_TLB, Vcl.StdCtrls, Vcl.ExtCtrls;

type
  TForm1 = class(TForm)
    TrueConfCallX1: TTrueConfCallX;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    TabSheet3: TTabSheet;
    memoCameras: TMemo;
    memoMicrophones: TMemo;
    memoSpeakers: TMemo;
    Panel1: TPanel;
    btnRefresh: TButton;
    btnClose: TButton;
    procedure TrueConfCallX1XAfterStart(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnRefreshClick(Sender: TObject);
    procedure btnCloseClick(Sender: TObject);
  private
    { Private declarations }
  protected
    procedure RefreshLists;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.btnCloseClick(Sender: TObject);
begin
  Close;
end;

procedure TForm1.btnRefreshClick(Sender: TObject);
begin
  RefreshLists;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  btnRefresh.Enabled := False;
  PageControl1.ActivePageIndex := 0;
end;

procedure TForm1.RefreshLists;
begin
  if not btnRefresh.Enabled then
    Exit;

  memoCameras.Lines.Text := TrueConfCallX1.XGetCameraList;
  memoMicrophones.Lines.Text := TrueConfCallX1.XGetMicList;
  memoSpeakers.Lines.Text := TrueConfCallX1.XGetSpeakerList;
end;

procedure TForm1.TrueConfCallX1XAfterStart(Sender: TObject);
begin
  btnRefresh.Enabled := True;
  RefreshLists;
end;

end.
