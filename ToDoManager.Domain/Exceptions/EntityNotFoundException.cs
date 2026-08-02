namespace ToDoManager.Domain.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string entityName, object id) :
            base($"'{entityName}' with 'Id':'{id}' was not found.")
        {
        }
        public EntityNotFoundException(string entityName, string paremterName, object parameterValue) :
        base($"'{entityName}' not found: {paremterName} = '{parameterValue}'")
        {
        }
    }
