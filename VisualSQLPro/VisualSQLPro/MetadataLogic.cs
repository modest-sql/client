using System.Collections.Generic;

namespace VisualSQLPro
{
    public partial class ClientForm
    {
        //private List<Metadata> _currentMetadata = new List<Metadata>();
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
}
