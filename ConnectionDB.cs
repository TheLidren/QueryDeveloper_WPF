using QueryDeveloper_WPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QueryDeveloper_WPF
{
    public class ConnectionDB
    {
        List<ConnModel> _models = new() 
        { 
            new ConnModel() { Name = "Name 1", Description = "Some Desc", ConnectionString = "Some conn" },
            new ConnModel() { Name = "Name 2", Description = "Some Desc", ConnectionString = "Some conn" }
        };
        public ConnectionDB()
        {
            //_models = [];
        }

        public bool AddConnection(ConnModel model)
        {
            try
            {
                //using (SqlConnection sqlConnection = new())
                //{
                //    sqlConnection.ConnectionString = model.ConnectionString;
                //    sqlConnection.Open();
                    _models.Add(model);
                    MessageBox.Show("Подключение установлено");
                    return true;

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nПодключение не установлено!");
                return false;
            }
        }

        public bool DeleteConnection(ConnModel model) 
        {
            _models.Remove(model);
            return true;
        }

        public List<ConnModel> GetModels()
        {
            return _models;
        }
    }
}
