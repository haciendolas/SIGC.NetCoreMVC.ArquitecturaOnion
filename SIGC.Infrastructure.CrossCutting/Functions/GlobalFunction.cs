namespace SIGC.Infrastructure.CrossCutting.Functions
{
    public static class GlobalFunction
    {
        public static DateTime ChangeToUtcDate(string timeZoneCode, DateTime currentDate)
        {
            var currentTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            var utcDate = TimeZoneInfo.ConvertTime(DateTime.SpecifyKind(currentDate, DateTimeKind.Unspecified), currentTimeZone);

            return utcDate;
        }

        /*
        public static AuditDto? GetAudit<T>(T oldObj, T newObj, string operationType)
        {
            var props = typeof(T).GetProperties();

            var oldValues = new Dictionary<string, object>();
            var newValues = new Dictionary<string, object>();
            var affected = new List<string>();

            foreach (var prop in props)
            {
                var oldValue = oldObj != null ? prop.GetValue(oldObj) : null;
                var newValue = newObj != null ? prop.GetValue(newObj) : null;
            
                if (operationType == OperationTypeConst.CREATE)
                {
                    newValues[prop.Name] = newValue;
                    continue;
                }
            
                if (operationType == OperationTypeConst.DELETE)
                {
                    oldValues[prop.Name] = oldValue;
                    continue;
                }
              
                // UPDATE               
                if (!Equals(oldValue, newValue))
                {
                    oldValues[prop.Name] = oldValue;
                    newValues[prop.Name] = newValue;
                    affected.Add(prop.Name);
                }
            }

            // UPDATE sin cambios
            if (operationType == OperationTypeConst.UPDATE && affected.Count == 0)
                return null;

            return new AuditDto
            {
                OperationType = operationType,
                OldValues = oldValues.Count > 0 ? JsonSerializer.Serialize(oldValues) : null,
                NewValues = newValues.Count > 0 ? JsonSerializer.Serialize(newValues) : null,
                AffectedColumns = affected.Count > 0 ? JsonSerializer.Serialize(affected) : null
            };
        }
        */
    }
}