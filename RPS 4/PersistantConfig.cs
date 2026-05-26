using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace RPS {
    // Simple entity class for Setting table
    // Note: Table/Column attributes removed - using raw SQL via DBConnector instead
    class Setting {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
