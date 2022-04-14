namespace ZeroRootKillerServer
{
    public partial class ZRKSForm : Form
    {
        public ZRKSForm()
        {
            InitializeComponent();
        }

        private void ZRKSForm_Load(object sender, EventArgs e)
        {
            ZeroRootKillerServer.ServerClass.Run();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ZeroRootKillerServer.IOClass.ListenServiceAndExecCommand();
        }
    }
}