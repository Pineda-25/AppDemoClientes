using System;
using System.Windows.Forms;
using BOL;

namespace UI
{
    public partial class FrmMain : Form
    {
        private readonly ClienteBol? _clienteBol;
        private readonly ProveedorBol? _proveedorBol;

        // Constructor para diseñador de Visual Studio
        public FrmMain()
        {
            InitializeComponent();
        }

        public FrmMain(ClienteBol clienteBol, ProveedorBol proveedorBol) : this()
        {
            _clienteBol = clienteBol;
            _proveedorBol = proveedorBol;
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (_clienteBol == null) return;
            using var frm = new FrmCliente(_clienteBol);
            frm.ShowDialog(this);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            if (_proveedorBol == null) return;
            using var frm = new FrmProveedor(_proveedorBol);
            frm.ShowDialog(this);
        }
    }
}
