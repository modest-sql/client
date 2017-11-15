using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private List<Metadata> _currentMetadata = new List<Metadata>();

        private void PrintMetadata(string json)
        {
            var dbMeta = ConvertToOldDB(JsonConvert.DeserializeObject<ResponseDb>(json));
            update_new_metadata(dbMeta);
            draw_db_meta();
        }

        private DbMetadata ConvertToOldDB(ResponseDb newDb)
        {
            DbMetadata dbMeta = new DbMetadata { DbName = newDb.DB_Name };
            List<DbTable> tableList = new List<DbTable>();

            foreach (var table in newDb.Tables)
            {
                DbTable oldTable = new DbTable { TableName = table.TableName };

                List<string> columnNames = new List<string>();
                List<string> columnTypes = new List<string>();

                foreach (var field in table.TableColumns)
                {
                    columnNames.Add(field.ColumnName);
                    if (field.ColumnType == DataType.Char)
                        columnTypes.Add(field.ColumnType.ToString() + "(" + field.ColumnSize + ")");
                    else
                        columnTypes.Add(field.ColumnType.ToString());
                }

                oldTable.ColumnNames = columnNames.ToArray();
                oldTable.ColumnTypes = columnTypes.ToArray();

                tableList.Add(oldTable);
            }
            dbMeta.Tables = tableList.ToArray();
            return dbMeta;
        }

        private void update_new_metadata(DbMetadata dbMeta)
        {
            check_column_type_integrity(dbMeta);
            _currentMetadata.Clear();
            DataBase db = BuildDatabase(dbMeta);
            fill_new_meta_list(db);
        }

        private void draw_db_meta()
        {
            metadata_listBox.Items.Clear();
            update_current_meta();
            if (_currentMetadata.Count != 0)
            {
                foreach (var meta in _currentMetadata)
                {
                    switch (meta.MetadataType)
                    {
                        case MetadataType.DbName:
                            metadata_listBox.Items.Add(meta.ValueName);
                            break;
                        case MetadataType.DbTable:
                            metadata_listBox.Items.Add("\t" + meta.ValueName);
                            break;
                        case MetadataType.ColumnName:
                            metadata_listBox.Items.Add("\t\t" + meta.ValueName);
                            break;
                        case MetadataType.ColumnType:
                            metadata_listBox.Items.Add("\t\t\t" + meta.ValueName);
                            break;
                    }
                }
            }
        }

        private void check_column_type_integrity(DbMetadata dbMeta)
        {
            foreach (var table in dbMeta.Tables)
            {
                if (table.ColumnNames.Length != table.ColumnTypes.Length)
                    throw new Exception("Hey! Column count and type count don't match in table " + table.TableName);
            }
        }

        private DataBase BuildDatabase(DbMetadata dbMeta)
        {
            DataBase db = new DataBase()
            {
                MetadataType = MetadataType.DbName,
                Expanded = true,
                ValueName = dbMeta.DbName
            };

            foreach (var table in dbMeta.Tables)
            {
                Table tbl = new Table()
                {
                    MetadataType = MetadataType.DbTable,
                    ValueName = table.TableName,
                    Expanded = false
                };

                for (int i = 0; i < table.ColumnNames.Length; i++)
                {
                    Column clm = new Column()
                    {
                        MetadataType = MetadataType.ColumnName,
                        Expanded = false,
                        ValueName = table.ColumnNames[i]
                    };
                    ColumnType clmType = new ColumnType()
                    {
                        MetadataType = MetadataType.ColumnType,
                        Expanded = false,
                        ValueName = table.ColumnTypes[i]
                    };
                    clm.ColumnType = clmType;
                    tbl.Columns.Add(clm);
                }
                db.Tables.Add(tbl);
            }
            return db;
        }

        private void fill_new_meta_list(DataBase db)
        {
            _currentMetadata.Add(db);

            foreach (var table in db.Tables)
            {
                _currentMetadata.Add(table);
            }
        }

        private void update_current_meta()
        {
            List<Metadata> updatedMetadata = new List<Metadata>();

            foreach (var meta in _currentMetadata)
            {
                if (meta.MetadataType == MetadataType.DbName)
                {
                    updatedMetadata.Add(meta);
                    if (meta.Expanded)
                    {
                        foreach (var table in ((DataBase)meta).Tables)
                        {
                            updatedMetadata.Add(table);
                            if (table.Expanded)
                            {
                                foreach (var column in table.Columns)
                                {
                                    updatedMetadata.Add(column);
                                    if (column.Expanded)
                                        updatedMetadata.Add(column.ColumnType);
                                }
                            }
                        }
                    }
                }
            }
            _currentMetadata = updatedMetadata;
        }

        private void metadata_listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = metadata_listBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                switch (_currentMetadata[index].MetadataType)
                {
                    case MetadataType.DbName:
                        switch_database(_currentMetadata[index].ValueName);
                        break;
                    case MetadataType.DbTable:
                        switch_table(_currentMetadata[index].ValueName, index);
                        break;
                    case MetadataType.ColumnName:
                        switch_column(_currentMetadata[index].ValueName, index);
                        break;
                }
            }
            draw_db_meta();
        }

        private void switch_database(string dbName)
        {
            foreach (var meta in _currentMetadata)
            {
                if (meta.ValueName == dbName && meta.MetadataType == MetadataType.DbName)
                {
                    meta.Expanded = !meta.Expanded;
                    foreach (var table in ((DataBase)meta).Tables)
                    {
                        table.Expanded = false;
                        foreach (var column in table.Columns)
                        {
                            column.Expanded = false;
                            column.ColumnType.Expanded = false;
                        }
                    }
                    break;
                }
            }
        }

        private void switch_table(string tableName, int index)
        {
            int counter = index;
            string parentDatabase = "";
            while (counter >= 0)
            {
                if (_currentMetadata[counter].MetadataType == MetadataType.DbName)
                {
                    parentDatabase = _currentMetadata[counter].ValueName;
                    break;
                }
                counter--;
            }
            foreach (var meta in _currentMetadata)
            {
                if (meta.ValueName == parentDatabase && meta.MetadataType == MetadataType.DbName)
                {
                    foreach (var table in ((DataBase)meta).Tables)
                    {
                        if (table.ValueName == tableName)
                        {
                            table.Expanded = !table.Expanded;
                            foreach (var column in table.Columns)
                            {
                                column.Expanded = false;
                                column.ColumnType.Expanded = false;
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void switch_column(string columnName, int index)
        {
            int counter = index;
            string parentDatabase = "";
            string parentTable = "";
            while (counter >= 0)
            {
                if (_currentMetadata[counter].MetadataType == MetadataType.DbTable)
                {
                    parentTable = _currentMetadata[counter].ValueName;
                    break;
                }
                counter--;
            }
            while (counter >= 0)
            {
                if (_currentMetadata[counter].MetadataType == MetadataType.DbName)
                {
                    parentDatabase = _currentMetadata[counter].ValueName;
                    break;
                }
                counter--;
            }
            foreach (var meta in _currentMetadata)
            {
                if (meta.ValueName == parentDatabase && meta.MetadataType == MetadataType.DbName)
                {
                    foreach (var table in ((DataBase)meta).Tables)
                    {
                        if (table.ValueName == parentTable)
                        {
                            foreach (var column in table.Columns)
                            {
                                if (column.ValueName == columnName)
                                {
                                    column.Expanded = !column.Expanded;
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }
    }
    abstract class Metadata
    {
        public string ValueName { get; set; }
        public MetadataType MetadataType { get; set; }
        public bool Expanded { get; set; }
    }

    class DataBase : Metadata
    {
        public List<Table> Tables = new List<Table>();
    }

    class Table : Metadata
    {
        public List<Column> Columns = new List<Column>();
    }

    class Column : Metadata
    {
        public ColumnType ColumnType;
    }

    class ColumnType : Metadata
    {

    }
    enum MetadataType
    {
        DbName,
        DbTable,
        ColumnName,
        ColumnType
    }

    class DbMetadata
    {
        public string DbName { get; set; }
        public DbTable[] Tables { get; set; }
    }

    class DbTable
    {
        public string TableName { get; set; }
        public string[] ColumnNames { get; set; }
        public string[] ColumnTypes { get; set; }
    }

    class ResponseDb
    {
        public string DB_Name { get; set; } //do not change this till change in server!!
        public ResponseTable[] Tables { get; set; }
    }

    class ResponseTable
    {
        public string TableName { get; set; }
        public ResponseColumn[] TableColumns { get; set; }
    }

    class ResponseColumn
    {
        public string ColumnName { get; set; }
        public DataType ColumnType { get; set; }
        public UInt16 ColumnSize { get; set; }
    }

    enum DataType
    {
        Integer,
        Float,
        Boolean,
        Char,
        Datetime
    }
}
