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
        ConnModel? SelectModel;
        List<ConnModel> _models = new() 
        { 
            new ConnModel() { Id = 1, Name = "LocalHost", Description = "Some Desc", ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True" },
            new ConnModel() { Id = 2, Name = "Name 2", Description = "Some Desc", ConnectionString = "Some conn" }
        };
        public ConnectionDB()
        {
            //_models = [];
            SelectModel = new();
        }

        public bool AddConnection(ConnModel model)
        {
            try
            {
                using (SqlConnection sqlConnection = new())
                {
                    sqlConnection.ConnectionString = model.ConnectionString;
                    sqlConnection.Open();
                    model.Id = _models.Count + 1;
                    _models.Add(model);
                    MessageBox.Show("Подключение установлено");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nПодключение не установлено!");
                return false;
            }
        }

        public bool UpdateConnection(ConnModel model)
        {
            try
            {
                SelectModel = _models.Find(e => e.Id == model.Id);
                if (SelectModel == null)
                    return false;
                _models.Remove(SelectModel);
                _models.Insert(model.Id-1, model);
                using (SqlConnection sqlConnection = new())
                {
                    sqlConnection.ConnectionString = SelectModel.ConnectionString;
                    sqlConnection.Open();
                    MessageBox.Show("Подключение установлено");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nПодключение не установлено!");
                return false;
            }


        }

        public List<ConnModel> GetModels()
        {
            return _models;
        }
    }
}
