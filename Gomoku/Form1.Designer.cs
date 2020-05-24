using System.Windows.Forms;

namespace Gomoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_AI_Mode = new System.Windows.Forms.Button();
            this.btn_Start_Server = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(73, 23);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 0;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_AI_Mode
            // 
            this.btn_AI_Mode.Location = new System.Drawing.Point(155, 23);
            this.btn_AI_Mode.Name = "btn_AI_Mode";
            this.btn_AI_Mode.Size = new System.Drawing.Size(97, 23);
            this.btn_AI_Mode.TabIndex = 1;
            this.btn_AI_Mode.Text = "AI Mode";
            this.btn_AI_Mode.UseVisualStyleBackColor = true;
            this.btn_AI_Mode.Click += new System.EventHandler(this.btn_AI_Mode_Click);
            // 
            // btn_Start_Server
            // 
            this.btn_Start_Server.Location = new System.Drawing.Point(601, 23);
            this.btn_Start_Server.Name = "btn_Start_Server";
            this.btn_Start_Server.Size = new System.Drawing.Size(75, 23);
            this.btn_Start_Server.TabIndex = 2;
            this.btn_Start_Server.Text = "Host";
            this.btn_Start_Server.UseVisualStyleBackColor = true;
            this.btn_Start_Server.Click += new System.EventHandler(this.btn_Start_Server_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(520, 22);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 3;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Gomoku.Properties.Resources.board;
            this.ClientSize = new System.Drawing.Size(750, 750);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.btn_Start_Server);
            this.Controls.Add(this.btn_AI_Mode);
            this.Controls.Add(this.btn_Reset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Gomoku";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_Reset;
        private Button btn_AI_Mode;
        private Button btn_Start_Server;
        private Button btn_Connect;
    }
}

