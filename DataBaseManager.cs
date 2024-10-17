using Microsoft.EntityFrameworkCore;
using QueryDeveloper_WPF.Model;
using QueryDeveloper_WPF.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace QueryDeveloper_WPF
{
    public class DataBaseManager
    {
        ConnDataBase? selectModel;
        AppDbContext _appDbContext;

        public DataBaseManager()
        {
            selectModel = new();
            _appDbContext = new AppDbContext();
        }

        public bool AddConnection(ConnDataBase model)
        {
            try
            {
                using (SqlConnection sqlConnection = new())
                {
                    sqlConnection.ConnectionString = model.ConnectionString;
                    sqlConnection.Open();
                    _appDbContext.Connections.Add(model);
                    _appDbContext.SaveChanges();
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

        public bool UpdateConnection(ConnDataBase model)
        {
            try
            {
                selectModel = _appDbContext.Connections.Find(model.Id);
                if (selectModel == null)
                    return false;
                using (SqlConnection sqlConnection = new())
                {
                    sqlConnection.ConnectionString = selectModel.ConnectionString;
                    sqlConnection.Open();
                    selectModel.ConnectionString = model.ConnectionString;
                    selectModel.Name = model.Name;
                    selectModel.Description = model.Description;
                    _appDbContext.Entry(selectModel).State = EntityState.Modified;
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

        public List<ConnDataBase> GetListConn()
        {
            return _appDbContext.Connections.ToList();
        }

        public (DataView?, DataBaseViewModel?) OpenQuery((string filePath, string fileName) queryFile, ConnDataBase connDataBase, User _currentUser)
        {
            try
            {
                using (SqlConnection sqlConnection = new(connDataBase.ConnectionString))
                {
                    string query = File.ReadAllText(queryFile.filePath);
                    SqlCommand command = new(query, sqlConnection);
                    SqlDataAdapter adapter = new(command);
                    DataTable dataTable = new();
                    adapter.Fill(dataTable);
                    UserQuery userQuery = new() { Name = queryFile.fileName, Path = queryFile.filePath, UserId = _currentUser.Id, ConnDataBaseId = connDataBase.Id };
                    _appDbContext.Quires.Add(userQuery);
                    _appDbContext.SaveChanges();
                    return (dataTable.DefaultView, new() { ConnDataBase = connDataBase, UserQuery = userQuery});
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nПодключение не установлено!");
                return (null, null);
            }
        }
        public DataView? ExecuteQuery(string connectionString, UserQuery userQuery)
        {
            try
            {
                using (SqlConnection sqlConnection = new(connectionString))
                {
                    string query = File.ReadAllText(userQuery.Path);
                    SqlCommand command = new(query, sqlConnection);
                    SqlDataAdapter adapter = new(command);
                    DataTable dataTable = new();
                    adapter.Fill(dataTable);
                    return dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nПодключение не установлено!");
                return null;
            }
        }
    }
}
