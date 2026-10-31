namespace UI
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            btnClientes = new Button();
            btnProveedores = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(110, 30);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(185, 21);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Mantenimiento General";
            // 
            // btnClientes
            // 
            btnClientes.Font = new Font("Segoe UI", 10F);
            btnClientes.Location = new Point(100, 75);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(200, 40);
            btnClientes.TabIndex = 1;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            // 
            // btnProveedores
            // 
            btnProveedores.Font = new Font("Segoe UI", 10F);
            btnProveedores.Location = new Point(100, 130);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.Size = new Size(200, 40);
            btnProveedores.TabIndex = 2;
            btnProveedores.Text = "Proveedores";
            btnProveedores.UseVisualStyleBackColor = true;
            btnProveedores.Click += btnProveedores_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 210);
            Controls.Add(btnProveedores);
            Controls.Add(btnClientes);
            Controls.Add(lblTitulo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menú Principal";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnClientes;
        private Button btnProveedores;
    }
}
