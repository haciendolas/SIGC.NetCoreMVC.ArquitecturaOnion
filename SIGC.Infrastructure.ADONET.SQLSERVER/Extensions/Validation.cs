using Microsoft.Data.SqlClient;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Extensions
{
    public class Validation
    {
        #region "Validaciones para Tipo de dato"
        #region  "INICIO VALIDACION PARA MSSQL"
        public static DateTime SqlDBToDateTime(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? DateTime.MinValue : Convert.ToDateTime(reader[ColumnName]);
        }

        public static string SqlDBToString(ref SqlDataReader reader, string ColumnName)
        {
            return (reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? "" : Convert.ToString(reader[ColumnName])).Trim();
        }

        public static int SqlDBToInt32(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? 0 : (int)reader[ColumnName];
        }

        public static long SqlDBToInt64(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? 0 : (long)reader[ColumnName];
        }
        public static decimal SqlDBToDecimal(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? 0 : Convert.ToDecimal(reader[ColumnName]);
        }

        public static short SqlDBToInt16(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? (short)0 : (short)reader[ColumnName];
        }

        public static bool SqlDBToBoolean(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? false : (bool)reader[ColumnName];
        }

        public static float SqlDBToFloat(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? 0 : (float)reader[ColumnName];
        }
        public static byte[] SqlDBToByte(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? null : (byte[])reader[ColumnName];
        }
        public static TimeSpan SqlDBToTimeSpan(ref SqlDataReader reader, string ColumnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(ColumnName)) ? TimeSpan.MinValue : reader.GetTimeSpan(reader.GetOrdinal(ColumnName));
        }

        #endregion  "INICIO VALIDACION PARA MSSQL"     

        #endregion "Validaciones para Tipo de dato
    }
}
