
namespace GridOfCells
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.matriz = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.usuario = new System.Windows.Forms.TextBox();
            this.contrasena = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.conectar = new System.Windows.Forms.Button();
            this.desconectar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.registrarse = new System.Windows.Forms.Button();
            this.iniciarsesion = new System.Windows.Forms.Button();
            this.Consulta1 = new System.Windows.Forms.Button();
            this.Consulta2 = new System.Windows.Forms.Button();
            this.invitar = new System.Windows.Forms.Button();
            this.invitado = new System.Windows.Forms.TextBox();
            this.jugar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.matriz)).BeginInit();
            this.SuspendLayout();
            // 
            // matriz
            // 
            this.matriz.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.matriz.ColumnHeadersHeight = 15;
            this.matriz.GridColor = System.Drawing.Color.Black;
            this.matriz.Location = new System.Drawing.Point(940, 57);
            this.matriz.Name = "matriz";
            this.matriz.RowHeadersWidth = 15;
            this.matriz.RowTemplate.Height = 24;
            this.matriz.Size = new System.Drawing.Size(523, 436);
            this.matriz.TabIndex = 0;
            this.matriz.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.matriz_CellClick_1);
            this.matriz.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.matriz_CellClick_1);
            this.matriz.Leave += new System.EventHandler(this.Form1_Load);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(2165, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.Yellow;
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(263, 166);
            this.usuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(166, 26);
            this.usuario.TabIndex = 12;
            // 
            // contrasena
            // 
            this.contrasena.Location = new System.Drawing.Point(263, 208);
            this.contrasena.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(166, 26);
            this.contrasena.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(614, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(310, 734);
            this.label2.TabIndex = 22;
            // 
            // conectar
            // 
            this.conectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conectar.Location = new System.Drawing.Point(37, 65);
            this.conectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(224, 48);
            this.conectar.TabIndex = 25;
            this.conectar.Text = "conectar";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // desconectar
            // 
            this.desconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desconectar.Location = new System.Drawing.Point(300, 65);
            this.desconectar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(258, 48);
            this.desconectar.TabIndex = 26;
            this.desconectar.Text = "desconectar";
            this.desconectar.UseVisualStyleBackColor = true;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "Contrasena";
            // 
            // registrarse
            // 
            this.registrarse.Location = new System.Drawing.Point(39, 319);
            this.registrarse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registrarse.Name = "registrarse";
            this.registrarse.Size = new System.Drawing.Size(242, 102);
            this.registrarse.TabIndex = 29;
            this.registrarse.Text = "Registrarse";
            this.registrarse.UseVisualStyleBackColor = true;
            this.registrarse.Click += new System.EventHandler(this.registrarse_Click_1);
            // 
            // iniciarsesion
            // 
            this.iniciarsesion.Location = new System.Drawing.Point(317, 319);
            this.iniciarsesion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iniciarsesion.Name = "iniciarsesion";
            this.iniciarsesion.Size = new System.Drawing.Size(258, 102);
            this.iniciarsesion.TabIndex = 30;
            this.iniciarsesion.Text = "Iniciar Sesión";
            this.iniciarsesion.UseVisualStyleBackColor = true;
            this.iniciarsesion.Click += new System.EventHandler(this.iniciarsesion_Click_1);
            // 
            // Consulta1
            // 
            this.Consulta1.Location = new System.Drawing.Point(37, 442);
            this.Consulta1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Consulta1.Name = "Consulta1";
            this.Consulta1.Size = new System.Drawing.Size(242, 115);
            this.Consulta1.TabIndex = 31;
            this.Consulta1.Text = "Dime el nombre de los usuarios menores de edad con más de 40 puntos";
            this.Consulta1.UseVisualStyleBackColor = true;
            this.Consulta1.Click += new System.EventHandler(this.Consulta1_Click);
            // 
            // Consulta2
            // 
            this.Consulta2.Location = new System.Drawing.Point(317, 442);
            this.Consulta2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Consulta2.Name = "Consulta2";
            this.Consulta2.Size = new System.Drawing.Size(258, 115);
            this.Consulta2.TabIndex = 32;
            this.Consulta2.Text = "Dame los puntos de las partidas que ha ganado Luis44 en menos de 5 minutos";
            this.Consulta2.UseVisualStyleBackColor = true;
            this.Consulta2.Click += new System.EventHandler(this.Consulta2_Click);
            // 
            // invitar
            // 
            this.invitar.Location = new System.Drawing.Point(80, 599);
            this.invitar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.invitar.Name = "invitar";
            this.invitar.Size = new System.Drawing.Size(201, 105);
            this.invitar.TabIndex = 33;
            this.invitar.Text = "Invitar a (escribe el usuario exacto con quién desees jugar):";
            this.invitar.UseVisualStyleBackColor = true;
            this.invitar.Click += new System.EventHandler(this.invitar_Click_1);
            // 
            // invitado
            // 
            this.invitado.Location = new System.Drawing.Point(334, 638);
            this.invitado.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.invitado.Name = "invitado";
            this.invitado.Size = new System.Drawing.Size(148, 26);
            this.invitado.TabIndex = 34;
            // 
            // jugar
            // 
            this.jugar.Location = new System.Drawing.Point(174, 730);
            this.jugar.Name = "jugar";
            this.jugar.Size = new System.Drawing.Size(308, 72);
            this.jugar.TabIndex = 35;
            this.jugar.Text = "Empezar partida";
            this.jugar.UseVisualStyleBackColor = true;
            this.jugar.Click += new System.EventHandler(this.jugar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(2165, 870);
            this.Controls.Add(this.jugar);
            this.Controls.Add(this.invitado);
            this.Controls.Add(this.invitar);
            this.Controls.Add(this.Consulta2);
            this.Controls.Add(this.Consulta1);
            this.Controls.Add(this.iniciarsesion);
            this.Controls.Add(this.registrarse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.desconectar);
            this.Controls.Add(this.conectar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.contrasena);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.matriz);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.matriz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView matriz;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox contrasena;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button registrarse;
        private System.Windows.Forms.Button iniciarsesion;
        private System.Windows.Forms.Button Consulta1;
        private System.Windows.Forms.Button Consulta2;
        private System.Windows.Forms.Button invitar;
        private System.Windows.Forms.TextBox invitado;
        private System.Windows.Forms.Button jugar;
    }
}

