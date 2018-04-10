using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HouTai
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //CZBK.ItcastOA.BLL.T_FGJHtmlDataService CTF = new CZBK.ItcastOA.BLL.T_FGJHtmlDataService();
           
            //var Distc = CTF.LoadEntities(x => x.delflag != 1 || x.delflag == null).DefaultIfEmpty();
            //var t = Distc.GroupBy(x => new { x.photo, x.FwMianji }).Where(g => g.Count() > 1);
            //List<string> lts = new List<string>();
            //foreach (var a in t)
            //{
            //    var temp1 = CTF.LoadEntities(x => x.photo == a.Key.photo).FirstOrDefault();
            //    if (temp1 != null)
            //    {       
            //       var del = CTF.LoadEntities(x => x.photo == a.Key.photo).DefaultIfEmpty();
            //       DateTime dt = del.Max(x => x.FbTime);
            //       foreach (var d in del)
            //       {
            //           if (d.FbTime != dt)
            //           {
            //                d.delflag = 1;
            //                CTF.EditEntity(d);
            //           }
            //       }            
            //    }

            //}

        }
    }
}
