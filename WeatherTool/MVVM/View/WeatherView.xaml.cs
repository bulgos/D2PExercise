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
using System.Windows.Shapes;

namespace WeatherTool.MVVM.View
{
    public partial class WeatherView : Window
    {
        public WeatherView()
        {
            InitializeComponent();
            DataGrid1.AutoGeneratingColumn += OnDataGridAutogeneratingColumn;
        }

        private void OnDataGridAutogeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var headerName = e.Column.Header as string;

            if (headerName == "Date")
            {
                DataGridTextColumn dateColumn = e.Column as DataGridTextColumn;
                dateColumn.Binding = new Binding(e.PropertyName) { StringFormat = "dd/MM/yyyy" };
            }
        }
    }
}
