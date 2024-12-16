open System
open System.Drawing
open System.Windows.Forms
open AdminPanel
open BookPanel

let MainWindow = new Form(
    Text = "System Informacji Medycznej",
    Size = new Size(800, 600),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = true,
    BackColor = Color.White
)

let layout = new TableLayoutPanel(
    Dock = DockStyle.Fill,
    RowCount = 4,
    ColumnCount = 1,
    Padding = new Padding(20)
)
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 10.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 60.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20.0f)) |> ignore

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
    ConnectionTestDialog.Show()
    
    BookWindow.Show()
    ConnectionTestDialog.Close()
)
layout.Controls.Add(AppointmentButton, 0, 2)

let LoginButtonn = new Button(
    Text = "Portal lekarza",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
layout.Controls.Add(LoginButtonn, 0, 3)

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
        Text = "Wprowadź hasło"
    )

    let PasswordBox = new TextBox(
        PasswordChar = '•',
        Dock = DockStyle.Top
    )

    let SubmitButton = new Button(
        Text = "Zaloguj",
        Dock = DockStyle.Bottom
    )

    AdminLogin.Controls.Add(PasswordBox)
    AdminLogin.Controls.Add(SubmitButton)
    AdminLogin.Show()

    SubmitButton.Click.Add(fun _ ->
        if PasswordBox.Text = "admin" then
            AdminForm.Show()
            AdminLogin.Close()
        else
            MessageBox.Show("Niepoprawne hasło!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) |> ignore
            AdminLogin.Close()
    )
)

Application.EnableVisualStyles()
Application.Run(MainWindow)