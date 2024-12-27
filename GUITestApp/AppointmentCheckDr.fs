module AppointmentCheckDr

open System
open System.Drawing
open System.Windows.Forms
open Microsoft.Data.SqlClient
open System.IO

let iconMain = Icon.ExtractAssociatedIcon(Path.Combine(__SOURCE_DIRECTORY__, "imageres_5356.ico"))

let CheckDrWindow = new Form(
    Text = "Panel lekarza",
    Size = new Size(800, 600),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = true,
    Icon = iconMain,
    BackColor = Color.White
)

let queryLayout = new TableLayoutPanel(
    RowCount = 4,
    ColumnCount = 1,
    Dock = DockStyle.Fill,
    Padding = new Padding(10)
)

queryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f)) |> ignore
queryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f)) |> ignore
queryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3f)) |> ignore

let queryLabel = new Label(
    Text = "Twoje najbliższe wizyty:",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill,
    Font = new Font("Arial", 24.0f, FontStyle.Bold)
)
queryLayout.Controls.Add(queryLabel, 0, 0)

let fetchDoctorAppointments pincode = 
    use connection = new SqlConnection(File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt")))
    connection.Open()
    let query = """
        select r.Pacjent, r.DataWizyty, r.Godzina from Rezerwacje as r
        inner join Lekarze as l
        on r.DrID = l.DrID
        where l.PassCode = @pin and r.Pacjent is not Null and r.Zarezerwowane = 'TAK'
    """
    let cmd = new SqlCommand(query, connection)
    cmd.Parameters.AddWithValue("@pin", pincode) |> ignore
    let reader = cmd.ExecuteReader()
    let results = new System.Collections.Generic.List<obj[]>()
    while reader.Read() do
        results.Add([|
            box(reader.["Pacjent"].ToString())
            box(reader.["DataWizyty"].ToString())
            box(reader.["Godzina"].ToString())
        |])
    results

let SQLQueryResults = new DataGridView(
    Dock = DockStyle.Fill,
    Visible = true,
    AllowUserToAddRows = false,
    BackgroundColor = Color.White,
    ForeColor = Color.Black,
    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle(
        BackColor = Color.Navy,
        ForeColor = Color.White,
        Font = new Font("Arial", 12.0f, FontStyle.Bold)
    ),
    DefaultCellStyle = new DataGridViewCellStyle(
        BackColor = Color.White,
        ForeColor = Color.Black,
        SelectionBackColor = Color.Blue,
        SelectionForeColor = Color.White
    )
)
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Pacjent"))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Data Wizyty"))
SQLQueryResults.Columns.Add(new DataGridViewTextBoxColumn(Name = "Godzina"))
queryLayout.Controls.Add(SQLQueryResults, 0, 1)

let CloseButton = new Button(
    Text = "Zakończ przegląd",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
CloseButton.Click.Add(fun _ -> CheckDrWindow.Hide())
queryLayout.Controls.Add(CloseButton, 0, 2)



let CheckPIN pin =
    use connection = new SqlConnection(File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "ipconfig.txt")))
    connection.Open()
    let query = """
        select PassCode from Lekarze
        where PassCode = @passcode
    """
    let cmd = new SqlCommand(query, connection)
    cmd.Parameters.AddWithValue("@passcode", pin) |> ignore
    let reader = cmd.ExecuteReader()
    let results = reader.Read()
    if results then
        true
    else
        false

let public CheckDrQuestion = new Form(
    Text = "Logowanie",
    Size = new Size(400, 200),
    StartPosition = FormStartPosition.CenterScreen,
    MaximizeBox = false,
    MinimizeBox = false,
    ControlBox = false,
    BackColor = Color.White
)

let layout = new TableLayoutPanel(
    Dock = DockStyle.Fill,
    RowCount = 4,
    ColumnCount = 1,
    Padding = new Padding(10)
)

layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore
layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25.0f)) |> ignore

let InfoLabel = new Label(
    Text = "Wprowadź hasło dostępu:",
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill
)
layout.Controls.Add(InfoLabel, 0, 0)

let txtBox = new TextBox(
    Text = "",
    Multiline = false,
    PasswordChar = '•',
    Dock = DockStyle.Fill,
    TextAlign = HorizontalAlignment.Center
)
layout.Controls.Add(txtBox, 0, 1)

let LogonButton = new Button(
    Text = "Zaloguj się",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
LogonButton.Click.Add(fun _ ->
        if txtBox.Text = "" then
            MessageBox.Show("Nie wprowadzone żadne dane", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) |> ignore
        else
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
                CheckDrQuestion.Hide()
                ConnectionTestDialog.Close()
                
                let result = CheckPIN txtBox.Text
                if result = true then
                   CheckDrQuestion.Hide()
                   CheckDrWindow.Show()
                   try
                       let results = fetchDoctorAppointments(txtBox.Text)
                       if results.Count > 0 then
                        SQLQueryResults.Rows.Clear()
                        for row in results do
                            SQLQueryResults.Rows.Add(row)
                        else
                            MessageBox.Show("Obecnie nie masz zarezerowanych wizyt przez pacjentów", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
                            CheckDrWindow.Hide()
                   with
                   | ex -> MessageBox.Show("Wystąpił błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore

                else
                   MessageBox.Show("Błędne hasło!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore

            with
            | ex -> MessageBox.Show("Błąd połączenia z bazą danych! Sprawdź, czy baza nie jest offline albo konfigurację połączenia w zakładce 'Inne'", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
                    ConnectionTestDialog.Close()
                    CheckDrQuestion.Hide()

)
layout.Controls.Add(LogonButton, 0, 2)

let CancelButton = new Button(
    Text = "Anuluj",
    Size = new Size(200, 50),
    Anchor = AnchorStyles.None
)
CancelButton.Click.Add(fun _ ->
    CheckDrQuestion.Hide()
    txtBox.Text <- ""
)
layout.Controls.Add(CancelButton, 0, 3)

CheckDrQuestion.Controls.Add(layout)
CheckDrWindow.Controls.Add(queryLayout)