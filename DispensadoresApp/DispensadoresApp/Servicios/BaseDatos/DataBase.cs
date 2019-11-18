using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DispensadoresApp.Servicios.BaseDatos
{
    public class DataBase
    {
        //Atributos
        public readonly SQLiteAsyncConnection connection;

        //Constructores
        public DataBase(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
        }

    }
}
