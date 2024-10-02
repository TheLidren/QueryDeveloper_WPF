using QueryDeveloper_WPF.Model;
using System.Data.SqlClient;
using System.Windows;

namespace QueryDeveloper_WPF
{
    public class ConnectionDB
    {
        ListConnection? SelectModel;
        List<ListConnection> _models;

        public ConnectionDB()
        {
            _models = [];
            SelectModel = new();
        }

        public bool AddConnection(ListConnection model)
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

        public bool UpdateConnection(ListConnection model)
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
