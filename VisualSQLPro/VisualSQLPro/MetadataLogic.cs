﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        private List<Metadata> _currentMetadata = new List<Metadata>();
        private readonly Timer _metadataListboxTimer = new Timer();
        private bool _firstClick;
        private bool _secondClick;

        private void SetUpMetadata()
        {
            ColumnHeader header = new ColumnHeader();
            header.Text = "";
            header.Name = "col1";
            metadata_listBox.Columns.Add(header);
            metadata_listBox.HeaderStyle = ColumnHeaderStyle.None;

            metadata_listBox.Scrollable = true;
            metadata_listBox.View = View.Details;

            metadata_listBox.MouseUp += metadata_listBox_MouseUp;

            _metadataListboxTimer.Interval = 200;
            _metadataListboxTimer.Enabled = false;
            _metadataListboxTimer.Elapsed += MetadataListboxTimerEvent;
            _metadataListboxTimer.AutoReset = true;

            ImageList imageListSmall = new ImageList();
            imageListSmall.Images.Add(Bitmap.FromFile(GetAssetFilePath("db2_icon.bmp")));
            imageListSmall.Images.Add(Bitmap.FromFile(GetAssetFilePath("table_icon.bmp")));
            imageListSmall.Images.Add(Bitmap.FromFile(GetAssetFilePath("column_icon.bmp")));
            imageListSmall.Images.Add(Bitmap.FromFile(GetAssetFilePath("orange_key_icon.bmp")));
            //imageListSmall.ImageSize = new Size(24, 24);

            metadata_listBox.View = View.SmallIcon;
            metadata_listBox.SmallImageList = imageListSmall;
        }

        private void MetadataListboxTimerEvent(object sender, ElapsedEventArgs e)
        {
            UpdateMetadataClickManager();
        }

        private void MetadataClickLogic()
        {
            if (_firstClick && _secondClick)
            {
                MetadataDoubleClick();
            }
            else if (_firstClick)
            {
                MetadataSingleClick();
            }
            _firstClick = false;
            _secondClick = false;
            _metadataListboxTimer.Enabled = false;
        }

        private void metadata_listBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_firstClick == false)
            {
                _firstClick = true;
                _metadataListboxTimer.Enabled = true;
            }
            else
            {
                _secondClick = true;
            }
        }

        private void PrintMetadata(string json)
        {
            _currentMetadata.Clear();
            var metadataArray = JsonConvert.DeserializeObject<ResponseMetadataArray>(json);
            foreach (var db in metadataArray.Databases)
            {
                var dbMeta = ConvertToOldDB(db);
                update_new_metadata(dbMeta);
            }
            draw_db_meta();
        }

        private DbMetadata ConvertToOldDB(ResponseDb newDb)
        {
            DbMetadata dbMeta = new DbMetadata {DbName = newDb.DB_Name};
            List<DbTable> tableList = new List<DbTable>();
            dbMeta.Tables = tableList.ToArray();
            if (newDb.Tables == null)
                return dbMeta;
            foreach (var table in newDb.Tables)
            {
                DbTable oldTable = new DbTable {TableName = table.TableName};

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
            DataBase db = BuildDatabase(dbMeta);
            fill_new_meta_list(db);
        }

        private void draw_db_meta()
        {
            metadata_listBox.Items.Clear();
            update_current_meta();
            int counter = 0;
            if (_currentMetadata.Count != 0)
            {
                foreach (var meta in _currentMetadata)
                {
                    switch (meta.MetadataType)
                    {
                        case MetadataType.DbName:
                            metadata_listBox.Items.Add(meta.ValueName);
                            metadata_listBox.Items[counter].ImageIndex = 0;
                            break;
                        case MetadataType.DbTable:
                            metadata_listBox.Items.Add(Spaces(5) + meta.ValueName);
                            metadata_listBox.Items[counter].ImageIndex = 1;
                            break;
                        case MetadataType.ColumnName:
                            metadata_listBox.Items.Add(Spaces(10) + meta.ValueName);
                            metadata_listBox.Items[counter].ImageIndex = 2;
                            break;
                        case MetadataType.ColumnType:
                            metadata_listBox.Items.Add(Spaces(15) + meta.ValueName);
                            break;
                    }
                    counter++;
                }
            }
            DrawActiveDb();
            metadata_listBox.Columns[0].Width = -1;
            //metadata_listBox.Items[0].ImageIndex = 0;
        }

        private void DrawActiveDb()
        {
            int itemCount = metadata_listBox.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                if (metadata_listBox.Items[i].Text == _activeDb &&
                    _currentMetadata[i].MetadataType == MetadataType.DbName)
                    metadata_listBox.Items[i].Font = new Font(metadata_listBox.Items[i].Font.FontFamily,
                        metadata_listBox.Items[i].Font.Size, metadata_listBox.Items[i].Font.Style | FontStyle.Bold);
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
                        foreach (var table in ((DataBase) meta).Tables)
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

        private void MetadataDoubleClick()
        {
            int index;
            try
            {
                index = metadata_listBox.FocusedItem.Index;
            }
            catch (Exception)
            {
                index = -2;
            }

            if (index != ListBox.NoMatches && index != -2)
            {
                switch (_currentMetadata[index].MetadataType)
                {
                    case MetadataType.DbName:
                        BuildAndSendServerRequest((int) ServerRequests.LoadDatabase, _currentMetadata[index].ValueName);
                        _activeDb = _currentMetadata[index].ValueName;
                        draw_db_meta();
                        break;
                }
            }
        }

        private void MetadataSingleClick()
        {
            int index;
            try
            {
                index = metadata_listBox.FocusedItem.Index;
            }
            catch (Exception)
            {
                index = -2;
            }
            
            if (index != ListBox.NoMatches && index != -2)
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

        private void refresh_metadata_button_Click(object sender, EventArgs e)
        {
            BuildAndSendServerRequest((int) ServerRequests.GetMetadata, " ");
        }

        private string Spaces(int quantity)
        {
            string returnSpaces = "";
            for (int i = 0; i < quantity; i++)
            {
                returnSpaces += " ";
            }
            return returnSpaces;
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

    class ResponseMetadataArray
    {
        public ResponseDb[] Databases { get; set; }
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
        Datetime,
        Char,
        Invalid
    }
}
