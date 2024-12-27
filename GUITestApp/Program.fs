open System
open System.Drawing
open System.Windows.Forms
open Microsoft.Data.SqlClient
open System.IO
open AdminPanel
open BookPanel
open AppointmentCheckUser
open AppointmentCheckDr

let iconMain = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_5356.ico"))
let iconInfo = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_81.ico"))
let iconSecure = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_104.ico"))

let MainWindow = new Form(
    Text = "System Informacji Medycznej",
    Size = new Size(800, 600),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = true,
    BackColor = Color.White,
    Icon = iconMain
)

let layout = new TableLayoutPanel(
    Dock = DockStyle.Fill,
    RowCount = 6,
    ColumnCount = 1,
    Padding = new Padding(20)
)
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f)) |> ignore //settings 0
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f)) |> ignore //header 1
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60.0f)) |> ignore //find 2
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20.0f)) |> ignore //check 3
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20.0f)) |> ignore //login 4
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20.0f)) |> ignore //info 5

let Header = new Label(
    Text = "System Informacji Medycznej",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill,
    Font = new Font("Arial", 24.0f, FontStyle.Bold)
)
layout.Controls.Add(Header, 0, 1)

let AppointmentButton = new Button(
    Text = "Umów wizytę",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
AppointmentButton.Click.Add(fun _ ->
    let ConnectionTestDialog = new Form(
        ControlBox = false,
        MinimizeBox = false,
        MaximizeBox = false,
        StartPosition = FormStartPosition.CenterScreen,
        Size = new Size(200, 150),
        Text = "Proszę czekać..."
    )

    let ConnectionTestLabel = new Label(
        Text = "Sprawdzenie połączenia z bazą danych...",
        TextAlign = ContentAlignment.MiddleCenter,
        Dock = DockStyle.Top
    )
    ConnectionTestDialog.Controls.Add(ConnectionTestLabel)

    let PBar = new ProgressBar(
        Location = new Point(100, 75),
        Size = new Size(50, 25),
        Style = ProgressBarStyle.Marquee,
        Dock = DockStyle.Bottom
    )

    ConnectionTestDialog.Controls.Add(PBar)
    try
        ConnectionTestDialog.Show()
        let connectionString = File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt"))
        use connection = new SqlConnection(connectionString)
        connection.Open()
        BookWindow.Show()
        ConnectionTestDialog.Close()
    with
    | ex -> MessageBox.Show("Błąd połączenia z bazą danych!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
            ConnectionTestDialog.Close()
)
layout.Controls.Add(AppointmentButton, 0, 2)

let CheckButton = new Button(
    Text = "Sprawdź swoje wizyty",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
CheckButton.Click.Add(fun _ ->
    CheckWindowQuestion.Show()
)
layout.Controls.Add(CheckButton, 0, 3)

let LoginButtonn = new Button(
    Text = "Portal lekarza",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
LoginButtonn.Click.Add(fun _ ->
    CheckDrQuestion.Show()
)
layout.Controls.Add(LoginButtonn, 0, 4)

MainWindow.Controls.Add(layout)

let SettingsButton = new Button(
    Text = "Inne",
    Size = new Size(50, 50),
    Location = new Point(750, 0)
)
SettingsButton.Anchor <- AnchorStyles.Top ||| AnchorStyles.Right
layout.Controls.Add(SettingsButton, 0, 0)
SettingsButton.Click.Add(fun _ ->
    let AdminLogin = new Form(
        MinimizeBox = false,
        MaximizeBox = false,
        StartPosition = FormStartPosition.CenterScreen,
        Size = new Size(300, 200),
        Text = "Wprowadź hasło",
        BackColor = Color.White,
        Icon = iconSecure
    )

    let PasswordBox = new TextBox(
        PasswordChar = '•',
        Dock = DockStyle.Top,
        TextAlign = HorizontalAlignment.Center
    )

    let SubmitButton = new Button(
        Text = "Zaloguj się",
        Dock = DockStyle.Bottom
    )

    AdminLogin.Controls.Add(PasswordBox)
    AdminLogin.Controls.Add(SubmitButton)
    AdminLogin.Show()

    SubmitButton.Click.Add(fun _ ->
        if PasswordBox.Text = "admin" then
            AdminForm.Show()
            AdminLogin.Hide()
        else
            MessageBox.Show("Niepoprawne hasło!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) |> ignore
            AdminLogin.Hide()
    )
)

let AboutButton = new Button(
    //Text = "O programie",
    Size = new Size(100, 50),
    Location = new Point(750, 0),
    Dock = DockStyle.Right,
    Image = iconInfo.ToBitmap(),
    ImageAlign = ContentAlignment.MiddleCenter
)
layout.Controls.Add(AboutButton, 0, 5)

AboutButton.Click.Add(fun _ ->
    let AboutWindow = new Form(
        MinimizeBox = false,
        MaximizeBox = false,
        StartPosition = FormStartPosition.CenterScreen,
        Size = new Size(300, 200),
        Text = "O programie",
        BackColor = Color.White,
        Icon = iconInfo
    )
    let microLayout = new TableLayoutPanel(
        Dock = DockStyle.Fill,
        RowCount = 1,
        ColumnCount = 2,
        Padding = new Padding(5)
    )
    microLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0f)) |> ignore
    microLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.0f)) |> ignore

    let AboutLabel = new Label(
        Text = "System Informacji Medycznej\n\nAutor: Yaroslav Haivoronskyi\n\nRzeszów, 2024",
        TextAlign = ContentAlignment.MiddleCenter,
        Dock = DockStyle.Fill
    )

    let ProgramIcon = new PictureBox(
        Image = iconMain.ToBitmap(),
        Size = new Size(50, 50),
        Dock = DockStyle.Fill
    )
    microLayout.Controls.Add(AboutLabel, 1,0)
    microLayout.Controls.Add(ProgramIcon, 0, 0)
    AboutWindow.Controls.Add(microLayout)

    AboutWindow.Show()
)

Application.EnableVisualStyles()
Application.Run(MainWindow)