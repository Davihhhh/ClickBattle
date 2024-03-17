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
            this.button = new System.Windows.Forms.Button();
            this.labelRecord = new System.Windows.Forms.Label();
            this.labelVittorie = new System.Windows.Forms.Label();
            this.labelNome = new System.Windows.Forms.Label();
            this.buttonRivincita = new System.Windows.Forms.Button();
            this.timerPartita = new System.Windows.Forms.Timer(this.components);
            this.comboBoxAvversari = new System.Windows.Forms.ComboBox();
            this.buttonSfida = new System.Windows.Forms.Button();
            this.buttonRicarica = new System.Windows.Forms.Button();
            this.labelPunteggio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonGioca
            // 
            this.buttonGioca.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.buttonGioca.Location = new System.Drawing.Point(609, 316);
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
            this.textBoxNome.Location = new System.Drawing.Point(545, 252);
            this.textBoxNome.Name = "textBoxNome";
            this.textBoxNome.Size = new System.Drawing.Size(236, 31);
            this.textBoxNome.TabIndex = 1;
            // 
            // labelAttesa
            // 
            this.labelAttesa.AutoSize = true;
            this.labelAttesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.labelAttesa.Location = new System.Drawing.Point(305, 56);
            this.labelAttesa.Name = "labelAttesa";
            this.labelAttesa.Size = new System.Drawing.Size(282, 31);
            this.labelAttesa.TabIndex = 2;
            this.labelAttesa.Text = "Scegli un avversario...";
            this.labelAttesa.Visible = false;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(1223, 12);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(75, 23);
            this.buttonLogout.TabIndex = 3;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Visible = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // button
            // 
            this.button.BackColor = System.Drawing.Color.LimeGreen;
            this.button.Location = new System.Drawing.Point(320, 418);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(60, 60);
            this.button.TabIndex = 4;
            this.button.UseVisualStyleBackColor = false;
            this.button.Visible = false;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // labelRecord
            // 
            this.labelRecord.AutoSize = true;
            this.labelRecord.Location = new System.Drawing.Point(12, 32);
            this.labelRecord.Name = "labelRecord";
            this.labelRecord.Size = new System.Drawing.Size(42, 13);
            this.labelRecord.TabIndex = 8;
            this.labelRecord.Text = "Record";
            this.labelRecord.Visible = false;
            // 
            // labelVittorie
            // 
            this.labelVittorie.AutoSize = true;
            this.labelVittorie.Location = new System.Drawing.Point(12, 56);
            this.labelVittorie.Name = "labelVittorie";
            this.labelVittorie.Size = new System.Drawing.Size(39, 13);
            this.labelVittorie.TabIndex = 10;
            this.labelVittorie.Text = "Vittorie";
            this.labelVittorie.Visible = false;
            // 
            // labelNome
            // 
            this.labelNome.AutoSize = true;
            this.labelNome.Location = new System.Drawing.Point(12, 9);
            this.labelNome.Name = "labelNome";
            this.labelNome.Size = new System.Drawing.Size(35, 13);
            this.labelNome.TabIndex = 11;
            this.labelNome.Text = "Nome";
            this.labelNome.Visible = false;
            // 
            // buttonRivincita
            // 
            this.buttonRivincita.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.buttonRivincita.Location = new System.Drawing.Point(594, 400);
            this.buttonRivincita.Name = "buttonRivincita";
            this.buttonRivincita.Size = new System.Drawing.Size(127, 38);
            this.buttonRivincita.TabIndex = 5;
            this.buttonRivincita.Text = "Rivincita";
            this.buttonRivincita.UseVisualStyleBackColor = true;
            this.buttonRivincita.Visible = false;
            this.buttonRivincita.Click += new System.EventHandler(this.buttonRivincita_Click);
            // 
            // timerPartita
            // 
            this.timerPartita.Interval = 20000;
            this.timerPartita.Tick += new System.EventHandler(this.timerPartita_Tick_1);
            // 
            // comboBoxAvversari
            // 
            this.comboBoxAvversari.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.comboBoxAvversari.FormattingEnabled = true;
            this.comboBoxAvversari.Location = new System.Drawing.Point(311, 124);
            this.comboBoxAvversari.Name = "comboBoxAvversari";
            this.comboBoxAvversari.Size = new System.Drawing.Size(121, 25);
            this.comboBoxAvversari.TabIndex = 12;
            this.comboBoxAvversari.Visible = false;
            // 
            // buttonSfida
            // 
            this.buttonSfida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.buttonSfida.Location = new System.Drawing.Point(465, 124);
            this.buttonSfida.Name = "buttonSfida";
            this.buttonSfida.Size = new System.Drawing.Size(85, 25);
            this.buttonSfida.TabIndex = 13;
            this.buttonSfida.Text = "Sfida";
            this.buttonSfida.UseVisualStyleBackColor = true;
            this.buttonSfida.Visible = false;
            this.buttonSfida.Click += new System.EventHandler(this.buttonSfida_Click);
            // 
            // buttonRicarica
            // 
            this.buttonRicarica.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.buttonRicarica.Location = new System.Drawing.Point(192, 123);
            this.buttonRicarica.Name = "buttonRicarica";
            this.buttonRicarica.Size = new System.Drawing.Size(85, 25);
            this.buttonRicarica.TabIndex = 14;
            this.buttonRicarica.Text = "Ricarica";
            this.buttonRicarica.UseVisualStyleBackColor = true;
            this.buttonRicarica.Visible = false;
            this.buttonRicarica.Click += new System.EventHandler(this.buttonRicarica_Click);
            // 
            // labelPunteggio
            // 
            this.labelPunteggio.AutoSize = true;
            this.labelPunteggio.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.labelPunteggio.Location = new System.Drawing.Point(556, 0);
            this.labelPunteggio.Name = "labelPunteggio";
            this.labelPunteggio.Size = new System.Drawing.Size(115, 25);
            this.labelPunteggio.TabIndex = 15;
            this.labelPunteggio.Text = "Punteggio:";
            this.labelPunteggio.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 644);
            this.Controls.Add(this.labelPunteggio);
            this.Controls.Add(this.buttonRicarica);
            this.Controls.Add(this.buttonSfida);
            this.Controls.Add(this.comboBoxAvversari);
            this.Controls.Add(this.labelNome);
            this.Controls.Add(this.labelVittorie);
            this.Controls.Add(this.labelRecord);
            this.Controls.Add(this.buttonRivincita);
            this.Controls.Add(this.button);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.labelAttesa);
            this.Controls.Add(this.textBoxNome);
            this.Controls.Add(this.buttonGioca);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGioca;
        private System.Windows.Forms.TextBox textBoxNome;
        private System.Windows.Forms.Label labelAttesa;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label labelRecord;
        private System.Windows.Forms.Label labelVittorie;
        private System.Windows.Forms.Label labelNome;
        private System.Windows.Forms.Button buttonRivincita;
        private System.Windows.Forms.Timer timerPartita;
        private System.Windows.Forms.ComboBox comboBoxAvversari;
        private System.Windows.Forms.Button buttonSfida;
        private System.Windows.Forms.Button buttonRicarica;
        private System.Windows.Forms.Label labelPunteggio;
    }
}

