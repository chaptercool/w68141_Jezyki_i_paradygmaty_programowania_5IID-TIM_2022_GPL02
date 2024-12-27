module AdminPanel

open System.Drawing
open System.Windows.Forms
open System
open System.IO

let iconSecureOK = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_106.ico"))

let public AdminForm = new Form(
    Text = "Ustawienia systemowe",
    Size = new Size(500, 400),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = false,
    BackColor = Color.White,
    Icon = iconSecureOK
)

// Top layout panel
let layoutTop = new TableLayoutPanel(
    Dock = DockStyle.Top,
    RowCount = 1,
    ColumnCount = 2,
    Height = 100 // Adjust height as needed
)
layoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0f)) |> ignore
layoutTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.0f)) |> ignore

let txtLabel = new Label(
    Text = "Konfiguracja bazy:",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill,
    AutoSize = true,
    ForeColor = Color.Black,
    BackColor = Color.White
)
layoutTop.Controls.Add(txtLabel, 0, 0)

let content =
    let filePath = Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt")
    if File.Exists(filePath) then
        File.ReadAllText(filePath)
    else
        ""

let txtBox = new TextBox(
    Text = content,
    Multiline = true,
    Dock = DockStyle.Fill
)
layoutTop.Controls.Add(txtBox, 1, 0)

// Middle layout panel
let layoutMiddle = new TableLayoutPanel(
    Dock = DockStyle.Fill,
    RowCount = 1,
    ColumnCount = 1,
    Padding = new Padding(5),
    AutoScroll = false
)
layoutMiddle.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f)) |> ignore

let NoteLabel = new Label(
    Text = "Format konfiguracji musi być następujący:\nServer=<Nazwa albo IP serwera>;Database=<Nazwa bazy>;TrustServerCertificate=<True albo False>;User Id=<username>;Password=<Hasło>;.\n W przypadku łączenia do serwera przez IP adres, należy wskazać port przez który należy uzyskać dostęp (przykładowo 0.0.0.0,0000). Standardowy port do połączenia serwera to 1433.",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill,
    AutoSize = false,
    MaximumSize = new Size(480, 0),
    ForeColor = Color.Black,
    BackColor = Color.White
)
layoutMiddle.Controls.Add(NoteLabel, 0, 0)

// Bottom layout panel
let layoutBottom = new TableLayoutPanel(
    Dock = DockStyle.Bottom,
    RowCount = 1,
    ColumnCount = 2,
    Height = 60 // Adjust height as needed
)
layoutBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0f)) |> ignore
layoutBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0f)) |> ignore

let CancelButton = new Button(
    Text = "Anuluj",
    Size = new Size(100, 50),
    Anchor = AnchorStyles.None
)
CancelButton.Click.Add(fun _ -> AdminForm.Close())
layoutBottom.Controls.Add(CancelButton, 0, 0)

let SaveButton = new Button(
    Text = "Zapisz",
    Size = new Size(100, 50),
    Anchor = AnchorStyles.None
)
SaveButton.Click.Add(fun _ ->
    let newContent = txtBox.Text
    let filePath = Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt")
    try
        File.WriteAllText(filePath, newContent)
        MessageBox.Show("Zapisano ustawienia", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
        AdminForm.Hide()
    with
    | ex ->
        MessageBox.Show($"Wystąpił błąd podczas zapisywania konfiguracji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
)
layoutBottom.Controls.Add(SaveButton, 1, 0)

// Add layout panels to the form
AdminForm.Controls.Add(layoutTop)
AdminForm.Controls.Add(layoutMiddle)
AdminForm.Controls.Add(layoutBottom)
