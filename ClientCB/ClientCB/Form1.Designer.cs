namespace ClientCB
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonGioca = new System.Windows.Forms.Button();
            this.textBoxNome = new System.Windows.Forms.TextBox();
            this.labelAttesa = new System.Windows.Forms.Label();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.timerSposta = new System.Windows.Forms.Timer(this.components);
            this.timerPartita = new System.Windows.Forms.Timer(this.components);
            this.button = new System.Windows.Forms.Button();
            this.buttonRivincita = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGioca
            // 
            this.buttonGioca.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.buttonGioca.Location = new System.Drawing.Point(573, 339);
            this.buttonGioca.Name = "buttonGioca";
            this.buttonGioca.Size = new System.Drawing.Size(101, 57);
            this.buttonGioca.TabIndex = 0;
            this.buttonGioca.Text = "Gioca";
            this.buttonGioca.UseVisualStyleBackColor = true;
            this.buttonGioca.Click += new System.EventHandler(this.Login_Click);
            // 
            // textBoxNome
            // 
            this.textBoxNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.textBoxNome.Location = new System.Drawing.Point(509, 275);
            this.textBoxNome.Name = "textBoxNome";
            this.textBoxNome.Size = new System.Drawing.Size(236, 31);
            this.textBoxNome.TabIndex = 1;
            // 
            // labelAttesa
            // 
            this.labelAttesa.AutoSize = true;
            this.labelAttesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.labelAttesa.Location = new System.Drawing.Point(469, 209);
            this.labelAttesa.Name = "labelAttesa";
            this.labelAttesa.Size = new System.Drawing.Size(314, 31);
            this.labelAttesa.TabIndex = 2;
            this.labelAttesa.Text = "Attesa di un avversario...";
            this.labelAttesa.Visible = false;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(1431, 12);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(75, 23);
            this.buttonLogout.TabIndex = 3;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Visible = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // timerSposta
            // 
            this.timerSposta.Interval = 300;
            this.timerSposta.Tick += new System.EventHandler(this.timerSposta_Tick);
            // 
            // timerPartita
            // 
            this.timerPartita.Interval = 1000;
            this.timerPartita.Tick += new System.EventHandler(this.timerPartita_Tick);
            // 
            // button
            // 
            this.button.BackColor = System.Drawing.Color.LimeGreen;
            this.button.Location = new System.Drawing.Point(275, 423);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(50, 50);
            this.button.TabIndex = 4;
            this.button.UseVisualStyleBackColor = false;
            this.button.Visible = false;
            // 
            // buttonRivincita
            // 
            this.buttonRivincita.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.buttonRivincita.Location = new System.Drawing.Point(558, 423);
            this.buttonRivincita.Name = "buttonRivincita";
            this.buttonRivincita.Size = new System.Drawing.Size(127, 38);
            this.buttonRivincita.TabIndex = 5;
            this.buttonRivincita.Text = "Rivincita";
            this.buttonRivincita.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 650);
            this.Controls.Add(this.buttonRivincita);
            this.Controls.Add(this.button);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.labelAttesa);
            this.Controls.Add(this.textBoxNome);
            this.Controls.Add(this.buttonGioca);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGioca;
        private System.Windows.Forms.TextBox textBoxNome;
        private System.Windows.Forms.Label labelAttesa;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Timer timerSposta;
        private System.Windows.Forms.Timer timerPartita;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Button buttonRivincita;
    }
}

