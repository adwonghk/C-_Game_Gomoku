using SimpleTCP;
using System;
using System.Media;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {

        private Game _game;
        private SoundPlayer _audio;

        private bool _isHosting = false;
        private bool _isConnecting = false;

        private SimpleTcpServer _server;
        private SimpleTcpClient _client;

        private string player_name;
        private bool turn = false;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _game = new Game();
            _audio = new SoundPlayer(Properties.Resources.place);
        }

        public void PlaceAChess(int x, int y)
        {
            if (_isConnecting && _client != null)
            {
                // client send data
                _client.WriteLine("player:" + player_name + "," + x + "," + y);

            }
            if (_isHosting && _server != null)
            {
                // server send data
                _server.BroadcastLine("player:" + player_name + "," + x + "," + y);
            }
            Chess chess = _game.PlaceAChess(x, y);
            if (chess != null)
            {
                chess.Name = "chess";
                this.Controls.Add(chess);
                _audio.Play();
            }
            if (_game.IsGameOver)
            {
                if (!_game.IsBlack)
                {
                    MessageBox.Show("Black wins");
                }
                else
                {
                    MessageBox.Show("White wins");
                }
                Reset();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            PlaceAChess(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        public void Reset()
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i].Name == "chess") Controls[i].Dispose();
            }

            _game.IsAIMode = false;
            btn_AI_Mode.Text = "AI Mode" + " = OFF";
            btn_AI_Mode.Enabled = true;

            _game.Reset();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btn_AI_Mode_Click(object sender, EventArgs e)
        {
            _game.IsAIMode = !_game.IsAIMode;
            String text = _game.IsAIMode ? " = ON" : " = OFF";
            btn_AI_Mode.Text = "AI Mode" + text;

            if (_game.IsAIMode)
            {
                Reset();
                btn_AI_Mode.Enabled = false;
            }

            Chess chess = _game.PlaceAChess(Board.OFFSET * 5, Board.OFFSET * 5);
            if (chess != null)
            {
                chess.Name = "chess";
                this.Controls.Add(chess);
                _audio.Play();
            }
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            player_name = "player2";
            if (ShowInputDialog(ref player_name, "Name", "Please input your name:") == DialogResult.OK)
            {
                string ip_address = string.Empty;
                if (ShowInputDialog(ref ip_address, "IP", "Please input an address:") == DialogResult.OK)
                {
                    _isConnecting = true;
                    _client = new SimpleTcpClient();
                    _client.Connect(ip_address, 6464);
                    Console.WriteLine("Connecting...");
                    _client.WriteLineAndGetReply("ask for connected", TimeSpan.FromSeconds(3));
                    _client.Delimiter = 0x13;
                    _client.DelimiterDataReceived += (ssender, msg) =>
                    {
                        if (msg.MessageString == "you are connected")
                        {
                            Console.WriteLine("Connected!");
                        }
                        else
                        {
                            string[] buffer = msg.MessageString.Split(',');
                            Chess chess = _game.PlaceAChess(Int32.Parse(buffer[1]), Int32.Parse(buffer[2]));
                            if (chess != null)
                            {
                                chess.Name = "chess";
                                this.Invoke((MethodInvoker)(() => this.Controls.Add(chess)));
                                this.Invoke((MethodInvoker)(() => this.Refresh()));
                                _audio.Play();
                            }
                            if (_game.IsGameOver)
                            {
                                if (!_game.IsBlack)
                                {
                                    this.Invoke((MethodInvoker)(() => MessageBox.Show("Black wins")));
                                }
                                else
                                {
                                    this.Invoke((MethodInvoker)(() => MessageBox.Show("White wins")));
                                }
                                this.Invoke((MethodInvoker)(() => Reset()));
                            }
                        }

                    };
                }
            }
        }

        private void btn_Start_Server_Click(object sender, EventArgs e)
        {
            _isHosting = !_isHosting;
            btn_Connect.Enabled = !_isHosting;

            if (!_isHosting) return;

            player_name = "player1";
            if (ShowInputDialog(ref player_name, "Name", "Please input your name:") == DialogResult.OK)
            {
                _server = new SimpleTcpServer().Start(6464);
                Console.WriteLine("Starting server...");
                _server.Delimiter = 0x13;
                _server.DelimiterDataReceived += (ssender, msg) =>
                {
                    if (msg.MessageString == "ask for connected")
                    {
                        Console.WriteLine("A player is connected!");
                    }
                    else
                    {

                        if (msg.MessageString.Contains("player:"))
                        {
                            Console.WriteLine(msg.MessageString.Substring(7) + " is connected to server.");
                        }
                        string[] buffer = msg.MessageString.Split(',');
                        //PlaceAChess(Int32.Parse(buffer[1]), Int32.Parse(buffer[2]));
                        Chess chess = _game.PlaceAChess(Int32.Parse(buffer[1]), Int32.Parse(buffer[2]));
                        if (chess != null)
                        {
                            chess.Name = "chess";
                            this.Invoke((MethodInvoker)(() => this.Controls.Add(chess)));
                            this.Invoke((MethodInvoker)(() => this.Refresh()));
                            _audio.Play();
                        }
                        if (_game.IsGameOver)
                        {
                            if (!_game.IsBlack)
                            {
                                this.Invoke((MethodInvoker)(() => MessageBox.Show("Black wins")));
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)(() => MessageBox.Show("White wins")));
                            }
                            this.Invoke((MethodInvoker)(() => Reset()));
                        }

                    }
                };
            }
        }

        private DialogResult ShowInputDialog(ref string input, string title, string tip)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 75);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;
            inputBox.FormBorderStyle = FormBorderStyle.FixedSingle;
            inputBox.MaximizeBox = false;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.ShowIcon = false;

            Label label = new Label();
            label.Text = tip;
            label.Location = new System.Drawing.Point(5, 5);
            label.Size = new System.Drawing.Size(size.Width - 10, 23);
            inputBox.Controls.Add(label);

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 28);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 50);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 50);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;

            inputBox.Dispose();
            return result;
        }
    }
}
