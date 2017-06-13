using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Data.SQLite;

namespace EnterTheColiseum
{
    static class Database
    {
        //Fields
        static private string database = "EnterTheColiseum";
        static private SQLiteConnection connection;
        static private string command;
        static private SQLiteCommand commander;
        static private SQLiteDataReader reader;

        //Properties
        static public string DatabaseName
        {
            get { return database; }
        }
        static public SQLiteConnection Connection
        {
            get { return connection; }
        }
        static public SQLiteDataReader Reader
        {
            get { return reader; }
        }

        //Constructor - Static Class

        //Methods
        /// <summary>
        /// Performs first time setup for database. If database already exists, setup is ignored.
        /// </summary>
        static public void Setup()
        {
            SQLiteConnection.CreateFile(database + ".db");
            connection = new SQLiteConnection($"Data Source = {database}.db;Version = 3");
            connection.Open();
            try
            {
                command = "create table gladiators(name text primary key, strength float, agility float, strategy float, helmet text, armour text, weapon text);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                command = "create table equipment(name text primary key, attack float, defense float, type text, cost float);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                command = "insert into gladiators values('Ains Ooal Gown', 10, 10, 10, null, null, null);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                command = "insert into gladiators values('Kappa Pride', 7, 5, 2, null, null, null);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                //Insert all equipment in the game into table equipment
            }
            catch (SQLiteException)
            {
                Console.WriteLine("SQLiteException: Table exists. Setup cancelled.");
            }
        }
        /// <summary>
        /// Creates a table with the specified name and rows.
        /// </summary>
        /// <param name="tableName">Specify name of table to create.</param>
        /// <param name="rows">Specify the columns of the table.</param>
        static public void Create_Table(string tableName, string rows)
        {
            command = $"create table {tableName}({rows});";
            commander = new SQLiteCommand(command, connection);
            commander.ExecuteNonQuery();
            
        }
        /// <summary>
        /// Inserts a row into the specified table. When adding a new table to setup, add it to the list below for easy reference.
        /// List of tables:
        /// gladiators(name text primary key, strength float, agility float, strategy float, helmet text, armour text, weapon text)
        /// equipment(name text primary key, attack float, defense float, type text, cost float)
        /// </summary>
        /// <remarks>Remember to pass the correct values in a single string.</remarks>
        /// <param name="tableName"></param>
        /// <param name="values"></param>
        static public void Create_Row(string tableName, string values)
        {
            command = $"insert into {tableName} values({values});";
            commander = new SQLiteCommand(command, connection);
            commander.ExecuteNonQuery();
        }
        /// <summary>
        /// Prepares an executing reader, reading from the specified table based on supplied search term for column.
        /// To retrieve data, use Database.Reader.
        /// </summary>
        /// <param name="tableName">Specify name of table.</param>
        /// <param name="column">Specify column to search within.</param>
        /// <param name="searchTerm">Specify database search term. Include 'text' when passing text. Pass null if no search term is desired.</param>
        /// <returns></returns>
        static public void Read(string tableName, string column, string searchTerm)
        {
            if (searchTerm != null)
            {
                command = $"select * from {tableName} where {column} = {searchTerm};";
            }
            else
            {
                command = $"select * from {tableName};";
            }
            commander = new SQLiteCommand(command, connection);
            reader = commander.ExecuteReader();
        }
        /// <summary>
        /// Updates target column of specified table to a target value based on supplied search term.
        /// </summary>
        /// <param name="table">Specify name of table.</param>
        /// <param name="targetColumn">Specify target column to update values for.</param>
        /// <param name="targetValue">Specify target value to update column to. Include 'text' when passing text.</param>
        /// <param name="column">Specify column to search in. Pass null if no search is desired.</param>
        /// <param name="searchTerm">Specify search term for column. Include 'text' when passing text. Pass null if no search is desired.</param>
        static public void Update(string table, string targetColumn, string targetValue, string column, string searchTerm)
        {
            if (column != null && searchTerm != null)
            {
                command = $"update {table} set {targetColumn} = {targetValue} where {column} = {searchTerm};";
            }
            else
            {
                command = $"update {table} set {targetColumn} = {targetValue};";
            }
            commander = new SQLiteCommand(command, connection);
            commander.ExecuteNonQuery();
        }
        /// <summary>
        /// Deletes rows from specified table based on supplied search term for column.
        /// </summary>
        /// <param name="tableName">Specify name of table.</param>
        /// <param name="column">Specify column to search in. Do not pass null.</param>
        /// <param name="searchTerm">Specify search term for column. Do not pass null.</param>
        static public void Delete(string tableName, string column, string searchTerm)
        {
            command = $"delete from {tableName} where {column} = {searchTerm}";
            commander = new SQLiteCommand(command, connection);
            commander.ExecuteNonQuery();
        }
    }
}
