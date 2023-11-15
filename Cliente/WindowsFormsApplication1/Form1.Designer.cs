namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.conectar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.iniciarsesion = new System.Windows.Forms.Button();
            this.registrarse = new System.Windows.Forms.Button();
            this.Consulta2 = new System.Windows.Forms.Button();
            this.Consulta1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contrasena = new System.Windows.Forms.TextBox();
            this.usuario = new System.Windows.Forms.TextBox();
            this.desconectar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // conectar
            // 
            this.conectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conectar.Location = new System.Drawing.Point(49, 31);
            this.conectar.Margin = new System.Windows.Forms.Padding(4);
            this.conectar.Name = "conectar";
            this.conectar.Size = new System.Drawing.Size(199, 38);
            this.conectar.TabIndex = 4;
            this.conectar.Text = "conectar";
            this.conectar.UseVisualStyleBackColor = true;
            this.conectar.Click += new System.EventHandler(this.conectar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.iniciarsesion);
            this.groupBox1.Controls.Add(this.registrarse);
            this.groupBox1.Controls.Add(this.Consulta2);
            this.groupBox1.Controls.Add(this.Consulta1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.contrasena);
            this.groupBox1.Controls.Add(this.usuario);
            this.groupBox1.Controls.Add(this.desconectar);
            this.groupBox1.Controls.Add(this.conectar);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(965, 670);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cliente";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(560, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 160);
            this.label2.TabIndex = 7;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // iniciarsesion
            // 
            this.iniciarsesion.Location = new System.Drawing.Point(291, 202);
            this.iniciarsesion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.iniciarsesion.Name = "iniciarsesion";
            this.iniciarsesion.Size = new System.Drawing.Size(229, 82);
            this.iniciarsesion.TabIndex = 18;
            this.iniciarsesion.Text = "Iniciar Sesión";
            this.iniciarsesion.UseVisualStyleBackColor = true;
            this.iniciarsesion.Click += new System.EventHandler(this.iniciarsesion_Click);
            // 
            // registrarse
            // 
            this.registrarse.Location = new System.Drawing.Point(49, 202);
            this.registrarse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registrarse.Name = "registrarse";
            this.registrarse.Size = new System.Drawing.Size(215, 82);
            this.registrarse.TabIndex = 17;
            this.registrarse.Text = "Registrarse";
            this.registrarse.UseVisualStyleBackColor = true;
            this.registrarse.Click += new System.EventHandler(this.registrarse_Click);
            // 
            // Consulta2
            // 
            this.Consulta2.Location = new System.Drawing.Point(291, 321);
            this.Consulta2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Consulta2.Name = "Consulta2";
            this.Consulta2.Size = new System.Drawing.Size(229, 92);
            this.Consulta2.TabIndex = 16;
            this.Consulta2.Text = "Dame los puntos de las partidas que ha ganado Luis44 en menos de 5 minutos";
            this.Consulta2.UseVisualStyleBackColor = true;
            this.Consulta2.Click += new System.EventHandler(this.Consulta2_Click);
            // 
            // Consulta1
            // 
            this.Consulta1.Location = new System.Drawing.Point(49, 321);
            this.Consulta1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Consulta1.Name = "Consulta1";
            this.Consulta1.Size = new System.Drawing.Size(215, 92);
            this.Consulta1.TabIndex = 15;
            this.Consulta1.Text = "Dime el nombre de los usuarios menores de edad con más de 40 puntos";
            this.Consulta1.UseVisualStyleBackColor = true;
            this.Consulta1.Click += new System.EventHandler(this.Consulta1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Contrasena";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Usuario";
            // 
            // contrasena
            // 
            this.contrasena.Location = new System.Drawing.Point(101, 150);
            this.contrasena.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(148, 22);
            this.contrasena.TabIndex = 12;
            // 
            // usuario
            // 
            this.usuario.Location = new System.Drawing.Point(101, 107);
            this.usuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(148, 22);
            this.usuario.TabIndex = 11;
            // 
            // desconectar
            // 
            this.desconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desconectar.Location = new System.Drawing.Point(291, 31);
            this.desconectar.Margin = new System.Windows.Forms.Padding(4);
            this.desconectar.Name = "desconectar";
            this.desconectar.Size = new System.Drawing.Size(229, 38);
            this.desconectar.TabIndex = 10;
            this.desconectar.Text = "desconectar";
            this.desconectar.UseVisualStyleBackColor = true;
            this.desconectar.Click += new System.EventHandler(this.desconectar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 692);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button conectar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button desconectar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox contrasena;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.Button Consulta1;
        private System.Windows.Forms.Button Consulta2;
        private System.Windows.Forms.Button iniciarsesion;
        private System.Windows.Forms.Button registrarse;
        private System.Windows.Forms.Label label2;
    }
}

